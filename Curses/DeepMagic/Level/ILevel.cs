namespace Deep.Magic
{
	public interface ILevel
	{
		Coordinate Size { get; }

		ITile TileAt(Coordinate coordinate);
	}
}