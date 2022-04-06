using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiConveyer
{
    internal class Tile
    {
        public Coordinate coordinate { get; }
        public bool isPassible { get; private set; } = true;

        public bool hasObject { get; private set; } = false;
        public FloorObject floorObject { get; private set; } = null;

        public Tile(int X, int Y)
        {
            coordinate = new Coordinate(X, Y);
        }

        public Tile(Coordinate coordinate)
        {
            this.coordinate = new Coordinate(coordinate);
        }

        public bool PlaceObject(FloorObject floorObject)
        {
            if (this.floorObject == null)
            {
                return false;
            }

            this.floorObject = floorObject;
            this.hasObject = true;
            this.isPassible = false;
            return true;
        } 

        public bool RemoveObject()
        {
            if (this.floorObject != null)
            {
                return false;
            }

            this.floorObject = null;
            this.hasObject = false;
            this.isPassible = true;
            return true;
        }
    }
}
