namespace Deep.Magic
{
	using System.Collections.Generic;

	public class Tile : ITile
	{
		private IList<string> tags;

		public Tile(char appearance, short color)
		{
			this.Appearance = appearance;
			this.Color = color;
			this.tags = new List<string>();
		}

		// This should't really be here, instead, a levelrenderer should
		// decide on looks and stuff based on tags, I guess?
		public char Appearance { get; private set; }

		public short Color { get; private set; }

		public bool HasTag(string tag)
		{
			return this.tags.Contains(tag.ToLower());
		}

		public ITile AddTag(string tag)
		{
			tag = tag.ToLower();
			if (!this.HasTag(tag))
			{
				this.tags.Add(tag);
			}
			else
			{
				throw new System.Exception("Duplicate tag.");
			}

			return this;
		}

		public ITile AddTags(params string[] tags)
		{
			foreach (string tag in tags)
			{
				this.AddTag(tag);
			}

			return this;
		}

		public ITile RemoveTag(string tag)
		{
			tag = tag.ToLower();
			if (this.HasTag(tag))
			{
				this.tags.Remove(tag);
			}
			else
			{
				throw new System.Exception("Trying to remove non-existant tag.");
			}

			return this;
		}

		public ITile RemoveTags(params string[] tags)
		{
			foreach (string tag in tags)
			{
				this.RemoveTag(tag);
			}

			return this;
		}
	}
}