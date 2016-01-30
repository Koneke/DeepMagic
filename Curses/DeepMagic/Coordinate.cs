namespace Deep.Magic
{
	public struct Coordinate
	{
		public static Coordinate Zero = new Coordinate(0, 0);
		public static Coordinate North = new Coordinate(0, -1);
		public static Coordinate NorthEast = new Coordinate(1, -1);
		public static Coordinate East = new Coordinate(1, 0);
		public static Coordinate SouthEast = new Coordinate(1, 1);
		public static Coordinate South = new Coordinate(0, 1);
		public static Coordinate SouthWest = new Coordinate(-1, 1);
		public static Coordinate West = new Coordinate(-1, 0);
		public static Coordinate NorthWest = new Coordinate(-1, -1);

		public Coordinate(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		public Coordinate(Coordinate coordinate)
		{
			this.X = coordinate.X;
			this.Y = coordinate.Y;
		}

		public int X { get; private set; }

		public int Y { get; private set; }

		public static bool operator ==(Coordinate a, Coordinate b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Coordinate a, Coordinate b)
		{
			return !a.Equals(b);
		}

		public static Coordinate operator +(Coordinate a, Coordinate b)
		{
			return new Coordinate(a.X + b.X, a.Y + b.Y);
		}

		public static Coordinate operator -(Coordinate a, Coordinate b)
		{
			return new Coordinate(a.X - b.X, a.Y - b.Y);
		}

		public static Coordinate FromNumpad(char numpadNumber)
		{
			int x;
			int y;

			switch (numpadNumber)
			{
				case '1':
					x = -1;
					y = 1;
					break;
				case '2':
					x = 0;
					y = 1;
					break;
				case '3':
					x = 1;
					y = 1;
					break;
				case '4':
					x = -1;
					y = 0;
					break;
				case '5':
					x = 1;
					y = 0;
					break;
				case '6':
					x = 1;
					y = 0;
					break;
				case '7':
					x = -1;
					y = -1;
					break;
				case '8':
					x = 0;
					y = -1;
					break;
				case '9':
					x = 1;
					y = -1;
					break;
				default:
					throw new System.ArgumentException();
			}

			return new Coordinate(x, y);
		}

		public static int AbsoluteDistance(Coordinate a, Coordinate b)
		{
			var difference = a - b;
			return System.Math.Abs(difference.X) + System.Math.Abs(difference.Y);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Coordinate))
			{
				return false;
			}

			var other = (Coordinate)obj;

			return this.X == other.X && this.Y == other.Y;
		}

		public override string ToString()
		{
			return string.Format("{0}, {1}", this.X, this.Y);
		}
	}
}