namespace Deep.Magic
{
	using System.Collections.Generic;
	using System.Linq;

	public class RogueLevel
	{
		private static Console console;
		private static System.Random random = new System.Random();

		public RogueLevel(Console console)
		{
			RogueLevel.console = console;
		}

		public class Room
		{
			public bool InGraph;
			public int CellX;
			public int CellY;
			public int Index { get { return CellY * 3 + CellX; } }
			public int X;
			public int Y;
			public int Width;
			public int Height;
			public List<Room> ConnectedTo;

			public Room()
			{
				this.ConnectedTo = new List<Room>();
			}

			public bool CanConnectTo(Room other)
			{
				// Only adjacent rooms can connect to eachother.
				return
					(System.Math.Abs(CellX - other.CellX) == 1 &&
					 System.Math.Abs(CellY - other.CellY) == 0) ||
					(System.Math.Abs(CellX - other.CellX) == 0 &&
					 System.Math.Abs(CellY - other.CellY) == 1);
			}
		}

		public static List<Room> DoRooms()
		{
			const int MaxRooms = 9;
			const int MaxWidth = 80 / 3;
			const int MaxHeight = 25 / 3;

			List<Room> rooms = new List<Room>();

			for (var i = 0; i < MaxRooms; i++)
			{
				var topX = (i % 3) * MaxWidth + 1;
				int topY = (i / 3) * MaxHeight;

				var roomWidth = RogueLevel.random.Next(4, MaxWidth - 2);
				var roomHeight = RogueLevel.random.Next(4, MaxHeight - 2);
				var positionX = topX + RogueLevel.random.Next(0, -2 + MaxWidth - roomWidth);
				var positionY = topY + RogueLevel.random.Next(0, -2 + MaxHeight - roomHeight);

				rooms.Add(new Room {
					InGraph = false,
					CellX = i % 3,
					CellY = i / 3,
					X = positionX,
					Y = positionY,
					Width = roomWidth,
					Height = roomHeight
				});

				for (var y = 0; y < roomHeight; y++)
				{
					var floor = string.Join("", System.Linq.Enumerable.Repeat('.', roomWidth));
					RogueLevel.console
						.Cursor.SetPosition((short)positionX, (short)(positionY + y))
						.Write(floor);
				}
			}

			return rooms;
		}

		public static void DoCorridors(List<Room> rooms)
		{
			const int MaxRooms = 9;
			Room first = null;
			Room second = null;

			first = rooms.Shuffle()[ 0 ];
			first.InGraph = true;
			var j = 0;
			var roomCount = 1;
			do
			{
				j = 0;
				foreach (var targetRoom in rooms.Where(first.CanConnectTo))
				{
					if (
						first.CanConnectTo(targetRoom) &&
						!targetRoom.InGraph &&
						RogueLevel.random.Next(++j) == 0
					) {
						second = targetRoom;
					}
				}

				// if we didn't find any room we could connect to at all, shuffle.
				if (j == 0)
				{
					do { first = rooms.Shuffle()[ 0 ]; }
					while (!first.InGraph);
				}
				else
				{
					second.InGraph = true;
					first.ConnectedTo.Add(second);
					second.ConnectedTo.Add(first);
					Connect(first, second);
					roomCount++;
				}
			} while (roomCount < MaxRooms);

			var randomExtras = RogueLevel.random.Next(1, 4);
			do
			{
				j = 0;
				foreach (var targetRoom in rooms.Where(first.CanConnectTo))
				{
					if (
						first.CanConnectTo(targetRoom) &&
						!first.ConnectedTo.Contains(targetRoom) &&
						RogueLevel.random.Next(++j) == 0
					) {
						second = targetRoom;
					}
				}

				// if we didn't find any room we could connect to at all, shuffle.
				if (j == 0)
				{
					do { first = rooms.Shuffle()[ 0 ]; }
					while (!first.InGraph);
				}
				else
				{
					second.InGraph = true;
					first.ConnectedTo.Add(second);
					second.ConnectedTo.Add(first);
					RogueLevel.Connect(first, second);
					randomExtras--;
				}
			} while (randomExtras > 0);
		}

		private static void Connect(Room first, Room second)
		{
			Coordinate startPoint, endPoint, delta, turnDelta;
			Room from, to;
			bool right;
			int distance;
			int turnPoint;
			int turnLength;

			from = first.Index < second.Index ? first : second;
			to = first.Index < second.Index ? second : first;
			right = from.Index + 1 == to.Index;

			//if (froms.Count > 1) return;

			if (right)
			{
				startPoint = new Coordinate(
					from.X + from.Width - 1,
					from.Y + RogueLevel.random.Next(from.Height - 2) + 1);
				endPoint = new Coordinate(
					to.X,
					to.Y + RogueLevel.random.Next(to.Height - 2) + 1);

				delta = new Coordinate(1, 0);
				distance = System.Math.Abs(startPoint.X - endPoint.X) - 1;
				turnDelta = new Coordinate(0, startPoint.Y < endPoint.Y ? 1 : -1);
				turnLength = System.Math.Abs(startPoint.Y - endPoint.Y);
			}
			else // down
			{
				startPoint = new Coordinate(
					from.X + RogueLevel.random.Next(from.Width - 2) + 1,
					from.Y + from.Height - 1);
				endPoint = new Coordinate(
					to.X + RogueLevel.random.Next(to.Width - 2) + 1,
					to.Y);

				delta = new Coordinate(0, 1);
				distance = System.Math.Abs(startPoint.Y - endPoint.Y) - 1;
				turnDelta = new Coordinate(startPoint.X < endPoint.X ? 1 : -1, 0);
				turnLength = System.Math.Abs(startPoint.X - endPoint.X);
			}

			turnPoint = RogueLevel.random.Next(1, distance - 2);

			var current = new Coordinate(startPoint);
			System.Action drawAtCurrent = () => RogueLevel.console
				.Cursor.SetPosition(current)
				.Cursor.SetColor(0x02)
				.Write('#');

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

			RogueLevel.console
				.Cursor.SetPosition((short)startPoint.X, (short)startPoint.Y)
				.Cursor.SetColor(0x07 | 0x08)
				.Write('+');
			RogueLevel.console
				.Cursor.SetPosition((short)endPoint.X, (short)endPoint.Y)
				.Cursor.SetColor(0x07 | 0x08)
				.Write('@');
		}
	}
}