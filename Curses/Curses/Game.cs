namespace Deep.Magic
{
	using Deep.Magic.Implementations.Rogue;

	public class Game
	{
		private DmConsole console;
		private bool run;
		private Character playerCharacter;

		private ILevel currentLevel;
		private ILevelGenerator levelGenerator;
		private ILevelRenderer levelRenderer;

		public void Run()
		{
			this.run = true;
			this.Initialise();

			while (this.run)
			{
				ConsoleKey.PollInput(this.console);

				this.Update();
				this.console.Update();

				// Prepare the input queue for the next frame.
				ConsoleKey.Clear();
			}
		}

		protected virtual void Initialise()
		{
			this.console = new DmConsole(80, 25)
			{
				IsCursorVisible = false
			};
			this.playerCharacter = new Character();

			var generatorParameters = new LevelGeneratorParameters()
				.SetParameter(RogueLevelGenerator.ParameterNames.LevelWidth, 80)
				.SetParameter(RogueLevelGenerator.ParameterNames.LevelHeight, 25)
				.SetParameter(RogueLevelGenerator.ParameterNames.MaxRooms, 9)
				.SetParameter(RogueLevelGenerator.ParameterNames.HorizontalCellCount, 3)
				.SetParameter(RogueLevelGenerator.ParameterNames.VerticalCellCount, 3);

			this.levelGenerator = new RogueLevelGenerator(generatorParameters);
			this.currentLevel = this.levelGenerator.Generate();
			this.levelRenderer = new RogueLevelRenderer(this.console, this.currentLevel);
			this.levelRenderer.RenderLevel();
		}

		protected virtual void Update()
		{
			if (ConsoleKey.Pressed("d"))
			{
				this.console.Clear();
				this.currentLevel = this.levelGenerator.Generate();
				this.levelRenderer.Level = this.currentLevel;
				this.levelRenderer.RenderLevel();
			}

			if (ConsoleKey.Pressed("s-q"))
			{
				this.run = false;
			}
		}
	}
}