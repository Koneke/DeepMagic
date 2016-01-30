namespace Deep.Magic
{

	public class DmGame
	{
		public event System.EventHandler SomethingMoved;

		public bool Run { get; set; }

		public Character PlayerCharacter { get; set; }

		public ILevel CurrentLevel { get; set; }

		protected ILevelGenerator LevelGenerator { get; set; }

		public void ReceiveInput(string command)
		{
			switch (command)
			{
				case "dev:generate-level":
					this.GenerateLevel();
					break;
				case "game:quit":
					this.Run = false;
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
		}

		public void MoveCharacter(Character character, Coordinate destination)
		{
			var source = character.Position;
			var args = new SomethingMovedArgs(character, source, destination);

			character.Position = destination;
			this.SomethingMoved(this, args);
		}

		public virtual void Initialise()
		{
			this.PlayerCharacter = new Character();
		}

		public virtual void Update()
		{
		}

		public void GenerateLevel()
		{
			this.CurrentLevel = this.LevelGenerator.Generate();
			this.CurrentLevel.Characters.Add(this.PlayerCharacter);

			this.PlayerCharacter.Position = this.CurrentLevel.TileList
				.SelectRandom(
					true,
					t => t.Type == "floor")
				.Position;
		}
	}
}