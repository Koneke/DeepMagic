namespace Deep.Magic
{
	public class ConsoleCursor
	{
		private readonly DmConsole console;

		private short x;

		private short y;

		public ConsoleCursor(DmConsole console)
		{
			this.console = console;
		}

		public ushort Color { get; set; }

		public short X
		{
			get
			{
				return this.x;
			}

			set
			{
				this.SetPosition(value, this.y);
			}
		}

		public short Y
		{
			get
			{
				return this.y;
			}

			set
			{
				this.SetPosition(this.x, value);
			}
		}

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

		public DmConsole SetColor(ushort color)
		{
			this.Color = color;
			return this.console;
		}

		public DmConsole SetColor(Deep.Magic.Color foreground, Deep.Magic.Color background)
		{
			this.ForegroundColor = foreground;
			this.BackgroundColor = background;
			return this.console;
		}

		// Just wrappers, for the sake of chaining.
		public DmConsole SetForegroundColor(Deep.Magic.Color foreground)
		{
			this.ForegroundColor = foreground;
			return this.console;
		}

		public DmConsole SetBackgroundColor(Deep.Magic.Color background)
		{
			this.BackgroundColor = background;
			return this.console;
		}

		public DmConsole SetPosition(Coordinate coordinate)
		{
			return this.SetPosition((short)coordinate.X, (short)coordinate.Y);
		}

		public DmConsole SetPosition(short x, short y)
		{
			this.x = x;
			this.y = y;
			Deep.Magic.Bindings.SetConsoleCursorPosition(
				this.console.OutputHandle,
				new Deep.Magic.Bindings.Coord { X = x, Y = y });

			return this.console;
		}

		public DmConsole Move(short x, short y)
		{
			this.SetPosition((short)(this.x + x), (short)(this.y + y));
			return this.console;
		}
	}
}