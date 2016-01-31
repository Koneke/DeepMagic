namespace Deep.Magic
{
	public abstract class InputAction : IInputAction
	{
		public InputAction(DmGame game)
		{
			this.Game = game;
		}

		protected DmGame Game { get; set; }

		public abstract void ApplyAction(InputActionParameterSet parameters);
	}
}