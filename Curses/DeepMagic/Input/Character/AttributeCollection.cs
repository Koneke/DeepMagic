namespace Deep.Magic
{
	using System.Collections.Generic;

	public class AttributeCollection
	{
		private Dictionary<string, double> attributes;
		private AttributeCollection parent;

		public AttributeCollection(AttributeCollection parent = null)
		{
			this.attributes = new Dictionary<string, double>();
			this.parent = parent;
		}

		public AttributeCollection SetAttribute(string attribute, double attributeValue)
		{
			attribute = attribute.ToLower();

			if (!this.attributes.ContainsKey(attribute))
			{
				this.attributes.Add(attribute, 0);
			}

			this.attributes[attribute] = attributeValue;

			return this;
		}

		public AttributeCollection ModifyAttribute(string attribute, double delta)
		{
			attribute = attribute.ToLower();

			this.attributes[attribute] += delta;

			return this;
		}

		public bool HasAttribute(string attribute)
		{
			return this.HasParentAttribute(attribute) || this.attributes.ContainsKey(attribute.ToLower());
		}

		private bool HasParentAttribute(string attribute)
		{
			return parent != null && parent.HasAttribute(attribute);
		}

		public double GetAttribute(string attribute, double? defaultValue = null)
		{
			attribute = attribute.ToLower();
			double? attributeValue = null;

			if (this.attributes.ContainsKey(attribute))
			{
				attributeValue = (attributeValue ?? 0) + this.attributes[attribute];
			}

			if (this.HasParentAttribute(attribute))
			{
				attributeValue = (attributeValue ?? 0) + this.parent.GetAttribute(attribute);
			}

			attributeValue = attributeValue ?? defaultValue;

			if (!attributeValue.HasValue)
			{
				throw new System.Exception();
			}

			return attributeValue.Value;
		}
	}
}