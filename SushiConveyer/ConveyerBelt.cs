using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiConveyer
{
    internal class ConveyerBelt : FloorObject 
    {
        public Direction InputDirection { get; set; } = Direction.UNDEFINED;
        public Direction OutputDirection { get; set; } = Direction.UNDEFINED;

        public ConveyerBelt()
        {
            Rotation = Direction.UNDEFINED;
        }

        public bool IsValid()
        {
            return InputDirection != Direction.UNDEFINED && OutputDirection != Direction.UNDEFINED;
        }

        public bool IsCorner()
        {
            return IsValid() && InputDirection != OutputDirection;
        }
    }
}
