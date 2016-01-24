namespace Deep.Magic
{
	using System.Collections.Generic;

	public class LevelGeneratorParameters : ILevelGeneratorParameters
	{
		private Dictionary<System.Type, Dictionary<string, object>> parameters;

		public LevelGeneratorParameters()
		{
			this.parameters = new Dictionary<System.Type, Dictionary<string, object>>();
		}

		public T GetParameter<T>(string parameterName)
		{
			if (!parameters.ContainsKey(typeof(T)))
			{
				throw new System.Exception("Non-existant parameter or incorrect type.");
			}

			var key = parameterName.ToLower();
			if (!parameters[typeof(T)].ContainsKey(key))
			{
				throw new System.Exception("Non-existant parameter or incorrect type.");
			}

			return (T)parameters[typeof(T)][key];
		}

		public ILevelGeneratorParameters SetParameter<T>(string parameterName, T parameterValue)
		{
			if (!parameters.ContainsKey(typeof(T)))
			{
				parameters.Add(typeof(T), new Dictionary<string, object>());
			}

			parameters[typeof(T)].Add(parameterName.ToLower(), parameterValue);

			return this;
		}
	}
}