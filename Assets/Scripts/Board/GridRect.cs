using UnityEngine;

namespace Board
{
    public class GridRect
    {
        Vector2Int origin;
        Vector2Int dimensions;
        
        public GridRect(int x, int y, int width, int height)
        {
            this.origin = new Vector2Int(x, y);
            this.dimensions = new Vector2Int(width, height);
        }
        
        public GridRect(Vector2Int origin, Vector2Int dimensions)
        {
            this.origin = origin;
            this.dimensions = dimensions;
        }
        
        public int X
        {
            get { return this.origin.x; }
        }
        
        public int Y
        {
            get { return this.origin.y; }
        }
        
        public int Width
        {
            get { return this.dimensions.x; }
        }
        
        public int Height
        {
            get { return this.dimensions.y; }
        }
        
        public int MinX
        {
            get { return this.X; }
        }
        
        public int MinY
        {
            get { return this.Y; }
        }
        
        public int MaxX
        {
            get { return this.MinX + this.Width; }
        }
        
        public int MaxY
        {
            get { return this.MinY + this.Height; }
        }
    }
}