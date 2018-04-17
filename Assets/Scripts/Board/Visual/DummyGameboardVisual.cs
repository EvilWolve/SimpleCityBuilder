using Configuration.Board;
using UnityEngine;

namespace Board.Visual
{
	public class DummyGameboardVisual : IGameboardVisual
	{
		public void SetGameboard(IGameboard gameboard)
		{
			Debug.Log(string.Format("Setting gameboard with width {0} and height {1}", gameboard.Width, gameboard.Height));
		}
	}
}
