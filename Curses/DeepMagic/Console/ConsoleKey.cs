namespace Deep.Magic
{
	public partial class ConsoleKey
	{
		public ConsoleKey(
			char keyChar,
			System.ConsoleKey key,
			bool alt,
			bool ctrl,
			bool shift)
		{
			this.KeyChar = keyChar;
			this.Key = key;
			this.Alt = alt;
			this.Ctrl = ctrl;
			this.Shift = shift;
		}

		public char KeyChar { get; private set; }

		public System.ConsoleKey Key { get; private set; }

		public bool Alt { get; private set; }

		public bool Ctrl { get; private set; }

		public bool Shift { get; private set; }
	}
}