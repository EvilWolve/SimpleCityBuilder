using Buildings.Visual;
using Configuration.Board;
using UnityEngine;
using UnityEngine.Assertions;

namespace Board.Visual
{
	public class GameboardVisualFactory : IGameboardVisualFactory
	{
		public IGameboardVisual CreateVisualForGameboard(IGameboard gameboard, GameboardConfiguration config)
		{
			GameObject prefab = config.prefab;
			Assert.IsNotNull(prefab);

			GameObject go = Object.Instantiate(prefab);
			GameboardVisual visual = go.GetComponent<GameboardVisual>();
			
			visual.SetGameboard(gameboard);

			return visual;
		}
	}
}
