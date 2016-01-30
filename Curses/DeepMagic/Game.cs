namespace Deep.Magic
{
	public class DmGame
	{
		private bool run;

		public Character PlayerCharacter { get; set; }

		public ILevel CurrentLevel { get; set; }

		protected DmConsole Console { get; set; }

		protected ILevelGenerator LevelGenerator { get; set; }

		protected InputHandler InputHandler { get; set; }

		protected IGameRenderer Renderer { get; set; }

		public void Run()
		{
			this.run = true;
			this.Initialise();

			while (this.run)
			{
				ConsoleKey.PollInput(this.Console);

				this.InputHandler.Update();
				this.Update();
				this.Console.Update();

				// Prepare the input queue for the next frame.
				ConsoleKey.Clear();
			}
		}

		public void ReceiveInput(ICharacterAction characterAction, CharacterActionParameterSet parameterSet)
		{
			if (this.PlayerCharacter.Brain.CanApplyCharacterAction(characterAction, parameterSet))
			{
				this.PlayerCharacter.Brain.ApplyCharacterAction(characterAction, parameterSet);
			}
		}

		public void Render()
		{
			this.Renderer.RenderLevel();
		}

		protected virtual void Initialise()
		{
			this.Console = new DmConsole(80, 25)
			{
				IsCursorVisible = false
			};

			this.InputHandler = new InputHandler(this);
			this.PlayerCharacter = new Character();
		}

		protected virtual void Update()
		{
			if (ConsoleKey.Pressed("d") != null)
			{
				this.GenerateLevel();
			}

			if (ConsoleKey.Pressed("s-q") != null)
			{
				this.run = false;
			}
		}

		protected void GenerateLevel()
		{
			this.Console.Clear();
			this.CurrentLevel = this.LevelGenerator.Generate();
			this.CurrentLevel.Characters.Add(this.PlayerCharacter);
			this.Renderer.Level = this.CurrentLevel;

			this.PlayerCharacter.Position = this.CurrentLevel.TileList
				.SelectRandom(
					true,
					t => t.Type == "floor")
				.Position;

			this.Renderer.RenderLevel();
		}
	}
}