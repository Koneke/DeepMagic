namespace Deep.Magic
{
	using System.Collections.Generic;
	using System.Linq;

	public class ConsoleKey
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

		public static bool Compare(ConsoleKey key, string shorthand)
		{
			bool alt = false;
			bool ctrl = false;
			bool shift = false;

			char keyChar = key.KeyChar;
			if (keyChar <= 'a')
			{
				keyChar += (char)('a' - 'A');
			}

			shorthand = shorthand.ToLower();
			List<string> parts = shorthand.EzSplit("-");

			if (parts.Count > 1)
			{
				alt = parts[0][0] == 'a';
				ctrl = parts[0][0] == 'c';
				shift = parts[0][0] == 's';
			}

			return
				keyChar == parts.Last()[0] &&
				key.Alt == alt &&
				key.Ctrl == ctrl &&
				key.Shift == shift;
		}

		public static bool Pressed(string shorthand)
		{
			lock (Console.MessageQueue)
			{
				return Console.MessageQueue.Any(key => ConsoleKey.Compare(key, shorthand));
			}
		}
	}
}