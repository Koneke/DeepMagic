namespace Deep.Magic
{
	public class DmGame
	{
		private bool run;

		public Character PlayerCharacter { get; set; }

		public ILevel CurrentLevel { get; set; }

		protected ILevelGenerator LevelGenerator { get; set; }

		protected InputHandler InputHandler { get; set; }

		protected IGameRenderer Renderer { get; set; }

		public void Run()
		{
			this.run = true;
			this.Initialise();

			while (this.run)
			{
				this.InputHandler.Update();
				this.Update();
			}
		}

		public void ReceiveInput(string command)
		{
			switch (command)
			{
				case "dev:generate-level":
					this.GenerateLevel();
					break;
				case "game:quit":
					this.run = false;
					break;
			}
		}

		public void ReceiveCharacterInput(
			ICharacterAction characterAction,
			CharacterActionParameterSet parameterSet)
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
			this.InputHandler = new InputHandler(this);
			this.PlayerCharacter = new Character();
		}

		protected virtual void Update()
		{
		}

		protected void GenerateLevel()
		{
			this.CurrentLevel = this.LevelGenerator.Generate();
			this.CurrentLevel.Characters.Add(this.PlayerCharacter);

			this.PlayerCharacter.Position = this.CurrentLevel.TileList
				.SelectRandom(
					true,
					t => t.Type == "floor")
				.Position;

			// Do this last, so the character is moved when we render.
			this.Renderer.Level = this.CurrentLevel;
		}
	}
}