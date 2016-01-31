namespace Deep.Magic
{
	public class GameManager
	{
		private DmGame game;
		private InputHandler inputHandler;
		private IGameRenderer renderer;

		public GameManager(DmGame game, InputHandler inputHandler, IGameRenderer renderer)
		{
			this.game = game;
			this.inputHandler = inputHandler;
			this.renderer = renderer;
		}

		public void Run()
		{
			this.game.Initialise();
			this.game.Run = true;

			while (this.game.Run)
			{
				this.inputHandler.Update();
				this.renderer.Update();
			}
		}
	}
}