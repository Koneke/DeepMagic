namespace Deep.Magic
{
	using System.Collections.Generic;

	public class ItemTemplate
	{
		private static Dictionary<string, ItemTemplate> itemTemplates = new Dictionary<string, ItemTemplate>();

		public static ItemTemplate GetTemplate(string name)
		{
			return itemTemplates[name];
		}

		private List<string> equipSlots;

		public ItemTemplate(string itemType)
		{
			itemType = itemType.ToLower();
			this.ItemType = itemType;
			itemTemplates.Add(itemType, this);

			this.Attributes = new AttributeCollection();
			this.equipSlots = new List<string>();
		}

		public AttributeCollection Attributes { get; private set; }

		public string ItemType { get; private set; }

		public string DisplayName { get; private set; }

		public IEnumerable<string> EquipSlots
		{
			get
			{
				foreach (var slot in this.equipSlots)
				{
					yield return slot;
				}
			}
		}

		public ItemTemplate AddEquipSlot(string slot)
		{
			this.equipSlots.Add(slot.ToLower());
			return this;
		}
	}

	public class Item
	{
		public Item(ItemTemplate template)
		{
			this.Template = template;
			this.Attributes = new AttributeCollection(template.Attributes);
		}

		public ItemTemplate Template { get; private set; }

		public AttributeCollection Attributes { get; private set; }

		public IEnumerable<string> EquipSlots
		{
			get
			{
				return this.Template.EquipSlots;
			}
		}
	}
}