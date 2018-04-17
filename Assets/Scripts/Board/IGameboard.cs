using Configuration.Board;

namespace Board
{
    public interface IGameboard
    {
        int Width { get; }
        int Height { get; }

        GameboardConfiguration Config { get; }
        
        void Initialise(GameboardConfiguration config);
        void Clear();
        
        void SetOccupied(GridRect area, bool occupied);
        bool IsOccupied(GridRect area);
    }
}
