namespace Deep.Magic
{
	using System.Collections.Generic;

	public partial class DmConsole
	{
		private int width;
		private int height;
		private ScreenCharacter[,] buffer;
		private List<Delta> deltas;

		public DmConsole(int width, int height)
		{
			this.InitialiseScreen(width, height);

			this.deltas = new List<Delta>();
			this.InputHandle = Bindings.GetStdHandle(Deep.Magic.Constants.StdInput);
			this.OutputHandle = Bindings.GetStdHandle(Deep.Magic.Constants.StdOutput);
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

		public ScreenCharacter CharacterAt(int x, int y)
		{
			return this.buffer[x, y];
		}

		public void Clear(Deep.Magic.Color clearColor = Deep.Magic.Color.Black)
		{
			for (var x = 0; x < this.width; x++)
			{
				for (var y = 0; y < this.height; y++)
				{
					this.buffer[x, y] = new ScreenCharacter('\0', 0);
				}
			}

			uint charactersWritten;
			Bindings.FillConsoleOutputAttribute(
				this.OutputHandle,
				(ushort)clearColor,
				(uint)(this.width * this.height),
				new Bindings.Coord { X = 0, Y = 0 },
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

		public void Update()
		{
			foreach (var delta in this.deltas)
			{
				if (delta.Check(this.buffer))
				{
					uint charactersWritten;
					Bindings.WriteConsole(
						this.OutputHandle,
						delta.Text,
						(uint)delta.Text.Length,
						out charactersWritten,
						System.IntPtr.Zero);

					Bindings.FillConsoleOutputAttribute(
						this.OutputHandle,
						delta.Attributes,
						(uint)delta.Text.Length,
						new Bindings.Coord { X = this.Cursor.X, Y = this.Cursor.Y },
						out charactersWritten);

					delta.Write(this.buffer);
				}
			}

			this.deltas.Clear();
		}

		private void InitialiseScreen(int width, int height)
		{
			this.width = width;
			this.height = height;
			this.buffer = new ScreenCharacter[width, height]; 

			for (var x = 0; x < this.width; x++)
			{
				for (var y = 0; y < this.height; y++)
				{
					this.buffer[x, y] = new ScreenCharacter(' ', 0);
				}
			}
		}

		private void WriteAt(int x, int y, char character, ushort attributes)
		{
			Deep.Magic.Bindings.SetConsoleCursorPosition(
				this.OutputHandle,
				new Bindings.Coord { X = (short)x, Y = (short)y });

			uint trash;
			if (this.buffer[x, y].Character != character)
			{
				Deep.Magic.Bindings.WriteConsole(
					this.OutputHandle,
					string.Empty + character,
					1,
					out trash,
					System.IntPtr.Zero);
				this.buffer[x, y].Character = character;
			}

			if (this.buffer[x, y].Attributes != attributes)
			{
				Bindings.FillConsoleOutputAttribute(
					this.OutputHandle,
					attributes,
					1,
					new Bindings.Coord { X = this.Cursor.X, Y = this.Cursor.Y },
					out trash);
				this.buffer[x, y].Attributes = attributes;
			}
		}

		// Should this really be here, or in the namespace..?
		public class ScreenCharacter
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