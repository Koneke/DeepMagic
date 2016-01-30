namespace Deep.Magic
{
	using System.Collections.Generic;

	public class GameInputMap
	{
		private Dictionary<string, ICharacterAction> mappings;

		public GameInputMap()
		{
			this.mappings = new Dictionary<string, ICharacterAction>();
		}

		public GameInputMap CreateMapping(string name, ICharacterAction characterAction)
		{
			name = name.ToLower();

			// Intentionally not checking if the key already exists; we want the exception to rise.
			this.mappings.Add(name, characterAction);
			return this;
		}

		public ICharacterAction GetMapping(string name)
		{
			return this.mappings[name];
		}
	}
}