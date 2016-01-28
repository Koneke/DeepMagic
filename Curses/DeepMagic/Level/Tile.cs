namespace Deep.Magic
{
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Generic ITile implementation.
	/// </summary>
	public class Tile : ITile
	{
		private IList<string> tags;
		private Coordinate position;

		public Tile(string type, Coordinate position, bool solid = false)
		{
			this.Type = type;
			this.position = new Coordinate(position);
			this.tags = new List<string>();
			this.Solid = solid;
		}

		public string Type { get; set; }

		public bool Solid { get; set; }

		public Coordinate Position
		{
			get
			{
				return this.position;
			}
		}

		public Tile Clone()
		{
			var tile = new Tile(this.Type, this.position, this.Solid);
			tile.AddTags(this.tags.ToArray());
			return tile;
		}

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