namespace Deep.Magic
{
	using System.Collections.Generic;
	using System.Linq;

	public class Character
	{
		// tileMemory saves copies of tiles, the way we last saw them
		// (so we remember items and stuff lying around, even if they don't.
		// That's why it's an ITile[,].
		// For vision, on the other hand, we just need to know whether we see it or not.
		private Dictionary<ILevel, ITile[,]> tileMemory;
		private bool[,] inVision;
		private AttributeCollection Attributes;

		public Character()
		{
			this.Brain = new PlayerBrain(this);
			this.tileMemory = new Dictionary<ILevel, ITile[,]>();
			this.PaperDoll = new PaperDoll();
			this.Attributes = new AttributeCollection();
		}

		public PaperDoll PaperDoll { get; private set; }

		// A brain is what makes the character tick.
		// For players, the brain takes input.
		// For AI, the brain makes decisions itself.
		// The brain is what then moves the character about.
		public IBrain Brain { get; set; }

		public Coordinate Position { get; set; }

		public void SetupVision(ILevel level)
		{
			this.inVision = new bool[level.Size.X, level.Size.Y];
		}

		public bool Remembers(ILevel level, Coordinate coordinate)
		{
			if (!this.tileMemory.ContainsKey(level))
			{
				return false;
			}

			return this.tileMemory[level][coordinate.X, coordinate.Y] != null;
		}

		public Character SetAttribute(string name, double attributeValue)
		{
			this.Attributes.SetAttribute(name, attributeValue);
			return this;
		}

		public bool HasAttribute(string name)
		{
			return this.Attributes.HasAttribute(name) ||
				this.PaperDoll.Items.Any(item => item.Attributes.HasAttribute(name));
		}

		public double GetAttribute(string name, double? defaultValue = null)
		{
			var any = false;
			double attributeValue;

			if (this.Attributes.HasAttribute(name))
			{
				any = true;
				attributeValue = this.Attributes.GetAttribute(name);
			}
			else
			{
				attributeValue = 0;
			}

			foreach (var item in this.PaperDoll.Items)
			{
				if (item.Attributes.HasAttribute(name))
				{
					any = true;
					attributeValue += item.Attributes.GetAttribute(name);
				}
			}

			if (!any)
			{
				if (defaultValue.HasValue)
				{
					return defaultValue.Value;
				}

				throw new System.ArgumentException();
			}

			return attributeValue;
		}
	}
}