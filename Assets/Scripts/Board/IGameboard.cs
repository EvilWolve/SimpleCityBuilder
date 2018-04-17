using Board.Visual;
using Configuration.Board;
using UnityEngine;

namespace Board
{
    public interface IGameboard
    {
        IGameboardVisual Visual { get; }
        
        int Width { get; }
        int Height { get; }
        
        void Initialise(GameboardConfiguration config);
        void Clear();
        
        void SetOccupied(GridRect area, bool occupied);
        bool IsOccupied(GridRect area);
    }
}
