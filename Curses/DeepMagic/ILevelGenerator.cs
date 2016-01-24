namespace Deep.Magic
{
	public interface ILevelGenerator
	{
		ILevelGeneratorParameters Parameters { get; }
		ILevel Generate();
	}
}

namespace Deep.Magic
{
	public interface ILevelGeneratorParameters
	{
		T GetParameter<T>(string parameterName);
		ILevelGeneratorParameters SetParameter<T>(string parameterName, T parameterValue);
	}
}

namespace Deep.Magic
{
	public interface ILevel
	{
		Coordinate Size { get; }
		ITile TileAt(Coordinate coordinate);
	}
}

namespace Deep.Magic
{
	public interface ITile
	{
		char Appearance { get; }
		short Color { get; }
		bool HasTag(string tag);
		void AddTag(string tag);
		void RemoveTag(string tag);
	}
}