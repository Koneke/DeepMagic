namespace Deep.Magic
{
	using System.Collections.Generic;

	public partial class Console
	{
		public static Queue<System.ConsoleKeyInfo> MessageQueue = new Queue<System.ConsoleKeyInfo>();

		// Notice that this is not actually any specific console, it's *the* console.
		// We currently only support one.
		public static bool PollInput(Console console)
		{
			lock(MessageQueue)
			{
				MessageQueue.Enqueue(System.Console.ReadKey(true));
			}

			// Keep going until cancelled.
			return true;
		}
	}
}