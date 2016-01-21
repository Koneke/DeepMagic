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
			this.deltas.Add(new Delta(this.Cursor.X, this.Cursor.Y, text, this.Cursor.Color));

			if (MoveCursor)
			{
				this.Cursor.Move((short)text.Length, 0);
			}

			return this;
		}

		private bool CheckDelta(Delta change)
		{
			for (var x = 0; x < change.Text.Length; x++)
			{
				if (this.buffer[ change.X + x, change.Y ].Character != change.Text[ x ])
				{
					return true;
				}

				if (this.buffer[ change.X + x, change.Y ].Attributes != change.Attributes)
				{
					return true;
				}
			}

			return false;
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