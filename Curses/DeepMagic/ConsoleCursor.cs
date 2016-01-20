
namespace Deep.Magic
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

		public Console SetColor(Deep.Magic.Color foreground, Deep.Magic.Color background)
		{
			this.ForegroundColor = foreground;
			this.BackgroundColor = background;
			return this._console;
		}

		// Just wrappers, for the sake of chaining.
		public Console SetForegroundColor(Deep.Magic.Color foreground)
		{
			this.ForegroundColor = foreground;
			return this._console;
		}

		public Console SetBackgroundColor(Deep.Magic.Color background)
		{
			this.BackgroundColor = background;
			return this._console;
		}

		public Console SetPosition(short x, short y)
		{
			this._x = x;
			this._y = y;
			Deep.Magic.Bindings.SetConsoleCursorPosition(
				this._console.OutputHandle,
				new Deep.Magic.Bindings.Coord { X = x, Y = y }
			);

			return this._console;
		}

		public Console Move(short x, short y)
		{
			this.SetPosition((short)(this._x + x), (short)(this._y + y));
			return this._console;
		}
	}
}