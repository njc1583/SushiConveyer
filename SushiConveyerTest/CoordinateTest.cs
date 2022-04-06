using Microsoft.VisualStudio.TestTools.UnitTesting;
using SushiConveyer;

namespace SushiConveyerTest
{
    [TestClass]
    public class CoordinateTest
    {
        private readonly Coordinate _origin = new Coordinate(0, 0);
        private readonly Coordinate _up = new Coordinate(0, 1);
        private readonly Coordinate _left = new Coordinate(-1, 0);
        private readonly Coordinate _down = new Coordinate(0, -1);
        private readonly Coordinate _right = new Coordinate(1, 0);

        [TestMethod]
        public void EqualityTest()
        {
            Coordinate coordinate = new Coordinate(0, 0);
            Coordinate coordinate2 = new Coordinate(0, 0);

            Assert.IsTrue(coordinate.Equals(coordinate2));
            Assert.IsFalse(coordinate.Equals((4)));
            Assert.IsFalse(coordinate.Equals(null));
        }

        [TestMethod]
        public void CoordinateFromDirectionTest()
        {  
            Assert.AreEqual(_up, Coordinate.GetCoordinateFromDirection(_origin, Direction.UP));
            Assert.AreEqual(_left, Coordinate.GetCoordinateFromDirection(_origin, Direction.LEFT));
            Assert.AreEqual(_down, Coordinate.GetCoordinateFromDirection(_origin, Direction.DOWN));
            Assert.AreEqual(_right, Coordinate.GetCoordinateFromDirection(_origin, Direction.RIGHT));
            Assert.IsNull(Coordinate.GetCoordinateFromDirection(_origin, Direction.UNDEFINED));

        }

        [TestMethod]
        public void DirectionFromCoordinateTest()
        {
            Coordinate other = new Coordinate(1, 1);

            Assert.AreEqual(Direction.UP, Coordinate.GetDirectionFromCoordinates(_origin, _up));
            Assert.AreEqual(Direction.LEFT, Coordinate.GetDirectionFromCoordinates(_origin, _left));
            Assert.AreEqual(Direction.DOWN, Coordinate.GetDirectionFromCoordinates(_origin, _down));
            Assert.AreEqual(Direction.RIGHT, Coordinate.GetDirectionFromCoordinates(_origin, _right));
            Assert.AreEqual(Direction.UNDEFINED, Coordinate.GetDirectionFromCoordinates(_origin, _origin));
            Assert.AreEqual(Direction.UNDEFINED, Coordinate.GetDirectionFromCoordinates(_origin, other));
        }
    }
}