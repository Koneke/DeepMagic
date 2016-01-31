namespace Deep.Magic
{
	public class DevAction : InputAction
	{
		private string command;

		public DevAction(DmGame game, string command) : base(game)
		{
			this.command = command.ToLower();
		}

		public override void ApplyAction(InputActionParameterSet parameters)
		{
			switch (this.command)
			{
				case "generate-level":
					this.Game.GenerateLevel();
					break;
			}
		}
	}
}