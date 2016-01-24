namespace Deep.Magic
{
	using System.Collections.Generic;
	using System.Linq;
	class Level
	{
		public IList<Character> Characters;
	}

	class Game
	{
		private System.Random random;
		private Console console;
		private bool run;
		private Character playerCharacter;
		private RogueLevel currentLevel;

		public void Run()
		{
			this.run = true;
			this.Initialise();

			while(this.run)
			{
				ConsoleKey.PollInput(this.console);

				this.Update();
				this.console.Update();

				// Prepare the input queue for the next frame.
				ConsoleKey.Clear();
			}
		}

		protected virtual void Initialise()
		{
			this.console = new Console(80, 25)
			{
				IsCursorVisible = false
			};
			this.random = new System.Random();
			this.playerCharacter = new Character();
			this.currentLevel = new RogueLevel(this.console);
		}

		protected virtual void Update()
		{
			if (ConsoleKey.Pressed("d"))
			{
				this.console.Clear();
				RogueLevel.DoCorridors(RogueLevel.DoRooms());
			}

			if (ConsoleKey.Pressed("s-q"))
			{
				this.run = false;
			}
		}
	}
}