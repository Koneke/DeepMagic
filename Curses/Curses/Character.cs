namespace Deep.Magic
{
	public class Character
	{
		public Character()
		{
			this.Brain = new PlayerBrain(this);
		}

		// A brain is what makes the character tick.
		// For players, the brain takes input.
		// For AI, the brain makes decisions itself.
		// The brain is what then moves the character about.
		public IBrain Brain { get; set; }
	}
}