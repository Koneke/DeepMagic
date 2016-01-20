namespace Deep.Magic
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Cryptography;

	public static class Extensions
	{
		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			lock(enumerable)
			{
				foreach(T element in enumerable)
				{
					action(element);
				}
			}
		}

		public static List<string> EzSplit(this string s, string separator, bool removeEmpty = true)
		{
			return s.Split(
				new string[] { separator },
				removeEmpty
					? StringSplitOptions.RemoveEmptyEntries
					: StringSplitOptions.None).ToList();
		}

		// http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp
		public static void Shuffle<T>(this IList<T> list)
		{
			var provider = new RNGCryptoServiceProvider();
			var n = list.Count;

			while (n > 1)
			{
				var box = new byte[1];

				do { provider.GetBytes(box); }
				while (!(box[0] < n * (byte.MaxValue / n)));

				int k = (box[0] % n);
				n--;
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}
	}
}