namespace Deep.Magic
{
	using System.Collections.Generic;
	using System.Linq;

	public class PaperDoll
	{
		private List<string> slots;
		private List<string> freeSlots;

		public PaperDoll()
		{
			this.slots = new List<string>();
			this.freeSlots = new List<string>();
			this.Items = new List<Item>();
		}

		public List<Item> Items { get; private set; }

		public PaperDoll AddSlot(string slot)
		{
			slot = slot.ToLower();
			this.slots.Add(slot);
			this.freeSlots.Add(slot);
			return this;
		}

		public PaperDoll RemoveSlot(string slot)
		{
			if (this.freeSlots.Contains(slot))
			{
				this.slots.Remove(slot);
				this.freeSlots.Remove(slot);
				return this;
			}

			throw new System.Exception("Can't remove a used or non-existing slot.");
		}

		public bool CanEquipItem(Item item)
		{
			var checkList = new List<string>(this.freeSlots);

			foreach (var slot in item.EquipSlots)
			{
				if (!checkList.Contains(slot))
				{
					return false;
				}

				checkList.Remove(slot);
			}

			return true;
		}

		public void EquipItem(Item item)
		{
			if (!this.CanEquipItem(item))
			{
				throw new System.ArgumentException();
			}

			foreach (var slot in item.EquipSlots)
			{
				this.freeSlots.Remove(slot);
			}

			this.freeSlots = this.freeSlots.Except(item.EquipSlots).ToList();
			this.Items.Add(item);
		}

		public void RemoveItem(Item item)
		{
			this.Items.Remove(item);
			foreach (var slot in item.EquipSlots)
			{
				this.freeSlots.Add(slot);
			}
		}
	}
}