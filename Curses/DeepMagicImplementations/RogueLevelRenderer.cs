namespace Deep.Magic.Implementations.Rogue
{
	using System.Collections.Generic;
	using Deep.Magic;

	public class RogueLevelRenderer : ILevelRenderer
	{
		private DmConsole console;

		private Dictionary<TileType, char> tileAppearances = new Dictionary<TileType, char>()
		{
			{ TileType.Floor,    '.' },
			{ TileType.Wall,     '#' },
			{ TileType.Passage,  '#' },
			{ TileType.Door,     '+' },
			{ TileType.OpenDoor, '/' },
		};

		private Dictionary<TileType, ushort> tileColors = new Dictionary<TileType, ushort>()
		{
			{ TileType.Floor,    0x07 },
			{ TileType.Wall,     0x77 },
			{ TileType.Passage,  0x07 },
			{ TileType.Door,     0x07 },
			{ TileType.OpenDoor, 0x07 },
		};

		public RogueLevelRenderer(DmConsole console, ILevel level)
		{
			this.console = console;
			this.Level = level;
		}

		private enum TileType
		{
			Floor,
			Wall,
			Passage,
			Door,
			OpenDoor
		}

		public ILevel Level { get; set; }

		public void Clear()
		{
			this.console.Clear();
		}

		public void RenderLevel()
		{
			for (var x = 0; x < this.Level.Size.X; x++)
			{
				for (var y = 0; y < this.Level.Size.Y; y++)
				{
					this.RenderTile(new Coordinate(x, y));
				}
			}
		}

		private void RenderTile(Coordinate tilePosition)
		{
			var tile = this.Level.TileAt(tilePosition);

			if (tile == null)
			{
				return;
			}

			var tileType = this.GetTileType(tile);

			this.console
				.Cursor.SetPosition((short)tilePosition.X, (short)tilePosition.Y)
				.Cursor.SetColor(this.tileColors[tileType])
				.Write(this.tileAppearances[tileType]);
		}

		private TileType GetTileType(ITile tile)
		{
			if (tile.HasTag("floor"))
			{
				return TileType.Floor;
			}
			else if (tile.HasTag("wall"))
			{
				return TileType.Wall;
			}
			else if (tile.HasTag("passage"))
			{
				return TileType.Passage;
			}
			else if (tile.HasTag("door"))
			{
				return tile.HasTag("open")
					? TileType.OpenDoor
					: TileType.Door;
			}

			throw new System.ArgumentException();
		}
	}
}