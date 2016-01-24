namespace Deep.Magic.Implementations.Rogue
{
	using System.Collections.Generic;

	// Genericise this.
	public class RogueTile : ITile
	{
		char appearance;
		public char Appearance { get { return this.appearance; } }

		short color;
		public short Color { get { return this.color; } }

		private IList<string> tags;

		public RogueTile(char appearance, short color)
		{
			this.appearance = appearance;
			this.color = color;
			this.tags = new List<string>();
		}

		public void AddTag(string tag)
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
		}

		public bool HasTag(string tag)
		{
			return this.tags.Contains(tag.ToLower());
		}

		public void RemoveTag(string tag)
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
		}
	}

	public class RogueLevel : ILevel
	{
		public class Passage
		{
			public Coordinate Start;
			public Coordinate TurnStart;
			public Coordinate TurnEnd;
			public Coordinate End;
		}

		public class Room
		{
			public bool InGraph;
			public Coordinate Cell;
			public int Index { get { return Cell.Y * 3 + Cell.X; } }
			public Coordinate Position;
			public Coordinate Size;
			public List<Room> ConnectedTo;

			public Room()
			{
				this.ConnectedTo = new List<Room>();
			}

			public bool CanConnectTo(Room other)
			{
				// Only adjacent rooms can connect to eachother.
				return Coordinate.AbsoluteDistance(this.Cell, other.Cell) == 1;
			}
		}

		private Coordinate size;
		public Coordinate Size { get { return this.size; } }

		public List<Room> Rooms;
		public List<Passage> Passages;
		public ITile[,] Tiles;

		public RogueLevel(Coordinate levelSize)
		{
			this.size = levelSize;
			this.Tiles = new RogueTile[ levelSize.X, levelSize.Y ];
			this.Rooms = new List<Room>();
			this.Passages = new List<Passage>();
		}

		public ITile TileAt(Coordinate coordinate)
		{
			return this.Tiles[ coordinate.X, coordinate.Y ];
		}

		public void SetTile(Coordinate coordinate, RogueTile tile)
		{
			this.Tiles[ coordinate.X, coordinate.Y ] = tile;
		}
	}
}