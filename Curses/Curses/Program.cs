namespace Curses
{
	using Deep.Magic;

	class Program
	{
		public static bool Run;

		static void Main(string[] args)
		{
			var console = new Console();
			console.Clear();

			Program.Run = true;

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

			while(Program.Run) {
				Console.MessageQueue.ForEach(k => Program.Run = Program.Run && k.KeyChar != 'q');
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