﻿namespace Deep.Magic
{
	public interface ILevelGenerator
	{
		ILevel Generate(ILevelGeneratorParameters parameters);
	}
}

namespace Deep.Magic
{
	public interface ILevelGeneratorParameters
	{
		T GetParameter<T>(string parameterName);
		void SetParameter<T>(string parameterName, T parameterValue);
	}
}

namespace Deep.Magic
{
	public interface ILevel
	{
		ITile TileAt(Coordinate coordinate);
	}
}

namespace Deep.Magic
{
	public interface ITile
	{
		bool HasTag(string tag);
		void AddTag(string tag);
		void RemoveTag(string tag);
	}
}