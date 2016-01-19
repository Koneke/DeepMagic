namespace Deep.Magic
{
	using System;

	public partial class Console
	{

		// Magic
		public IntPtr InputHandle;
		public IntPtr OutputHandle;

		// Outwards facing
		public ConsoleCursor Cursor;

		public Console()
		{
			this.InputHandle = Bindings.GetStdHandle(Deep.Magic.Constants.StdInput);
			this.OutputHandle = Bindings.GetStdHandle(Deep.Magic.Constants.StdOutput);
			this.Cursor = new ConsoleCursor(this);
			this.Clear();
		}

		public void Clear(Deep.Magic.Color clearColor = Deep.Magic.Color.Black)
		{
			uint charactersWritten;
			Bindings.FillConsoleOutputAttribute(
				this.OutputHandle,
				(ushort)clearColor,
				80 * 25,
				new Bindings.Coord{ X = 0, Y = 0 },
				out charactersWritten);
		}

		public Console Write(string text, bool MoveCursor = false)
		{
			uint charactersWritten;
			Bindings.WriteConsole(
				this.OutputHandle,
				text,
				(uint)text.Length,
				out charactersWritten,
				IntPtr.Zero);

			Bindings.FillConsoleOutputAttribute(
				this.OutputHandle,
				this.Cursor.Color,
				(uint)text.Length,
				new Bindings.Coord{ X = this.Cursor.X, Y = this.Cursor.Y },
				out charactersWritten);

			if (MoveCursor)
			{
				this.Cursor.Move((short)text.Length, 0);
			}

			return this;
		}

		public Console WriteAt(short x, short y, string text, bool moveCursor = false)
		{
			this.Cursor.SetPosition(x, y);
			this.Write(text, moveCursor);
			return this;
		}
	}
}