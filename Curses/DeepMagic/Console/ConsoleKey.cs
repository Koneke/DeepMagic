namespace Deep.Magic
{
	public partial class ConsoleKey
	{
		public char KeyChar;
		public System.ConsoleKey Key;
		public bool Alt;
		public bool Ctrl;
		public bool Shift;

		public ConsoleKey(
			char keyChar,
			System.ConsoleKey key,
			bool alt,
			bool ctrl,
			bool shift
		) {
			this.KeyChar = keyChar;
			this.Key = key;
			this.Alt = alt;
			this.Ctrl = ctrl;
			this.Shift = shift;
		}
	}
}