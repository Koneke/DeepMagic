namespace Deep.Magic.Implementations.Rogue
{
	using System.Collections.Generic;
	using Deep.Magic;

	public class RogueRenderer : IGameRenderer
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

		public RogueRenderer(DmConsole console)
		{
			this.console = console;
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

			foreach (Character character in Level.Characters)
			{
				this.RenderCharacter(character);
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

		private void RenderCharacter(Character character)
		{
			this.console
				.Cursor.SetPosition(
					(short)character.Position.X,
					(short)character.Position.Y)
				.Cursor.SetColor(0x0f)
				.Write('@');
		}

		private TileType GetTileType(ITile tile)
		{
			switch (tile.Type)
			{
				case "floor":
					return TileType.Floor;
				case "wall":
					return TileType.Wall;
				case "passage":
					return TileType.Passage;
				case "door":
					return tile.HasTag("open")
						? TileType.OpenDoor
						: TileType.Door;
				default:
					throw new System.ArgumentException();
			}
		}
	}
}