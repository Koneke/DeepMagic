namespace Deep.Magic
{
	public enum Color
	{
		Black = 0x00,
		HalfBlue = 0x01,
		VeryBlue = 0x01 | 0x08,
		HalfGreen = 0x02,
		VeryGreen = 0x02 | 0x08,
		HalfRed = 0x04,
		VeryRed = 0x04 | 0x08,
		HalfTurquoise = 0x01 | 0x02,
		VeryTurquoise = 0x01 | 0x02 | 0x08,
		HalfPurple = 0x01 | 0x04,
		VeryPurple = 0x01 | 0x04 | 0x08,
		HalfBrown = 0x02 | 0x04,
		VeryBrown = 0x02 | 0x04 | 0x08,
		HalfWhite = 0x01 | 0x02 | 0x04,
		VeryWhite = 0x01 | 0x02 | 0x04 | 0x08
	}

	public static class ColorUtilities
	{
		private static System.Random random = new System.Random();

		public static ushort GetRandomColor(bool foreground, bool background)
		{
			ushort fg = (ushort)random.Next(0, 0xf);
			ushort bg = (ushort)(random.Next(0, 0xf) * 0x10);
			ushort result = 0;

			if (foreground)
			{
				result |= fg; 
			}

			if (background)
			{
				result |= bg; 
			}

			return result;
		}
	}
}