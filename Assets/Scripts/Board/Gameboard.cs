﻿using UnityEngine;

namespace Board
{
	public class Gameboard : IGameboard
	{
		public int Width { get; private set; }
		public int Height { get; private set; }

		bool[] grid;
		
		public void Initialise(int width, int height)
		{
			this.Width = width;
			this.Height = height;
			
			this.grid = new bool[width * height];
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