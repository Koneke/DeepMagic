namespace Deep.Magic
{
	using Implementations.Rogue;

	public class Game
	{
		private DmConsole console;
		private bool run;
		private Character playerCharacter;

		private ILevel currentLevel;
		private ILevelGenerator levelGenerator;
		private IGameRenderer renderer;

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
			this.renderer = new RogueRenderer(this.console);

			this.GenerateLevel();
		}

		protected virtual void Update()
		{
			if (ConsoleKey.Pressed("d") != null)
			{
				this.GenerateLevel();
			}

			var numpadKeys = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

			var directionKey = ConsoleKey.Any(numpadKeys);

			if (directionKey != null)
			{
				var delta = Coordinate.FromNumpad(directionKey.KeyChar);
				var tileAt = currentLevel.TileAt(playerCharacter.Position + delta);
				if (tileAt != null && !tileAt.Solid)
				{
					playerCharacter.Position += Coordinate.FromNumpad(directionKey.KeyChar);

					// Obviously not the optimal way of doing things, but it's actually clever enough
					// to not to the actual drawing if we already have the right stuff in the buffer.
					// The actual calculations for the rendering are pretty much free it's the actual
					// drawing that takes time.
					this.renderer.RenderLevel();
				}
			}

			if (ConsoleKey.Pressed("s-q") != null)
			{
				this.run = false;
			}
		}

		private void GenerateLevel()
		{
			this.console.Clear();
			this.currentLevel = this.levelGenerator.Generate();
			this.currentLevel.Characters.Add(this.playerCharacter);
			this.renderer.Level = this.currentLevel;

			this.playerCharacter.Position = this.currentLevel.TileList
				.SelectRandom(
					true,
					t => t.Type == "floor")
				.Position;

			this.renderer.RenderLevel();
		}
	}
}