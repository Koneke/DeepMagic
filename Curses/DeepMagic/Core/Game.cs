namespace Deep.Magic
{
	public class DmGame
	{
		public event System.Action<DmGame, SomethingMovedArgs> SomethingMoved;

		public event System.Action<DmGame, ChangedLevelArgs> ChangedLevel;

		public bool Run { get; set; }

		public Character PlayerCharacter { get; set; }

		public ILevel CurrentLevel { get; set; }

		protected ILevelGenerator LevelGenerator { get; set; }

		public void MoveCharacter(Character character, Coordinate destination)
		{
			var source = character.Position;
			var args = new SomethingMovedArgs(character, source, destination);

			character.Position = destination;

			if (this.SomethingMoved != null)
			{
				this.SomethingMoved(this, args);
			}
		}

		public virtual void Initialise()
		{
			this.PlayerCharacter = new Character();
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

			if (this.ChangedLevel != null)
			{
				this.ChangedLevel(this, new ChangedLevelArgs(this.CurrentLevel));
			}
		}
	}
}