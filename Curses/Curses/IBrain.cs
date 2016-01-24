namespace Deep.Magic
{
	public interface IBrain
	{
		Character Character { get; }

		void Dance();
	}

	public class PlayerBrain : IBrain
	{
		public PlayerBrain(Character character)
		{
			this.Character = character;
		}

		public Character Character { get; private set; }

		public void Dance()
		{
		}
	}
}