using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game2D.Model;

namespace Game2DUnitTests
{
    [TestClass]
    public class RectangleTests
    {
        [TestMethod]
        public void CheckCollisionsTest()
        {
            Rectangle Object1;
            Rectangle Object2;

            Object1 = new Rectangle(10, 10, 10, 10);
            Object2 = new Rectangle(8, 12, 14, 6);

            Assert.IsTrue(Object1.CheckCollisions(Object2));
            Assert.IsTrue(Object2.CheckCollisions(Object1));

            Object1 = new Rectangle(10, 10, 10, 10);
            Object2 = new Rectangle(20, 20, 10, 10);

            Assert.IsTrue(Object1.CheckCollisions(Object2));
            Assert.IsTrue(Object2.CheckCollisions(Object1));

            Object1 = new Rectangle(10, 10, 10, 10);
            Object2 = new Rectangle(12, 25, 10, 10);

            Assert.IsFalse(Object1.CheckCollisions(Object2));
            Assert.IsFalse(Object2.CheckCollisions(Object1));
        }
    }
}
