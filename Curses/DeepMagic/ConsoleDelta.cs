namespace Deep.Magic
{
	public partial class Console
	{
		public class Delta
		{
			public int X;
			public int Y;
			public string Text;
			public ushort Attributes;

			public Delta(int x, int y, string text, ushort attributes)
			{
				this.X = x;
				this.Y = y;
				this.Text = text;
				this.Attributes = attributes;
			}

			public void Write(ScreenCharacter[,] buffer)
			{
				for (var x = 0; x < this.Text.Length; x++)
				{
					buffer[ this.X + x, this.Y ].Character = this.Text[ x ];
					buffer[ this.X + x, this.Y ].Attributes = this.Attributes;
				}
			}

			public bool Check(ScreenCharacter[,] buffer)
			{
				for (var x = 0; x < this.Text.Length; x++)
				{
					if (buffer[ this.X + x, this.Y ].Character != this.Text[ x ])
					{
						return true;
					}

					if (buffer[ this.X + x, this.Y ].Attributes != this.Attributes)
					{
						return true;
					}
				}

				return false;
			}
		}
	}
}