namespace Curses
{
	using Deep.Magic;
	using Deep.Magic.Implementations.Rogue;

	class Game : DmGame
	{
		protected override void Initialise()
		{
			base.Initialise();

			var generatorParameters = new LevelGeneratorParameters()
				.SetParameter(RogueLevelGenerator.ParameterNames.LevelWidth, 80)
				.SetParameter(RogueLevelGenerator.ParameterNames.LevelHeight, 25)
				.SetParameter(RogueLevelGenerator.ParameterNames.MaxRooms, 9)
				.SetParameter(RogueLevelGenerator.ParameterNames.HorizontalCellCount, 3)
				.SetParameter(RogueLevelGenerator.ParameterNames.VerticalCellCount, 3);

			this.LevelGenerator = new RogueLevelGenerator(generatorParameters);
			this.Renderer = new RogueRenderer();
			this.GenerateLevel();
		}
	}
}