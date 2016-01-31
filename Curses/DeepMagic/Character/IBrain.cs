namespace Deep.Magic
{
	public interface IBrain
	{
		Character Character { get; }

		bool CanApplyCharacterAction(
			ICharacterAction characterAction,
			InputActionParameterSet parameterSet);

		void ApplyCharacterAction(
			ICharacterAction characterAction,
			InputActionParameterSet parameterSet);
	}

	public class PlayerBrain : IBrain
	{
		public PlayerBrain(Character character)
		{
			this.Character = character;
		}

		public Character Character { get; private set; }

		public bool CanApplyCharacterAction(
			ICharacterAction characterAction,
			InputActionParameterSet parameterSet)
		{
			// Make sure to clone the parameterset so we don't destroy/alter it.
			// (Probably doesn't matter? but feels like good practice).
			var clonedSet = parameterSet.Clone().SetParameter("character", this.Character);
			return characterAction.CanApplyAction(clonedSet);
		}

		public void ApplyCharacterAction(
			ICharacterAction characterAction,
			InputActionParameterSet parameterSet)
		{
			parameterSet.SetParameter("character", this.Character);
			characterAction.ApplyAction(parameterSet);
		}
	}
}