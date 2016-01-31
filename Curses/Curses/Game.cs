namespace Curses
{
	using Deep.Magic;
	using Deep.Magic.Implementations.Rogue;

	public class Game : DmGame
	{
		public override void Initialise()
		{
			base.Initialise();

			var generatorParameters = new LevelGeneratorParameters()
				.SetParameter(RogueLevelGenerator.ParameterNames.LevelWidth, 80)
				.SetParameter(RogueLevelGenerator.ParameterNames.LevelHeight, 25)
				.SetParameter(RogueLevelGenerator.ParameterNames.MaxRooms, 9)
				.SetParameter(RogueLevelGenerator.ParameterNames.HorizontalCellCount, 3)
				.SetParameter(RogueLevelGenerator.ParameterNames.VerticalCellCount, 3);

			this.LevelGenerator = new RogueLevelGenerator(generatorParameters);
			this.GenerateLevel();

			this.PlayerCharacter.PaperDoll
				.AddSlot("body")
				.AddSlot("hand")
				.AddSlot("hand");

			var template = new ItemTemplate("leather armor");
			template.AddEquipSlot("body");
			template.Attributes.SetAttribute("armor", 3);

			var item = new Item(template);

			this.PlayerCharacter.PaperDoll.EquipItem(item);
		}
	}
}