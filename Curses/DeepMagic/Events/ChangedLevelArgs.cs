namespace Deep.Magic
{
	public class ChangedLevelArgs : System.EventArgs
	{
		public ChangedLevelArgs(ILevel newLevel)
		{
			this.NewLevel = newLevel;
		}

		public ILevel NewLevel { get; private set; }
	}
}