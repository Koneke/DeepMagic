namespace Deep.Magic
{
	public interface ICharacterAction
	{
		bool CanApplyAction(CharacterActionParameterSet parameters);

		void ApplyAction(CharacterActionParameterSet parameters);
	}
}