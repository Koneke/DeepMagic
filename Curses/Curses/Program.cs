namespace Curses
{
	using Deep.Magic;

	class Program
	{
		static void Main(string[] args)
		{
			var console = new Console(80, 25);
			console.IsCursorVisible = false;

			Cancellable cancellable = new Cancellable(
				-1, // No delay between polls
				p => ConsoleKey.PollInput(p[0] as Console),
				onCancel: p =>
					(p[0] as Console)
						.Cursor.SetColor(Color.VeryWhite, Color.HalfRed)
						.Cursor.SetPosition(0, 5)
						.Write("Done polling")
				);

			//cancellable.Run(console);

			var game = new Game();
			game.Run();

			//cancellable.Cancel();
		}
	}
}