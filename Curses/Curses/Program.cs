namespace Curses
{
	using Deep.Magic;
	using System.Collections.Generic;

	class Program
	{
		public static bool Run;

		static Queue<System.ConsoleKeyInfo> messageQueue = new Queue<System.ConsoleKeyInfo>();

		static bool PollInput(Console console)
		{
			lock(messageQueue)
			{
				messageQueue.Enqueue(System.Console.ReadKey(true));
			}

			// Keep going until cancelled.
			return true;
		}

		static void Main(string[] args)
		{
			var console = new Console();
			console.Clear();

			Run = true;

			Cancellable cancellable = new Cancellable(
				p => PollInput(p[0] as Console),
				onCancel: p =>
					(p[0] as Console)
						.Cursor.SetColor(Color.VeryWhite, Color.HalfRed)
						.Cursor.SetPosition(0, 5)
						.Write("Done polling")
				);

			cancellable.Run(console);

			while(Program.Run) {
				messageQueue.ForEach(k => Program.Run = Program.Run && k.KeyChar != 'q');
			}

			console
				.Cursor.SetForegroundColor(Color.VeryWhite)
				.Cursor.SetPosition(0, 0)
				.Write("We outta here!");

			// Stop polling input.
			cancellable.Cancel();

			System.Console.ReadLine();
		}
	}
}