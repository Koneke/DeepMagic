namespace Deep.Magic
{
	// Should this just be a static class?
	public partial class DmConsole
	{
		private int width;
		private int height;
		private ScreenCharacter[,] buffer;
		private ScreenCharacter[,] flipBuffer;

		public DmConsole(int width, int height)
		{
			this.InitialiseScreen(width, height);

			this.InputHandle = NativeMethods.GetStdHandle(Deep.Magic.Constants.StdInput);
			this.OutputHandle = NativeMethods.GetStdHandle(Deep.Magic.Constants.StdOutput);
			this.Cursor = new ConsoleCursor(this);
			this.Clear();
		}

		// Outwards facing
		public ConsoleCursor Cursor { get; private set; }

		// Magic
		public System.IntPtr InputHandle { get; private set; }

		public System.IntPtr OutputHandle { get; private set; }

		public bool IsCursorVisible
		{
			get
			{
				return System.Console.CursorVisible;
			}

			set
			{
				System.Console.CursorVisible = value;
			}
		}

		public void Clear(Color clearColor = Deep.Magic.Color.Black)
		{
			for (var x = 0; x < this.width; x++)
			{
				for (var y = 0; y < this.height; y++)
				{
					this.buffer[x, y] = new ScreenCharacter('\0', 0);
				}
			}

			System.Array.Copy(this.buffer, this.flipBuffer, this.buffer.Length);

			uint charactersWritten;
			NativeMethods.FillConsoleOutputCharacter(
				this.OutputHandle,
				' ',
				(uint)(this.width * this.height),
				new NativeMethods.Coord { X = 0, Y = 0 },
				out charactersWritten);

			NativeMethods.FillConsoleOutputAttribute(
				this.OutputHandle,
				(ushort)clearColor,
				(uint)(this.width * this.height),
				new NativeMethods.Coord { X = 0, Y = 0 },
				out charactersWritten);
		}

		public DmConsole Write(string text, bool moveCursor = false)
		{
			for (int x = 0; x < text.Length; x++)
			{
				this.WriteAt(this.Cursor.X + x, this.Cursor.Y, text[x], this.Cursor.Color);
			}

			if (moveCursor)
			{
				this.Cursor.Move((short)text.Length, 0);
			}

			return this;
		}

		public DmConsole Write(char character, bool moveCursor = false)
		{
			this.WriteAt(this.Cursor.X, this.Cursor.Y, character, this.Cursor.Color);

			if (moveCursor)
			{
				this.Cursor.Move(1, 0);
			}

			return this;
		}

		public void Flush()
		{
			uint trash;
			ScreenCharacter b;
			ScreenCharacter fb;

			for (var x = 0; x < this.width; x++)
			{
				for (var y = 0; y < this.height; y++)
				{
					b = this.buffer[x, y];
					fb = this.flipBuffer[x, y];

					if (b.Character != fb.Character || b.Attributes != fb.Attributes)
					{
						Deep.Magic.NativeMethods.SetConsoleCursorPosition(
							this.OutputHandle,
							new NativeMethods.Coord { X = (short)x, Y = (short)y });
					}

					if (b.Character != fb.Character)
					{
						Deep.Magic.NativeMethods.WriteConsole(
							this.OutputHandle,
							string.Empty + fb.Character,
							1,
							out trash,
							System.IntPtr.Zero);
					}

					if (b.Attributes != fb.Attributes)
					{
						NativeMethods.FillConsoleOutputAttribute(
							this.OutputHandle,
							fb.Attributes,
							1,
							new NativeMethods.Coord { X = (short)x, Y = (short)y },
							out trash);
					}
				}
			}

			System.Array.Copy(this.flipBuffer, this.buffer, this.flipBuffer.Length);
		}

		private void InitialiseScreen(int width, int height)
		{
			this.width = width;
			this.height = height;
			this.buffer = new ScreenCharacter[width, height]; 
			this.flipBuffer = new ScreenCharacter[width, height]; 

			for (var x = 0; x < this.width; x++)
			{
				for (var y = 0; y < this.height; y++)
				{
					this.buffer[x, y] = new ScreenCharacter(' ', 0);
				}
			}

			System.Array.Copy(this.buffer, this.flipBuffer, this.buffer.Length);
		}

		private void WriteAt(int x, int y, char character, ushort attributes)
		{
			this.flipBuffer[x, y].Character = character;
			this.flipBuffer[x, y].Attributes = attributes;
		}

		// Should this really be here, or in the namespace..?
		private struct ScreenCharacter
		{
			public ScreenCharacter(char character, ushort attributes)
			{
				this.Character = character;
				this.Attributes = attributes;
			}

			public char Character { get; set; }

			public ushort Attributes { get; set; }
		}
	}
}