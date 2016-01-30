namespace Deep.Magic
{
	using System.Security.Cryptography;

	public static class DmRandom
	{
		private static RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

		public static int Next(int maxValue)
		{
			var box = new byte[1];
			provider.GetBytes(box);
			return box[0] % maxValue;
		}

		public static int Next(int minValue, int maxValue)
		{
			return DmRandom.Next(maxValue - minValue) + minValue;
		}
	}
}