namespace Deep.Magic
{
	using System.Collections.Generic;

	public class Character
	{
		// tileMemory saves copies of tiles, the way we last saw them
		// (so we remember items and stuff lying around, even if they don't.
		// That's why it's an ITile[,].
		// For vision, on the other hand, we just need to know whether we see it or not.
		private Dictionary<ILevel, ITile[,]> tileMemory;
		private bool[,] inVision;

		public Character()
		{
			this.Brain = new PlayerBrain(this);
			this.tileMemory = new Dictionary<ILevel, ITile[,]>();
		}

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
	}
}