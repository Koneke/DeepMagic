namespace Deep.Magic
{
	public interface ICharacterAction : IInputAction
	{
		bool CanApplyAction(InputActionParameterSet parameters);
	}
}