namespace Deep.Magic
{
	using System.Security.Cryptography;

	public static class Random
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
			return Random.Next(maxValue - minValue) + minValue;
		}
	}
}