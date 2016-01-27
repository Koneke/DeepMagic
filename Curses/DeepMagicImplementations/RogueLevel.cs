namespace Deep.Magic.Implementations.Rogue
{
	using System.Collections.Generic;

	// Genericise this.
	public class RogueLevel : ILevel
	{
		public RogueLevel(Coordinate levelSize)
		{
			this.Size = levelSize;
			this.Tiles = new ITile[levelSize.X, levelSize.Y];
			this.TileList = new List<ITile>();
			this.Rooms = new List<Room>();
			this.Passages = new List<Passage>();
			this.Characters = new List<Character>();
		}

		public Coordinate Size { get; private set; }

		public List<Room> Rooms { get; private set; }

		public List<Passage> Passages { get; private set; }

		public IList<ITile> TileList { get; set; }

		public ITile[,] Tiles { get; private set; }

		public IList<Character> Characters { get; set; }

		public ITile TileAt(Coordinate coordinate)
		{
			return this.Tiles[coordinate.X, coordinate.Y];
		}

		public void SetTile(Coordinate coordinate, ITile tile)
		{
			var current = this.Tiles[coordinate.X, coordinate.Y];

			if (current != null)
			{
				this.TileList.Remove(current);
			}

			if (tile != null)
			{
				this.TileList.Add(tile);
			}

			this.Tiles[coordinate.X, coordinate.Y] = tile;
		}

		public class Passage
		{
			public Coordinate Start { get; private set; }

			public Coordinate TurnStart { get; private set; }

			public Coordinate TurnEnd { get; private set; }

			public Coordinate End { get; private set; }
		}

		public class Room
		{
			public Room()
			{
				this.ConnectedTo = new List<Room>();
			}

			public bool InGraph { get; set; }

			public Coordinate Cell { get; set; }

			public Coordinate Position { get; set; }

			public Coordinate Size { get; set; }

			public List<Room> ConnectedTo { get; set; }

			public int Index
			{
				get
				{
					return (this.Cell.Y * 3) + this.Cell.X;
				}
			}

			public bool CanConnectTo(Room other)
			{
				// Only adjacent rooms can connect to eachother.
				return Coordinate.AbsoluteDistance(this.Cell, other.Cell) == 1;
			}
		}
	}
}