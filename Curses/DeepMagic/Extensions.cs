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
			// Wait, why are we locking this?
			lock (enumerable)
			{
				foreach (T element in enumerable)
				{
					action(element);
				}
			}
		}

		public static T SelectRandom<T>(
			this IList<T> source,
			bool shortCircuit,
			params Func<T, bool>[] conditions)
		{
			var result = new List<T>();

			foreach (T element in source)
			{
				bool failed = false;
				foreach (Func<T, bool> condition in conditions)
				{
					if (!condition(element))
					{
						failed = true;
						if (shortCircuit)
						{
							// Break so we don't evaluate the other conditions.
							break;
						}
					}
				}

				if (!failed)
				{
					result.Add(element);
				}
			}

			if (result.Count == 0)
			{
				throw new Exception("No valid element.");
			}

			return result.Shuffle()[0];
		}

		public static List<Tuple<T, int>> RunTimeLengthEncoding<T>(this IList<T> source) where T : IComparable
		{
			var result = new List<Tuple<T, int>>();
			var list = new List<T>(source);

			int length = 1;
			T current = list[0];
			for (var i = 1; i < list.Count; i++)
			{
				if (list[i].Equals(current))
				{
					length += 1;
				}
				else
				{
					result.Add(Tuple.Create(current, length));
					current = list[i];
					length = 1;
				}
			}

			result.Add(Tuple.Create(current, length));

			return result;
		}

		public static List<string> EzSplit(this string s, string separator, bool removeEmpty = true)
		{
			var stringSplitOptions = removeEmpty
				? StringSplitOptions.RemoveEmptyEntries
				: StringSplitOptions.None;

			var result = s.Split(
				new string[] { separator },
				stringSplitOptions);

			return result.ToList();
		}

		// http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp
		public static IList<T> Shuffle<T>(this IList<T> source)
		{
			List<T> list = new List<T>(source);
			var provider = new RNGCryptoServiceProvider();
			var n = list.Count;

			while (n > 1)
			{
				var box = new byte[1];

				do
				{
					provider.GetBytes(box);
				}
				while (!(box[0] < n * (byte.MaxValue / n)));

				int k = box[0] % n;
				n--;
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}

			return list;
		}
	}
}