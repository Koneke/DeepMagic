namespace Deep.Magic
{
	using System.Collections.Generic;

	public class AttributeCollection
	{
		private Dictionary<string, double> attributes;

		public AttributeCollection()
		{
			this.attributes = new Dictionary<string, double>();
		}

		public AttributeCollection SetAttribute(string name, double attributeValue)
		{
			name = name.ToLower();

			if (!this.attributes.ContainsKey(name))
			{
				this.attributes.Add(name, 0);
			}

			this.attributes[name] = attributeValue;

			return this;
		}

		public bool HasAttribute(string name)
		{
			return this.attributes.ContainsKey(name.ToLower());
		}

		public double GetAttribute(string name)
		{
			return this.attributes[name.ToLower()];
		}
	}
}