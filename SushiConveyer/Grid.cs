using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiConveyer
{
    internal class Grid
    {
        private int Height { get; } 
        private int Width { get; }
        private Tile[,] TileGrid;

        public Grid(int H, int W)
        {
            Height = H;
            Width = W;

            BuildGrid();
            
        }
        private void BuildGrid()
        {
            TileGrid = new Tile[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    TileGrid[x, y] = new Tile(x, y);
                }
            }
        }

        private Tile? GetTile(Coordinate coordinate)
        {
            int X = coordinate.X;
            int Y = coordinate.Y;

            if (X < 0 || X >= Width || Y < 0 || Y >= Height)
            {
                return null;
            }

            return TileGrid[X, Y];
        }

        private Tile? GetTileFromDirection(Coordinate coordinate, Direction direction)
        {
            Coordinate? otherCoordinate = Coordinate.GetCoordinateFromDirection(coordinate, direction);
            
            if (otherCoordinate == null)
            {
                return null;
            }
            
            return GetTile(otherCoordinate);
        }
    }
}
