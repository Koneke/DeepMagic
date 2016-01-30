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
		};

		private Dictionary<string, string> devCommands = new Dictionary<string, string>()
		{
			{ "d", "dev:generate-level" },
			{ "s-q", "game:quit" }
		};

		private DmGame game;
		private Dictionary<string, CharacterActionParameterSet> parameterSets;
		private GameInputMap gameInputMap;

		public InputHandler(DmGame game)
		{
			this.game = game;
			this.parameterSets = new Dictionary<string, CharacterActionParameterSet>();
			this.gameInputMap = new GameInputMap();

			this.CreateMapping("move:north",     new MovementAction(this.game, Coordinate.North));
			this.CreateMapping("move:northeast", new MovementAction(this.game, Coordinate.NorthEast));
			this.CreateMapping("move:east",      new MovementAction(this.game, Coordinate.East));
			this.CreateMapping("move:southeast", new MovementAction(this.game, Coordinate.SouthEast));
			this.CreateMapping("move:south",     new MovementAction(this.game, Coordinate.South));
			this.CreateMapping("move:southwest", new MovementAction(this.game, Coordinate.SouthWest));
			this.CreateMapping("move:west",      new MovementAction(this.game, Coordinate.West));
			this.CreateMapping("move:northwest", new MovementAction(this.game, Coordinate.NorthWest));
		}

		public void Update()
		{
			ConsoleKey.Clear();
			ConsoleKey.PollInput();

			foreach (var key in this.devCommands.Keys)
			{
				if (ConsoleKey.Pressed(key) == null)
				{
					continue;
				}

				this.game.ReceiveInput(this.devCommands[key]);
			}

			foreach (var key in this.keyMap.Keys)
			{
				if (ConsoleKey.Pressed(key) == null)
				{
					continue;
				}

				var characterAction = this.keyMap[key];
				this.SendCharacterAction(characterAction);
			}
		}

		private void SendCharacterAction(string mapping)
		{
			this.game.ReceiveCharacterInput(
				this.gameInputMap.GetMapping(mapping),
				this.GetParameterSet(mapping));
		}

		private CharacterActionParameterSet GetParameterSet(string mapping)
		{
			return this.parameterSets[mapping.EzSplit(":")[0]];
		}

		// We use this instead of CreateMapping directly to setup our parametersets.
		private void CreateMapping(string name, ICharacterAction characterAction)
		{
			var parameterSetKey = name.EzSplit(":")[0];
			if (!this.parameterSets.ContainsKey(parameterSetKey))
			{
				this.parameterSets[parameterSetKey] = new CharacterActionParameterSet();
			}

			this.gameInputMap.CreateMapping(name, characterAction);
		}
	}
}