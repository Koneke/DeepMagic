namespace Deep.Magic
{
	using System.Collections.Generic;

	public partial class Console
	{
		public class ScreenCharacter
		{
			public char Character;
			public ushort Attributes;

			public ScreenCharacter(char character, ushort attributes)
			{
				this.Character = character;
				this.Attributes = attributes;
			}
		}

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

		private int width;
		private int height;
		private ScreenCharacter[,] buffer;
		private List<Delta> deltas;

		// Magic
		public System.IntPtr InputHandle;
		public System.IntPtr OutputHandle;

		// Outwards facing
		public ConsoleCursor Cursor;

		public Console(int width, int height)
		{
			this.InitialiseScreen(width, height);

			this.deltas = new List<Delta>();
			this.InputHandle = Bindings.GetStdHandle(Deep.Magic.Constants.StdInput);
			this.OutputHandle = Bindings.GetStdHandle(Deep.Magic.Constants.StdOutput);
			this.Cursor = new ConsoleCursor(this);
			this.Clear();
		}

		public ScreenCharacter CharacterAt(int x, int y)
		{
			return buffer[ x, y ];
		}

		private void InitialiseScreen(int width, int height)
		{
			this.width = width;
			this.height = height;
			this.buffer = new ScreenCharacter[ width, height ]; 

			for (var x = 0; x < this.width; x++)
			{
				for (var y = 0; y < this.height; y++)
				{
					this.buffer[ x, y ] = new ScreenCharacter(' ', 0);
				}
			}
		}

		public void Clear(Deep.Magic.Color clearColor = Deep.Magic.Color.Black)
		{
			uint charactersWritten;
			Bindings.FillConsoleOutputAttribute(
				this.OutputHandle,
				(ushort)clearColor,
				80 * 25,
				new Bindings.Coord { X = 0, Y = 0 },
				out charactersWritten);
		}

		public Console Write(string text, bool MoveCursor = false)
		{
			//this.deltas.Add(new Delta(this.Cursor.X, this.Cursor.Y, text, this.Cursor.Color));

			for (int x = 0; x < text.Length; x++)
			{
				this.WriteAt(this.Cursor.X + x, this.Cursor.Y, text[ x ], this.Cursor.Color);
			}

			if (MoveCursor)
			{
				this.Cursor.Move((short)text.Length, 0);
			}

			return this;
		}

		public Console Write(char character, bool MoveCursor = false)
		{
			this.WriteAt(this.Cursor.X, this.Cursor.Y, character, this.Cursor.Color);

			if (MoveCursor)
			{
				this.Cursor.Move(1, 0);
			}
			return this;
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
					"" + character,
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

		public void Update()
		{
			foreach (var delta in deltas)
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

			deltas.Clear();
		}
	}
}