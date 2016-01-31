namespace Deep.Magic
{
	public interface ITile
	{
		string Type { get; set; }

		bool Solid { get; set; }

		Coordinate Position { get; }

		bool HasTag(string tag);

		ITile AddTag(string tag);

		ITile AddTags(params string[] tags);

		ITile RemoveTag(string tag);

		ITile RemoveTags(params string[] tag);
	}
}