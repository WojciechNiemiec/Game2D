using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Game2D.Model.Components;

namespace Game2DUnitTests
{
    [TestClass]
    public class MotionDirectorTests
    {
        [TestMethod]
        public void GetNextDirectionTest1()
        {
            MotionDirector.Direction direction;
            MotionDirector.Vector requestedOffset;
            MotionDirector director;

            requestedOffset.X = 5;
            requestedOffset.Y = 3;

            director = new MotionDirector(requestedOffset);

            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Right);

            director.MoveSucceed = true;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Down);

            director.MoveSucceed = true;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Right);

            director.MoveSucceed = true;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Down);

            director.MoveSucceed = true;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Right);

            director.MoveSucceed = true;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Down);

            director.MoveSucceed = true;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Right);

            director.MoveSucceed = true;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Right);

            director.MoveSucceed = false;
            direction = director.GetNextDirection();//tutaj
            Assert.IsTrue(direction == MotionDirector.Direction.Left);

            director.MoveSucceed = false;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.None);
        }

        [TestMethod]
        public void GetNextDirectionTest2()
        {
            MotionDirector.Direction direction;
            MotionDirector.Vector requestedOffset;
            MotionDirector director;

            requestedOffset.X = -3;
            requestedOffset.Y = -3;

            director = new MotionDirector(requestedOffset);

            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Left);

            director.MoveSucceed = true;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Up);

            director.MoveSucceed = true;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Left);

            director.MoveSucceed = true;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Up);

            director.MoveSucceed = false;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Down);

            director.MoveSucceed = true;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Left);

            director.MoveSucceed = false;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Right);

            director.MoveSucceed = false;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.None);
        }

        [TestMethod]
        public void GetNextDirectionTest3()
        {
            MotionDirector.Direction direction;
            MotionDirector.Vector requestedOffset;
            MotionDirector director;

            requestedOffset.X = 0;
            requestedOffset.Y = 0;

            director = new MotionDirector(requestedOffset);

            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.None);
        }

        [TestMethod]
        public void GetNextDirectionTest4()
        {
            MotionDirector.Direction direction;
            MotionDirector.Vector requestedOffset;
            MotionDirector director;

            requestedOffset.X = 2;
            requestedOffset.Y = -2;

            director = new MotionDirector(requestedOffset);

            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Right);

            director.MoveSucceed = true;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Up);

            director.MoveSucceed = false;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Down);

            director.MoveSucceed = true;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Right);

            director.MoveSucceed = true;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Up);

            director.MoveSucceed = true;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Up);

            director.MoveSucceed = true;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.None);
        }

        [TestMethod]
        public void GetNextDirectionTest5()
        {
            MotionDirector.Direction direction;
            MotionDirector.Vector requestedOffset;
            MotionDirector director;

            requestedOffset.X = -3;
            requestedOffset.Y = -1;

            director = new MotionDirector(requestedOffset);

            director.MoveSucceed = true;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Left);

            director.MoveSucceed = false;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Right);

            director.MoveSucceed = true;
            direction = director.GetNextDirection();
            Assert.IsTrue(direction == MotionDirector.Direction.Up);
        }
    }
}
