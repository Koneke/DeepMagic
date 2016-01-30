namespace Deep.Magic
{
	using System.Collections.Generic;

	public class InputHandler
	{
		Dictionary<string, string> keyMap = new Dictionary<string, string>()
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

		DmGame game;
		Dictionary<string, CharacterActionParameterSet> parameterSets;
		GameInputMap gameInputMap;

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
			// Should be here later.
			// ConsoleKey.PollInput();

			foreach (var key in keyMap.Keys)
			{
				if (ConsoleKey.Pressed(key) == null)
				{
					continue;
				}

				var characterAction = keyMap[key];
				SendCharacterAction(characterAction);
			}
		}

		private void SendCharacterAction(string mapping)
		{
			this.game.ReceiveInput(
				this.gameInputMap.GetMapping(mapping),
				this.GetParameterSet(mapping));
		}

		private CharacterActionParameterSet GetParameterSet(string mapping)
		{
			return parameterSets[mapping.EzSplit(":")[0]];
		}

		private void CreateMapping(string name, ICharacterAction characterAction)
		{
			// We use this instead of CreateMapping directly to setup our parametersets.

			var parameterSetKey = name.EzSplit(":")[0];
			if (!parameterSets.ContainsKey(parameterSetKey))
			{
				parameterSets[parameterSetKey] = new CharacterActionParameterSet();
			}

			this.gameInputMap.CreateMapping(name, characterAction);
		}
	}
}