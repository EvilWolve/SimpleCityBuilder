using UnityEngine;

namespace Board
{
    public interface IGameboard
    {
        int Width { get; }
        int Height { get; }
        
        void Initialise(int width, int height);
        void SetOccupied(GridRect area, bool occupied);
        bool IsOccupied(GridRect area);
    }
}
