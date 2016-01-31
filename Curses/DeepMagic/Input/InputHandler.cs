namespace Deep.Magic
{
	using System.Collections.Generic;

	public class InputHandler
	{
		private Dictionary<string, string> keyMap = new Dictionary<string, string>()
		{
			{ "1", "move:southwest" },
			{ "b", "move:southwest" },
			{ "2", "move:south" },
			{ "j", "move:south" },
			{ "3", "move:southeast" },
			{ "n", "move:southeast" },
			{ "4", "move:west" },
			{ "h", "move:west" },
			{ "6", "move:east" },
			{ "l", "move:east" },
			{ "7", "move:northwest" },
			{ "y", "move:northwest" },
			{ "8", "move:north" },
			{ "k", "move:north" },
			{ "9", "move:northeast" },
			{ "u", "move:northeast" },
			{ "d", "dev:generate-level" },
			{ "s-q", "game:quit" }
		};

		private DmGame game;
		private Dictionary<string, ICharacterAction> characterActions = new Dictionary<string, ICharacterAction>();
		private Dictionary<string, InputAction> inputActions = new Dictionary<string, InputAction>();
		private Dictionary<string, InputActionParameterSet> parameterSets;

		public InputHandler(DmGame game)
		{
			this.game = game;

			this.parameterSets = new Dictionary<string, InputActionParameterSet>();
			this.parameterSets.Add("move", new InputActionParameterSet());
			this.parameterSets.Add("dev", new InputActionParameterSet());
			this.parameterSets.Add("game", new InputActionParameterSet());

			this.characterActions = new Dictionary<string, ICharacterAction>();
			this.characterActions.Add("move:north",     new MovementAction(this.game, Coordinate.North));
			this.characterActions.Add("move:northeast", new MovementAction(this.game, Coordinate.NorthEast));
			this.characterActions.Add("move:east",      new MovementAction(this.game, Coordinate.East));
			this.characterActions.Add("move:southeast", new MovementAction(this.game, Coordinate.SouthEast));
			this.characterActions.Add("move:south",     new MovementAction(this.game, Coordinate.South));
			this.characterActions.Add("move:southwest", new MovementAction(this.game, Coordinate.SouthWest));
			this.characterActions.Add("move:west",      new MovementAction(this.game, Coordinate.West));
			this.characterActions.Add("move:northwest", new MovementAction(this.game, Coordinate.NorthWest));

			this.inputActions = new Dictionary<string, InputAction>();
			this.inputActions.Add("game:quit", new QuitAction(this.game));
			this.inputActions.Add("dev:generate-level", new DevAction(this.game, "generate-level"));
		}

		public void Update()
		{
			ConsoleKey.Clear();
			ConsoleKey.PollInput();

			foreach (var key in this.keyMap.Keys)
			{
				if (ConsoleKey.Pressed(key) == null)
				{
					continue;
				}

				var mapping = this.keyMap[key];

				if (this.inputActions.ContainsKey(mapping))
				{
					this.ApplyAction(mapping);
				}
				else if (this.characterActions.ContainsKey(mapping))
				{
					this.ApplyCharacterAction(mapping);
				}
			}
		}

		private void ApplyAction(string mapping)
		{
			var action = this.inputActions[mapping];
			var parameterSet = this.GetParameterSet(mapping);

			action.ApplyAction(parameterSet);
		}

		private void ApplyCharacterAction(string mapping)
		{
			var action = this.characterActions[mapping];
			var parameterSet = this.GetParameterSet(mapping).Clone();

			parameterSet.SetParameter("character", this.game.PlayerCharacter);

			if (action.CanApplyAction(parameterSet))
			{
				action.ApplyAction(parameterSet);
			}

			// else ???
		}

		private InputActionParameterSet GetParameterSet(string mapping)
		{
			return this.parameterSets[mapping.EzSplit(":")[0]];
		}
	}
}