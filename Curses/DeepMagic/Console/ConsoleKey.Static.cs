namespace Deep.Magic
{
	using System.Collections.Generic;
	using System.Linq;

	public partial class ConsoleKey
	{
		private static Queue<ConsoleKey> messageQueue = new Queue<ConsoleKey>();

		// Notice that this is not actually any specific console, it's *the* console.
		// We currently only support one.
		public static bool PollInput(DmConsole console)
		{
			lock (messageQueue)
			{
				if (System.Console.KeyAvailable)
				{
					var rawKey = System.Console.ReadKey(true);

					var key = new ConsoleKey(
						rawKey.KeyChar,
						rawKey.Key,
						(rawKey.Modifiers & System.ConsoleModifiers.Alt) > 0,
						(rawKey.Modifiers & System.ConsoleModifiers.Control) > 0,
						(rawKey.Modifiers & System.ConsoleModifiers.Shift) > 0);

					messageQueue.Enqueue(key);
				}
			}

			// Keep going until cancelled.
			return true;
		}

		public static bool Compare(ConsoleKey key, string shorthand)
		{
			bool alt = false;
			bool ctrl = false;
			bool shift = false;

			char keyChar = key.KeyChar;

			// lower case it
			if (keyChar >= 'A' && keyChar <= 'Z')
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
			lock (ConsoleKey.messageQueue)
			{
				return ConsoleKey.messageQueue.Any(key => ConsoleKey.Compare(key, shorthand));
			}
		}

		public static void Clear()
		{
			lock (ConsoleKey.messageQueue)
			{
				ConsoleKey.messageQueue.Clear();
			}
		}
	}
}