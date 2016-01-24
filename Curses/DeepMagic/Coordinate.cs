namespace Deep.Magic
{
	public class Coordinate
	{
		public int X;
		public int Y;

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

		public override string ToString()
		{
			return string.Format("{0}, {1}", this.X, this.Y);
		}

		public static Coordinate operator +(Coordinate a, Coordinate b)
		{
			return new Coordinate(a.X + b.X, a.Y + b.Y);
		}

		public static Coordinate operator -(Coordinate a, Coordinate b)
		{
			return new Coordinate(a.X - b.X, a.Y - b.Y);
		}

		public static int AbsoluteDistance(Coordinate a, Coordinate b)
		{
			var difference = a - b;
			return System.Math.Abs(difference.X) + System.Math.Abs(difference.Y);
		}
	}
}