namespace Deep.Magic
{
	public interface ILevelGeneratorParameters
	{
		T GetParameter<T>(string parameterName);

		ILevelGeneratorParameters SetParameter<T>(string parameterName, T parameterValue);
	}
}