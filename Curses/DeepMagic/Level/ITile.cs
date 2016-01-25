namespace Deep.Magic
{
	public interface ITile
	{
		char Appearance { get; }

		short Color { get; }

		bool HasTag(string tag);

		ITile AddTag(string tag);

		ITile AddTags(params string[] tags);

		ITile RemoveTag(string tag);

		ITile RemoveTags(params string[] tag);
	}
}