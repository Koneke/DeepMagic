namespace Deep.Magic
{
	public interface ITile
	{
		bool HasTag(string tag);

		ITile AddTag(string tag);

		ITile AddTags(params string[] tags);

		ITile RemoveTag(string tag);

		ITile RemoveTags(params string[] tag);
	}
}