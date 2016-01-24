namespace Deep.Magic
{
	public interface ITile
	{
		char Appearance { get; }

		short Color { get; }

		bool HasTag(string tag);

		void AddTag(string tag);

		void RemoveTag(string tag);
	}
}