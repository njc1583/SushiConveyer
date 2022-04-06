using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiConveyer
{
    internal class Tile
    {
        private Coordinate _coordinate { get; }
        private bool _isPassible { get; set; } = true;
        private FloorObject _floorObject { get; } = null;

        public Tile(int X, int Y)
        {
            _coordinate = new Coordinate(X, Y);
        }

        public Tile(Coordinate coordinate)
        {
            _coordinate = new Coordinate(coordinate);
        }
    }
}
