namespace Deep.Magic
{
	public interface ILevelGenerator
	{
		ILevelGeneratorParameters Parameters { get; }

		ILevel Generate();
	}
}