namespace Deep.Magic
{
	public class MovementAction : ICharacterAction
	{
		private Coordinate direction;
		private DmGame game;

		public MovementAction(DmGame game, Coordinate direction)
		{
			this.game = game;
			this.direction = direction;
		}

		public bool CanApplyAction(InputActionParameterSet parameters)
		{
			var level = this.game.CurrentLevel;
			var character = parameters.GetParameter<Character>("character");
			var tile = level.TileAt(character.Position + this.direction);

			return !(tile == null || tile.Solid);
		}

		public void ApplyAction(InputActionParameterSet parameters)
		{
			var character = parameters.GetParameter<Character>("character");
			this.game.MoveCharacter(character, character.Position + this.direction);
		}
	}
}