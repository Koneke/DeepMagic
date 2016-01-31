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
			if (!this.parameters.ContainsKey(typeof(T)))
			{
				throw new System.Exception("Non-existant parameter or incorrect type.");
			}

			var key = parameterName.ToLower();
			if (!this.parameters[typeof(T)].ContainsKey(key))
			{
				throw new System.Exception("Non-existant parameter or incorrect type.");
			}

			return (T)this.parameters[typeof(T)][key];
		}

		public ILevelGeneratorParameters SetParameter<T>(string parameterName, T parameterValue)
		{
			if (!this.parameters.ContainsKey(typeof(T)))
			{
				this.parameters.Add(typeof(T), new Dictionary<string, object>());
			}

			this.parameters[typeof(T)].Add(parameterName.ToLower(), parameterValue);

			return this;
		}
	}
}