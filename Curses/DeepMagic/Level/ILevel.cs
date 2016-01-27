namespace Deep.Magic
{
	using System.Collections.Generic;

	public interface ILevel
	{
		Coordinate Size { get; }

		ITile TileAt(Coordinate coordinate);

		void SetTile(Coordinate coordinate, ITile tile);

		IList<ITile> TileList { get; set; }

		IList<Character> Characters { get; set; }
	}
}