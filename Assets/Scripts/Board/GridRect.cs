using UnityEngine;

namespace Board
{
    public class GridRect
    {
        Vector2Int origin;
        Vector2Int dimensions;

        Vector2 center;
        
        public GridRect(int x, int y, int width, int height)
        {
            this.origin = new Vector2Int(x, y);
            this.dimensions = new Vector2Int(width, height);
            
            this.CalculateCenter();
        }
        
        public GridRect(Vector2Int origin, Vector2Int dimensions)
        {
            this.origin = origin;
            this.dimensions = dimensions;
            
            this.CalculateCenter();
        }

        void CalculateCenter()
        {
            this.center = new Vector2(this.MinX + this.Width / 2f, this.MinY + this.Height / 2f);
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

        public Vector2 Center
        {
            get { return this.center; }
        }
    }
}