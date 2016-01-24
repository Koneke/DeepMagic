namespace Deep.Magic
{
	interface ILevelGenerator
	{
		ILevel Generate(ILevelGeneratorParameters parameters);
	}
}

namespace Deep.Magic
{
	interface ILevelGeneratorParameters
	{
		T GetParameter<T>(string parameterName);
		void SetParameter<T>(string parameterName, T parameterValue);
	}
}

namespace Deep.Magic
{
	interface ILevel
	{
		ITile TileAt(Coordinate coordinate);
	}
}

namespace Deep.Magic
{
	interface ITile
	{
		bool HasTag(string tag);
		void AddTag(string tag);
		void RemoveTag(string tag);
	}
}