namespace Deep.Magic
{
	public class QuitAction : InputAction
	{
		public QuitAction(DmGame game) : base(game)
		{
		}

		public override void ApplyAction(InputActionParameterSet parameters)
		{
			this.Game.Run = false;
		}
	}
}