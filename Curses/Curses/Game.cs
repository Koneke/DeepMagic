namespace Deep.Magic
{
	using System.Collections.Generic;

	class Level
	{
		public IList<Character> Characters;
	}

	class Game
	{
		private System.Random random;
		private Console console;
		private bool run;
		private Character playerCharacter;
		private Level currentLevel;

		public void Run()
		{
			this.run = true;
			this.Initialise();

			while(this.run)
			{
				ConsoleKey.PollInput(this.console);

				this.Update();
				this.console.Update();

				// Prepare the input queue for the next frame.
				ConsoleKey.Clear();
			}
		}

		protected virtual void Initialise()
		{
			this.console = new Console(80, 25)
			{
				IsCursorVisible = false
			};
			this.random = new System.Random();
			this.playerCharacter = new Character();
			this.currentLevel = new Level();

			this.DoRooms();
		}

		protected virtual void Update()
		{
			if (ConsoleKey.Pressed("d"))
			{
				this.console
					.Cursor.SetPosition((short)this.random.Next(0, 77), (short)this.random.Next(0, 24))
					.Cursor.SetColor(ColorUtilities.GetRandomColor(true, true))
					.Write("hi!");
			}

			if (ConsoleKey.Pressed("s-q"))
			{
				this.run = false;
			}
		}

		private class Room
		{
			public bool InGraph;
			public int CellX;
			public int CellY;
			public int X;
			public int Y;
			public int Width;
			public int Height;
			public List<Room> ConnectedTo;

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

		private void DoRooms()
		{
			const int MaxRooms = 9;
			const int MaxWidth = 80 / 3;
			const int MaxHeight = 25 / 3;

			List<Room> rooms = new List<Room>();

			for (var i = 0; i < MaxRooms; i++)
			{
				var topX = (i % 3) * MaxWidth + 1;
				int topY = (i / 3) * MaxHeight;

				var roomWidth = this.random.Next(4, MaxWidth);
				var roomHeight = this.random.Next(4, MaxHeight);
				var positionX = topX + this.random.Next(0, MaxWidth - roomWidth);
				var positionY = topY + this.random.Next(0, MaxHeight - roomHeight);

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
					this.console
						.Cursor.SetPosition((short)positionX, (short)(positionY + y))
						.Write(floor);
				}
			}
		}

		private void DoCorridors(List<Room> rooms)
		{
			const int MaxRooms = 0;
			Room first = null;
			Room second = null;

			first = rooms.Shuffle()[ 0 ];
			first.InGraph = true;
			var j = 0;
			for (var i = 0; i < MaxRooms; i++)
			{
				if (
					rooms[ i ].CanConnectTo(first) &&
					!rooms[ i ].InGraph &&
					this.random.Next(++j) == 0
				) {
					second = rooms[ i ];
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
				}
			}
		}

		private void Connect(Room first, Room second)
		{
		}
	}
}