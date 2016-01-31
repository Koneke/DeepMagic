namespace Deep.Magic
{
	public interface IGameRenderer
	{
		DmGame Game { get; set; }

		void Clear();

		void RenderLevel();

		void Update();
	}
}