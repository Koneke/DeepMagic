namespace Deep.Magic
{
	using System.Collections.Generic;

	public class Item
	{
		public Item(string name)
		{
			this.Name = name;
			this.EquipSlots = new List<string>();
			this.Attributes = new AttributeCollection();
		}

		public string Name { get; private set; }

		public List<string> EquipSlots { get; private set; }

		public AttributeCollection Attributes { get; private set; }
	}
}