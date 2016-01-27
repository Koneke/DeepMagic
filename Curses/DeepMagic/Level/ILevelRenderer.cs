namespace Deep.Magic
{
	public interface IGameRenderer
	{
		ILevel Level { get; set; }

		void Clear();

		void RenderLevel();
	}
}