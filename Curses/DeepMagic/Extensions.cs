namespace Deep.Magic
{
	using System;
	using System.Collections.Generic;

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
	}
}