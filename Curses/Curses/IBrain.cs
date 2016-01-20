namespace Deep.Magic
{
	interface IBrain
	{
		Character Character { get; }
		void Dance();
	}

	class PlayerBrain : IBrain
	{
		public Character Character { get; private set; }

		public PlayerBrain(Character character)
		{
			this.Character = character;
		}

		public void Dance()
		{
		}
	}
}