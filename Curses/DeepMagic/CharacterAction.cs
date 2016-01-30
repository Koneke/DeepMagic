namespace Deep.Magic
{
	using System;
	using System.Collections.Generic;

	public interface ICharacterAction
	{
		bool CanApplyAction(CharacterActionParameterSet parameters);

		void ApplyAction(CharacterActionParameterSet parameters);
	}

	public class CharacterActionParameterSet
	{
		private Dictionary<string, Type> parameterTypes;
		private Dictionary<string, object> parameters;

		public CharacterActionParameterSet()
		{
			this.parameterTypes = new Dictionary<string, Type>();
			this.parameters = new Dictionary<string, object>();
		}

		public CharacterActionParameterSet Clone()
		{
			var clone = new CharacterActionParameterSet();
			clone.parameterTypes = new Dictionary<string, Type>(this.parameterTypes);
			clone.parameters = new Dictionary<string, object>(this.parameters);

			return clone;
		}

		public CharacterActionParameterSet SetParameter<T>(string name, T parameter)
		{
			name = name.ToLower();

			if (this.parameters.ContainsKey(name))
			{
				this.parameterTypes[name] = typeof(T);
				this.parameters[name] = parameter;
				return this;
			}

			this.parameterTypes.Add(name, typeof(T));
			this.parameters.Add(name, parameter);

			return this;
		}

		public T GetParameter<T>(string name)
		{
			name = name.ToLower();
			return (T)this.parameters[name];
		}
	}
}

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

		public bool CanApplyAction(CharacterActionParameterSet parameters)
		{
			var level = this.game.CurrentLevel;
			var character = parameters.GetParameter<Character>("character");
			var tile = level.TileAt(character.Position + this.direction);

			return !(tile == null || tile.Solid);
		}

		public void ApplyAction(CharacterActionParameterSet parameters)
		{
			var character = parameters.GetParameter<Character>("character");
			character.Position += this.direction;
			this.game.Render();
		}
	}
}