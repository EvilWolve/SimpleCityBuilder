using Configuration.Board;

namespace Board.Visual
{
	public interface IGameboardVisualFactory
	{
		IGameboardVisual CreateVisualForGameboard(IGameboard gameboard);
	}
}
