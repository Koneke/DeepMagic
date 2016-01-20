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
		private bool run;
		private Character playerCharacter;
		private Level currentLevel;

		public void Run()
		{
			this.run = true;

			while(this.run)
			{
				Update();
				Draw();
			}
		}

		protected virtual void Initialise()
		{
			playerCharacter = new Character();
			currentLevel = new Level();
		}

		protected virtual void Update()
		{
			if (ConsoleKey.Pressed("d"))
			{
				playerCharacter.Brain.Dance();
				this.run = false;
			}
		}

		// Async this?
		protected virtual void Draw()
		{
		}
	}
}