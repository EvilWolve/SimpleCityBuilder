using Board.Visual;
using Configuration.Board;

namespace Board
{
	public class Gameboard : IGameboard
	{
		public int Width { get; private set; }
		public int Height { get; private set; }
		
		public GameboardConfiguration Config { get; private set; }

		bool[] grid;
		
		public void Initialise(GameboardConfiguration config)
		{
			this.Width = config.dimensions.x;
			this.Height = config.dimensions.y;
			
			this.grid = new bool[this.Width * this.Height];

			this.Config = config;
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

		public bool IsInGrid(GridRect area)
		{
			return area.MinX >= 0 && area.MaxX <= this.Width && area.MinY >= 0 && area.MaxY <= this.Height;
		}

		public bool IsValidPlacement(GridRect area)
		{
			return this.IsInGrid(area) && !this.IsOccupied(area);
		}

		int GetIndex(int x, int y)
		{
			return x + y * this.Width;
		}
	}
}