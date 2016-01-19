namespace Curses
{
	using System;

	class Program
	{
		static void Main(string[] args)
		{
			System.Console.CursorVisible = false;

			var console = new Console();

			console.Clear();

			console.Cursor.SetColor(
				Deep.Magic.Color.VeryWhite,
				Deep.Magic.Color.VeryBlue);
			console.Cursor.SetPosition(10, 10);
			console.Write("test");

			console.Cursor.SetColor(
				Deep.Magic.Color.VeryWhite,
				Deep.Magic.Color.VeryRed);
			console.Cursor.X = 20;
			console.Write("foobar");

			console.Cursor.SetColor(
				Deep.Magic.Color.Black,
				Deep.Magic.Color.VeryGreen);
			console.Cursor.Move(10, 0);
			console.Write("okay");

			System.Console.ReadKey();
		}
	}

	partial class Console
	{
		// Magic
		public IntPtr Handle;

		// Outwards facing
		public ConsoleCursor Cursor;

		public Console()
		{
			this.Handle = Deep.Magic.GetStdHandle(Deep.Magic.StdOutput);
			this.Cursor = new ConsoleCursor(this);
			this.Clear();
		}

		public void Clear(Deep.Magic.Color clearColor = Deep.Magic.Color.Black)
		{
			uint charactersWritten;
			Deep.Magic.FillConsoleOutputAttribute(
				this.Handle,
				(ushort)clearColor,
				80 * 25,
				new Deep.Magic.Coord{ X = 0, Y = 0 },
				out charactersWritten);
		}

		public void Write(string text)
		{
			uint charactersWritten;
			Deep.Magic.WriteConsole(
				this.Handle,
				text,
				(uint)text.Length,
				out charactersWritten,
				IntPtr.Zero);

			Deep.Magic.FillConsoleOutputAttribute(
				this.Handle,
				this.Cursor.Color,
				(uint)text.Length,
				new Deep.Magic.Coord{ X = this.Cursor.X, Y = this.Cursor.Y },
				out charactersWritten);
		}
	}

	partial class Console
	{
		public class ConsoleCursor
		{
			private readonly Console _console;

			private short _x;
			public short X
			{
				get
				{
					return this._x;
				}
				set
				{
					this.SetPosition(value, this._y);
				}
			}

			private short _y;
			public short Y
			{
				get
				{
					return this._y;
				}
				set
				{
					this.SetPosition(this._x, value);
				}
			}

			public ushort Color;

			public Deep.Magic.Color ForegroundColor
			{
				get
				{
					return (Deep.Magic.Color)(this.Color & 0x0f);
				}
				set
				{
					this.Color = (ushort)(((ushort)this.BackgroundColor * 0x10) | (ushort)value);
				}
			}

			public Deep.Magic.Color BackgroundColor
			{
				get
				{
					return (Deep.Magic.Color)((this.Color & 0xf0) / 0x10);
				}
				set
				{
					this.Color = (ushort)((ushort)this.ForegroundColor | ((ushort)value * 0x10));
				}
			}

			public ConsoleCursor(Console console)
			{
				this._console = console;
			}

			public void SetColor(Deep.Magic.Color foreground, Deep.Magic.Color background)
			{
				this.ForegroundColor = foreground;
				this.BackgroundColor = background;
			}

			public void SetPosition(short x, short y)
			{
				this._x = x;
				this._y = y;
				Deep.Magic.SetConsoleCursorPosition(
					this._console.Handle,
					new Deep.Magic.Coord { X = x, Y = y }
				);
			}

			public void Move(short x, short y)
			{
				this.SetPosition((short)(this._x + x), (short)(this._y + y));
			}
		}
	}
}