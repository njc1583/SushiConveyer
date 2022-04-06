using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiConveyer
{
    public class Coordinate
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Coordinate(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public Coordinate(Coordinate other)
        {
            this.X = other.X;
            this.Y = other.Y;
        }

        public static Coordinate? GetCoordinateFromDirection(Coordinate coordinate, Direction direction)
        {
            int X = coordinate.X;
            int Y = coordinate.Y; 

            switch (direction)
            {
                case Direction.UP:
                    Y++;
                    break;
                case Direction.LEFT:
                    X--;
                    break;
                case Direction.DOWN:
                    Y--;
                    break;
                case Direction.RIGHT:
                    X++;
                    break;
                case Direction.UNDEFINED:
                default:
                    return null;
            }

            return new Coordinate(X, Y);
        }

        public static Direction GetDirectionFromCoordinates(Coordinate source, Coordinate target)
        {
            int X_source = source.X;
            int Y_source = source.Y;
            int X_target = target.X;
            int Y_target = target.Y;

            if (X_source == X_target)
            {
                if (Y_source + 1 == Y_target)
                {
                    return Direction.UP;
                } 
                else if (Y_source - 1 == Y_target)
                {
                    return Direction.DOWN;
                }
            }
            else if (Y_source == Y_target)
            {
                if (X_source + 1 == X_target)
                {
                    return Direction.RIGHT;
                }
                else if (X_source - 1 == X_target)
                {
                    return Direction.LEFT;
                }
            }

            return Direction.UNDEFINED;
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Coordinate other = (Coordinate)obj;
                return this.X == other.X && this.Y == other.Y;
            }
        }
    }
}
