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

			var item = new Item("leather armor");
			item.EquipSlots.Add("body");
			item.Attributes.SetAttribute("armor", 3);

			var armor = this.PlayerCharacter.GetAttribute("armor", 0);

			var a = this.PlayerCharacter.HasAttribute("armor");
			this.PlayerCharacter.PaperDoll.EquipItem(item);
			var b = this.PlayerCharacter.HasAttribute("armor");

			var armor2 = this.PlayerCharacter.GetAttribute("armor", 0);

			this.PlayerCharacter.SetAttribute("armor", 2);

			var armor3 = this.PlayerCharacter.GetAttribute("armor", 0);
			var c = 0;
		}
	}
}