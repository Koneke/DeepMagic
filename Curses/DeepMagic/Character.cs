namespace Deep.Magic
{
	using System.Collections.Generic;

	public class Character
	{
		public Character()
		{
			this.Brain = new PlayerBrain(this);
			this.TileMemory = new Dictionary<ILevel, ITile[,]>();
		}

		// A brain is what makes the character tick.
		// For players, the brain takes input.
		// For AI, the brain makes decisions itself.
		// The brain is what then moves the character about.
		public IBrain Brain { get; set; }

		public Coordinate Position;

		public Dictionary<ILevel, ITile[,]> TileMemory;
	}
}