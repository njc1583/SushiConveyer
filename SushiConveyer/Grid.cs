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

        public bool PlaceObject(Coordinate coordinate, FloorObject floorObject)
        {
            Tile? tile = GetTile(coordinate);

            if (tile == null)
            {
                return false;
            }

            return tile.PlaceObject(floorObject);
        }

        public bool RemoveObject(Coordinate coordinate)
        {
            Tile? tile = GetTile(coordinate);

            if (tile == null)
            {
                return false;
            }

            return tile.RemoveObject();
        }

        public bool ConstructConveyerBelt()
        {
            List<Tile> conveyerBeltTiles = _GetConveyerBeltTiles(false);

            if (conveyerBeltTiles.Count == 0)
            {
                // Log that there are no conveyer belt tiles in the grid
                return false;
            }

            Tile upperLeftmostConveyerBelt = conveyerBeltTiles[0];

            foreach (Tile tile in conveyerBeltTiles)
            {
                if (tile.coordinate.Y > upperLeftmostConveyerBelt.coordinate.Y)
                {
                    upperLeftmostConveyerBelt = tile;
                }
                else if (tile.coordinate.Y == upperLeftmostConveyerBelt.coordinate.Y 
                    && tile.coordinate.X < upperLeftmostConveyerBelt.coordinate.X)
                {
                    upperLeftmostConveyerBelt = tile;
                }
            }

            return _ConstructConveyerBelt(upperLeftmostConveyerBelt, Direction.RIGHT);
        }

        private bool _ConstructConveyerBelt(Tile startTile, Direction startDirection)
        {
            Tile currentTile = startTile;
            ConveyerBelt currentBelt = (ConveyerBelt)currentTile.floorObject;
            Direction currentDirection = startDirection;
            
            while (true)
            {
                Tile nextTile = null;
                ConveyerBelt nextBelt = null;

                Direction[] directionPriority = _GetDirectionPriority(currentDirection);             

                foreach (Direction candidateDirection in directionPriority)
                {
                    Tile? candidateNextTile = GetTileFromDirection(currentTile, candidateDirection);

                    if (candidateNextTile == null)
                    {
                        continue;
                    }
                    else if (!candidateNextTile.hasObject || candidateNextTile.floorObject.GetType() != typeof(ConveyerBelt))
                    {
                        continue;
                    }

                    currentDirection = candidateDirection;
                    nextTile = candidateNextTile;
                    nextBelt = (ConveyerBelt)(nextTile.floorObject);
                    break;
                }

                if (nextTile == null || nextBelt == null)
                {
                    // Log where this happened; means a dead-end was reached
                    return false;
                }
                 
                currentBelt.OutputDirection = currentDirection;
                nextBelt.InputDirection = currentDirection;
                currentTile = nextTile;
                currentBelt = nextBelt;

                if (currentTile.Equals(startTile))
                {
                    break;
                }
            }

            // The number conveyer belts in the loop MUST be equal to the number of 
            // total belts on the grid
            List<Tile> validBeltTiles = _GetConveyerBeltTiles(true);
            List<Tile> allBeltTiles = _GetConveyerBeltTiles(false);
            
            if (validBeltTiles.Count != allBeltTiles.Count)
            {
                // Log which tiles are considered invalid
                return false;
            }
            
            return true;
        }

        private List<Tile> _GetConveyerBeltTiles(bool onlyValidBelts)
        {
            List<Tile> conveyerBeltTiles = new List<Tile>();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Tile? tile = GetTile(x, y);

                    if (tile.hasObject)
                    {
                        if (tile.floorObject.GetType() == typeof(ConveyerBelt))
                        {
                            
                            if (onlyValidBelts)
                            {
                                ConveyerBelt belt = (ConveyerBelt)tile.floorObject;
                                if (belt.IsValid())
                                {
                                    conveyerBeltTiles.Add(tile);
                                }
                            }
                            else
                            {

                                conveyerBeltTiles.Add(tile);
                            }
                        }
                    }
                }
            }

            return conveyerBeltTiles;
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

        private Tile? GetTile(int X, int Y)
        {
            return GetTile(new Coordinate(X, Y));
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
        private Tile? GetTileFromDirection(Tile tile, Direction direction)
        {
            return GetTileFromDirection(tile.coordinate, direction);
        }

        private Direction GetDirectionFromTile(Tile source, Tile target)
        {
            return Coordinate.GetDirectionFromCoordinates(source.coordinate, target.coordinate);
        }

        

        private Direction[] _GetDirectionPriority(Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    return new Direction[] { Direction.LEFT, Direction.UP, Direction.RIGHT };
                case Direction.RIGHT:
                    return new Direction[] { Direction.UP, Direction.RIGHT, Direction.DOWN };
                case Direction.DOWN:
                    return new Direction[] { Direction.RIGHT, Direction.DOWN, Direction.LEFT };
                case Direction.LEFT:
                    return new Direction[] { Direction.DOWN, Direction.LEFT, Direction.UP };
                case Direction.UNDEFINED:
                default:
                    return new Direction[] { Direction.UNDEFINED };
            }
        }
    }
}
