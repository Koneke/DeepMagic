namespace Deep.Magic.Implementations.Rogue
{
	using System.Collections.Generic;
	using System.Linq;

	public class RogueLevelGenerator : ILevelGenerator
	{
		public static class ParameterNames
		{
			public const string LevelWidth = "level-width";
			public const string LevelHeight = "level-height";
			public const string HorizontalCellCount = "horizontal-cell-count";
			public const string VerticalCellCount = "vertical-cell-count";
			public const string MaxRooms = "max-rooms";
		}

		private ILevelGeneratorParameters parameters;
		public ILevelGeneratorParameters Parameters { get { return this.parameters; } }

		public RogueLevelGenerator(ILevelGeneratorParameters parameters)
		{
			this.parameters = parameters;
		}

		public ILevel Generate()
		{
			var rogueLevel = new RogueLevel(new Coordinate(
				Parameters.GetParameter<int>(ParameterNames.LevelWidth),
				Parameters.GetParameter<int>(ParameterNames.LevelHeight)));

			this.DoRooms(rogueLevel);
			this.DoCorridors(rogueLevel);

			return rogueLevel;
		}

		void DoRooms(RogueLevel level)
		{
			int MaxRooms = Parameters.GetParameter<int>(ParameterNames.MaxRooms);
			int MaxWidth = level.Size.X / Parameters.GetParameter<int>(ParameterNames.HorizontalCellCount);
			int MaxHeight = level.Size.Y / Parameters.GetParameter<int>(ParameterNames.VerticalCellCount);

			for (var i = 0; i < MaxRooms; i++)
			{
				var topX = (i % 3) * MaxWidth + 1;
				int topY = (i / 3) * MaxHeight;

				var margin = 2; // Make sure there's some space between rooms.
				var roomWidth = Random.Next(4, MaxWidth - margin);
				var roomHeight = Random.Next(4, MaxHeight - margin);
				var positionX = topX + Random.Next(0, -margin + MaxWidth - roomWidth);
				var positionY = topY + Random.Next(0, -margin + MaxHeight - roomHeight);

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

				level.Rooms.Add(new RogueLevel.Room {
					InGraph = false,
					Cell = new Coordinate(i % 3, i / 3),
					Position = new Coordinate(positionX, positionY),
					Size = new Coordinate(roomWidth, roomHeight)
				});
			}

			foreach (var room in level.Rooms)
			{
				for (var x = 0; x < room.Size.X; x++)
				for (var y = 0; y < room.Size.Y; y++)
				{
					var tile = new RogueTile('.', 0x07);
					level.SetTile(room.Position + new Coordinate(x, y), tile);
				}
			}
		}

		private void DoCorridors(RogueLevel level)
		{
			RogueLevel.Room first = null;
			RogueLevel.Room second = null;

			var connect = new System.Action<RogueLevel.Room, RogueLevel.Room>((a, b) => {
				b.InGraph = true;
				a.ConnectedTo.Add(b);
				b.ConnectedTo.Add(a);
				this.Connect(level, a, b);
			});

			var j = 0;

			var notAlreadyConnected = new System.Func<RogueLevel.Room, bool>(
				target => !first.ConnectedTo.Contains(target));
			var notAlreadyInGraph = new System.Func<RogueLevel.Room, bool>(
				target => !target.InGraph);
			var firstCanConnect = new System.Func<RogueLevel.Room, bool>(
				target => first.CanConnectTo(target));
			var randomMagic = new System.Func<RogueLevel.Room, bool>(
				target => Random.Next(++j) == 0);

			var conditions = new List<System.Func<RogueLevel.Room, bool>>();

			var perform = new System.Action<int, bool>((count, randomFirst) => {
				int left = count;
				do
				{
					if (randomFirst)
					{
						first = level.Rooms.Shuffle()[ 0 ];
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
						do { first = level.Rooms.Shuffle()[ 0 ]; }
						while (!first.InGraph);
					}
					else
					{
						connect(first, second);
						left--;
					}
				} while (left > 0);
			});

			// The order matters, since we short circuit.
			// Always keep randomMagic at the end.
			conditions.Add(firstCanConnect);
			conditions.Add(notAlreadyInGraph);
			conditions.Add(randomMagic);

			first = level.Rooms.Shuffle()[ 0 ];
			first.InGraph = true;

			perform(parameters.GetParameter<int>(ParameterNames.MaxRooms) - 1, false);

			conditions.Clear();
			conditions.Add(firstCanConnect);
			conditions.Add(notAlreadyConnected);
			conditions.Add(randomMagic);

			var randomExtras = Random.Next(1, 4);

			perform(Random.Next(1, 4), true);
		}

		private void Connect(RogueLevel level, RogueLevel.Room first, RogueLevel.Room second)
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
					from.Position.Y + Random.Next(from.Size.Y - 2) + 1);
				endPoint = new Coordinate(
					to.Position.X,
					to.Position.Y + Random.Next(to.Size.Y - 2) + 1);

				delta = new Coordinate(1, 0);
				distance = System.Math.Abs(startPoint.X - endPoint.X) - 1;
				turnDelta = new Coordinate(0, startPoint.Y < endPoint.Y ? 1 : -1);
				turnLength = System.Math.Abs(startPoint.Y - endPoint.Y);
			}
			else // down
			{
				startPoint = new Coordinate(
					from.Position.X + Random.Next(from.Size.X - 2) + 1,
					from.Position.Y + from.Size.Y - 1);
				endPoint = new Coordinate(
					to.Position.X + Random.Next(to.Size.X - 2) + 1,
					to.Position.Y);

				delta = new Coordinate(0, 1);
				distance = System.Math.Abs(startPoint.Y - endPoint.Y) - 1;
				turnDelta = new Coordinate(startPoint.X < endPoint.X ? 1 : -1, 0);
				turnLength = System.Math.Abs(startPoint.X - endPoint.X);
			}

			turnPoint = Random.Next(1, distance - 1);

			var current = new Coordinate(startPoint);
			System.Action drawAtCurrent = () =>
				level.SetTile(current, new RogueTile('#', 0x02));

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

			level.SetTile(startPoint, new RogueTile('+', 0x07 | 0x08));
			level.SetTile(endPoint, new RogueTile('+', 0x07 | 0x08));
		}
	}
}