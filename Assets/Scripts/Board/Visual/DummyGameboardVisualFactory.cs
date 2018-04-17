using Configuration.Board;

namespace Board.Visual
{
	public class DummyGameboardVisualFactory : IGameboardVisualFactory
	{
		public IGameboardVisual CreateVisualForGameboard(IGameboard gameboard, GameboardConfiguration config)
		{
			DummyGameboardVisual visual = new DummyGameboardVisual();
			visual.SetGameboard(gameboard);

			return visual;
		}
	}
}
