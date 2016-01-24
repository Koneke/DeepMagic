namespace Deep.Magic
{
	using Deep.Magic.Implementations.Rogue;
	using System.Collections.Generic;

	class Level
	{
		public IList<Character> Characters;
	}

	class Game
	{
		private System.Random random;
		private Console console;
		private bool run;
		private Character playerCharacter;
		private ILevel currentLevel;

		private void RenderLevel()
		{
			for (var x = 0; x < currentLevel.Size.X; x++)
			for (var y = 0; y < currentLevel.Size.Y; y++)
			{
				var tile = (RogueTile)currentLevel.TileAt(new Coordinate(x, y));
				if (tile != null)
				{
					this.console
						.Cursor.SetPosition((short)x, (short)y)
						.Write(tile.Appearance);
				}
			}
		}

		public void Run()
		{
			this.run = true;
			this.Initialise();

			while(this.run)
			{
				ConsoleKey.PollInput(this.console);

				this.Update();
				this.console.Update();

				// Prepare the input queue for the next frame.
				ConsoleKey.Clear();
			}
		}

		private ILevelGenerator levelGenerator;

		protected virtual void Initialise()
		{
			this.console = new Console(80, 25)
			{
				IsCursorVisible = false
			};
			this.random = new System.Random();
			this.playerCharacter = new Character();

			var generatorParameters = new LevelGeneratorParameters()
				.SetParameter(RogueLevelGenerator.ParameterNames.LevelWidth, 80)
				.SetParameter(RogueLevelGenerator.ParameterNames.LevelHeight, 25)
				.SetParameter(RogueLevelGenerator.ParameterNames.MaxRooms, 9)
				.SetParameter(RogueLevelGenerator.ParameterNames.HorizontalCellCount, 3)
				.SetParameter(RogueLevelGenerator.ParameterNames.VerticalCellCount, 3);

			this.levelGenerator = new RogueLevelGenerator(generatorParameters);
			this.currentLevel = this.levelGenerator.Generate();
			this.RenderLevel();
		}

		protected virtual void Update()
		{
			if (ConsoleKey.Pressed("d"))
			{
				this.console.Clear();
				this.currentLevel = this.levelGenerator.Generate();
				this.RenderLevel();
			}

			if (ConsoleKey.Pressed("s-q"))
			{
				this.run = false;
			}
		}
	}
}