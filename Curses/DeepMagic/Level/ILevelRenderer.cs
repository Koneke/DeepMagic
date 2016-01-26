namespace Deep.Magic
{
	public interface ILevelRenderer
	{
		ILevel Level { get; set; }

		void Clear();

		void RenderLevel();
	}
}