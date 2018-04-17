using Board.Visual;
using Buildings.Visual;
using Configuration.Board;
using UnityEngine;
using Utilities;

namespace Board
{
	public class Gameboard : IGameboard
	{
		public IGameboardVisual Visual { get; private set; }
		
		public int Width { get; private set; }
		public int Height { get; private set; }

		bool[] grid;
		
		public void Initialise(GameboardConfiguration config)
		{
			this.Width = config.dimensions.x;
			this.Height = config.dimensions.y;
			
			this.grid = new bool[this.Width * this.Height];
			
			IGameboardVisualFactory gameboardVisualFactory = ServiceLocator.Instance.GetService<IGameboardVisualFactory>();
			this.Visual = gameboardVisualFactory.CreateVisualForGameboard(this, config);
		}

		public void Clear()
		{
			this.grid = new bool[this.Width * this.Height];
		}

		public void SetOccupied(GridRect area, bool occupied)
		{
			for (int x = area.MinX; x < area.MaxX; x++)
			{
				for (int y = area.MinY; y < area.MaxY; y++)
				{
					this.grid[this.GetIndex(x, y)] = occupied;
				}
			}
		}

		public bool IsOccupied(GridRect area)
		{
			for (int x = area.MinX; x < area.MaxX; x++)
			{
				for (int y = area.MinY; y < area.MaxY; y++)
				{
					if (this.grid[this.GetIndex(x, y)])
						return true;
				}
			}

			return false;
		}

		int GetIndex(int x, int y)
		{
			return x + y * this.Width;
		}
	}
}