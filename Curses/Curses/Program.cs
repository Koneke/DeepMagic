namespace Curses
{
	using Deep.Magic;
	using Deep.Magic.Implementations.Rogue;

	public class Program
	{
		public static void Main(string[] args)
		{
			var game = new Game();

			var gm = new GameManager(
				game,
				new InputHandler(game),
				new RogueRenderer(game));

			gm.Run();
		}
	}
}