namespace Deep.Magic.Implementations.Rogue
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public partial class RogueLevelGenerator : ILevelGenerator
	{
		private ILevelGeneratorParameters parameters;

		public RogueLevelGenerator(ILevelGeneratorParameters parameters)
		{
			this.parameters = parameters;
		}

		private delegate bool RoomCondition(RogueLevel.Room room);

		public ILevelGeneratorParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		public ILevel Generate()
		{
			var rogueLevel = new RogueLevel(new Coordinate(
				this.Parameters.GetParameter<int>(ParameterNames.LevelWidth),
				this.Parameters.GetParameter<int>(ParameterNames.LevelHeight)));

			this.DoRooms(rogueLevel);
			this.DoCorridors(rogueLevel);
			this.GenerateStairs(rogueLevel);

			return rogueLevel;
		}

		private void DoRooms(RogueLevel level)
		{
			int maxRooms = this.Parameters.GetParameter<int>(ParameterNames.MaxRooms);
			int horizontalCellCount = this.Parameters.GetParameter<int>(ParameterNames.HorizontalCellCount);
			int verticalCellCount = this.Parameters.GetParameter<int>(ParameterNames.VerticalCellCount);

			int maxWidth = level.Size.X / horizontalCellCount;
			int maxHeight = level.Size.Y / verticalCellCount;

			for (var i = 0; i < maxRooms; i++)
			{
				var topX = ((i % 3) * maxWidth) + 1;
				int topY = (i / 3) * maxHeight;

				var margin = 2; // Make sure there's some space between rooms.
				var roomWidth = DmRandom.Next(4, maxWidth - margin);
				var roomHeight = DmRandom.Next(4, maxHeight - margin);
				var positionX = topX + DmRandom.Next(0, -margin + maxWidth - roomWidth);
				var positionY = topY + DmRandom.Next(0, -margin + maxHeight - roomHeight);

				if (positionX < 1)
				{
					roomWidth--;
					positionX = 1;
				}

				if (positionY < 1)
				{
					roomHeight--;
					positionY = 1;
				}

				level.Rooms.Add(new RogueLevel.Room
				{
					InGraph = false,
					Cell = new Coordinate(i % 3, i / 3),
					Position = new Coordinate(positionX, positionY),
					Size = new Coordinate(roomWidth, roomHeight)
				});
			}

			this.DrawRooms(level);
		}

		private void DrawRooms(RogueLevel level)
		{
			foreach (var room in level.Rooms)
			{
				for (var x = 0; x < room.Size.X; x++)
				{
					for (var y = 0; y < room.Size.Y; y++)
					{
						ITile tile;
						Coordinate coordinate = new Coordinate(
							room.Position.X + x,
							room.Position.Y + y);

						if (x == 0 || y == 0 || x == room.Size.X - 1 || y == room.Size.Y - 1)
						{
							tile = new Tile("wall", coordinate, true).AddTag("visionblocker");
						}
						else
						{
							tile = new Tile("floor", coordinate);
						}

						level.SetTile(coordinate, tile);
					}
				}
			}
		}

		private void Connect(RogueLevel level, RogueLevel.Room a, RogueLevel.Room b)
		{
			b.InGraph = true;
			a.ConnectedTo.Add(b);
			b.ConnectedTo.Add(a);
			this.DrawCorridor(level, a, b);
		}

		private void DoCorridors(RogueLevel level)
		{
			RogueLevel.Room first = null;
			RogueLevel.Room second = null;

			var j = 0;

			var notAlreadyConnected = new RoomCondition(target => !first.ConnectedTo.Contains(target));
			var notAlreadyInGraph   = new RoomCondition(target => !target.InGraph);
			var firstCanConnect     = new RoomCondition(target => first.CanConnectTo(target));
			var randomMagic         = new RoomCondition(target => DmRandom.Next(++j) == 0);

			var conditions = new List<RoomCondition>();

			var perform = new Action<int, bool>((count, randomFirst) =>
			{
				int left = count;
				do
				{
					if (randomFirst)
					{
						first = level.Rooms.Shuffle()[0];
					}

					j = 0;
					foreach (var targetRoom in level.Rooms.Where(first.CanConnectTo))
					{
						try
						{
							second = level.Rooms.SelectRandom(
								shortCircuit: true,
								conditions: conditions.ToArray());
						}
						catch (System.Exception)
						{
							// No valid element, so just go on.
						}
					}

					// if we didn't find any room we could connect to at all, shuffle.
					if (j == 0)
					{
						do
						{
							first = level.Rooms.Shuffle()[0];
						}
						while (!first.InGraph);
					}
					else
					{
						Connect(level, first, second);
						left--;
					}
				}
				while (left > 0);
			});

			// The order matters, since we short circuit.
			// Always keep randomMagic at the end.
			conditions.Add(firstCanConnect);
			conditions.Add(notAlreadyInGraph);
			conditions.Add(randomMagic);

			first = level.Rooms.Shuffle()[0];
			first.InGraph = true;

			perform(this.parameters.GetParameter<int>(ParameterNames.MaxRooms) - 1, false);

			conditions.Clear();
			conditions.Add(firstCanConnect);
			conditions.Add(notAlreadyConnected);
			conditions.Add(randomMagic);

			var randomExtras = DmRandom.Next(1, 4);

			perform(DmRandom.Next(1, 4), true);
		}

		private void DrawCorridor(RogueLevel level, RogueLevel.Room first, RogueLevel.Room second)
		{
			Coordinate startPoint, endPoint, delta, turnDelta;
			RogueLevel.Room from, to;
			bool right;
			int distance;
			int turnPoint;
			int turnLength;

			from = first.Index < second.Index ? first : second;
			to = first.Index < second.Index ? second : first;
			right = from.Index + 1 == to.Index;

			if (right)
			{
				startPoint = new Coordinate(
					from.Position.X + from.Size.X - 1,
					from.Position.Y + DmRandom.Next(from.Size.Y - 2) + 1);
				endPoint = new Coordinate(
					to.Position.X,
					to.Position.Y + DmRandom.Next(to.Size.Y - 2) + 1);

				delta = new Coordinate(1, 0);
				distance = System.Math.Abs(startPoint.X - endPoint.X) - 1;
				turnDelta = new Coordinate(0, startPoint.Y < endPoint.Y ? 1 : -1);
				turnLength = System.Math.Abs(startPoint.Y - endPoint.Y);
			}
			else
			{
				// downward corridor
				startPoint = new Coordinate(
					from.Position.X + DmRandom.Next(from.Size.X - 2) + 1,
					from.Position.Y + from.Size.Y - 1);
				endPoint = new Coordinate(
					to.Position.X + DmRandom.Next(to.Size.X - 2) + 1,
					to.Position.Y);

				delta = new Coordinate(0, 1);
				distance = System.Math.Abs(startPoint.Y - endPoint.Y) - 1;
				turnDelta = new Coordinate(startPoint.X < endPoint.X ? 1 : -1, 0);
				turnLength = System.Math.Abs(startPoint.X - endPoint.X);
			}

			turnPoint = DmRandom.Next(1, distance - 1);

			var current = new Coordinate(startPoint);
			System.Action drawAtCurrent = () =>
				level.SetTile(current, new Tile("passage", current));

			while (distance > 0)
			{
				current += delta;

				if (distance == turnPoint)
				{
					while (turnLength-- > 0)
					{
						drawAtCurrent();
						current += turnDelta;
					}
				}

				drawAtCurrent();
				distance--;
			}

			var startDoor = new Tile("door", startPoint);
			startDoor.AddTag(DmRandom.Next(2) == 0
				? "open"
				: "closed");

			level.SetTile(startPoint, startDoor);

			var endDoor = new Tile("door", endPoint);
			endDoor.AddTag(DmRandom.Next(2) == 0
				? "open"
				: "closed");

			level.SetTile(
				endPoint,
				endDoor);
		}

		private void GenerateStairs(RogueLevel level)
		{
			var stairsTile = level.TileList.SelectRandom(
				true,
				t => t.Type == "floor");
			stairsTile.Type = "stairs";
		}
	}
}