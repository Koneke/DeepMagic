namespace Deep.Magic
{
	using System.Collections.Generic;

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
		private Level currentLevel;

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
			this.random = new System.Random();
			this.console = new Console(80, 25);
			this.playerCharacter = new Character();
			this.currentLevel = new Level();
		}

		protected virtual void Update()
		{
			//if (ConsoleKey.Pressed("d"))
			{
				this.console
					.Cursor.SetPosition((short)this.random.Next(0, 77), (short)this.random.Next(0, 24))
					.Cursor.SetColor(Utilities.GetRandomColor(true, true))
					.Write("hi!");
			}

			if (ConsoleKey.Pressed("s-q"))
			{
				this.run = false;
			}
		}
	}
}