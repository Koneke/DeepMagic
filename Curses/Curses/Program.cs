namespace Curses
{
	using Deep.Magic;

	class Program
	{
		static void Main(string[] args)
		{
			var console = new Console();
			console.Clear();

			Cancellable cancellable = new Cancellable(
				20, // 20 ms between each poll
				p => Console.PollInput(p[0] as Console),
				onCancel: p =>
					(p[0] as Console)
						.Cursor.SetColor(Color.VeryWhite, Color.HalfRed)
						.Cursor.SetPosition(0, 5)
						.Write("Done polling")
				);

			cancellable.Run(console);

			/*List<ConsoleKey> keys = new List<ConsoleKey>();
			List<ConsoleKey> keysLastFrame = new List<ConsoleKey>();
			while(Program.Run) {
				lock (Console.MessageQueue)
				{
					keys.Clear();
					while (Console.MessageQueue.Count > 0)
					{
						keys.Add(Console.MessageQueue.Dequeue());
					}
				}

				if (keys.Any(k => ConsoleKey.Compare(k, "s-q")))
				{
					break;
				}
			}*/

			var game = new Game();
			game.Run();
		}
	}
}