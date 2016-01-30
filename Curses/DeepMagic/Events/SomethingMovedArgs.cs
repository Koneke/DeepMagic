namespace Deep.Magic
{
	public class SomethingMovedArgs : System.EventArgs
	{
		public SomethingMovedArgs(Character character, Coordinate source, Coordinate destination)
		{
			this.Character = character;
			this.Source = source;
			this.Destination = destination;
		}

		public Character Character { get; set; }

		public Coordinate Source { get; set; }

		public Coordinate Destination { get; set; }
	}
}