using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Game2D.Model.Components
{
    public class MotionDirector
    {
        public struct Vector
        {
            public int X;
            public int Y;

            public Vector(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public enum Direction
        {
            None,
            Up,
            Down,
            Left,
            Right
        }

        private uint NumberOfCollisions;
        private uint maxNumberOfCollisions;
        private bool previousMoveSucceed;
        private bool verticalMoveFailed;
        private bool horizontalMoveFailed;
        private Vector requestedOffset;
        private Vector currentOffset;
        private Direction lastDirection;
        private Direction nextDirection;

        public bool MoveSucceed
        {
            set
            {
                this.previousMoveSucceed = value;
            }
        }

        public bool HightReached
        {
            set
            {
                if (value == true)
                {
                    requestedOffset.Y = currentOffset.Y;
                }
            }
        }

        public Direction NextMove
        {
            get
            {
                return GetNextDirection();
            }
        }

        private bool moveVertical()
        {
            bool movePossible = true;
            verticalMoveFailed = false;

            if (requestedOffset.Y < currentOffset.Y)
            {
                nextDirection = Direction.Up;
                currentOffset.Y--;
            }
            else if (requestedOffset.Y > currentOffset.Y)
            {
                nextDirection = Direction.Down;
                currentOffset.Y++;
            }
            else
            {
                verticalMoveFailed = true;
                movePossible = false;
            }

            return movePossible;
        }

        private bool moveHorizontal()
        {
            bool movePossible = true;
            horizontalMoveFailed = false;

            if (requestedOffset.X > currentOffset.X)
            {
                nextDirection = Direction.Right;
                currentOffset.X++;
            }
            else if (requestedOffset.X < currentOffset.X)
            {
                nextDirection = Direction.Left;
                currentOffset.X--;
            }
            else
            {
                horizontalMoveFailed = true;
                movePossible = false;
            }

            return movePossible;
        }

        private void moveBack()
        {
            if (lastDirection == Direction.Left)
            {
                nextDirection = Direction.Right;
                currentOffset.X++;
            }
            else if (lastDirection == Direction.Right)
            {
                nextDirection = Direction.Left;
                currentOffset.X--;
            }
            else if (lastDirection == Direction.Up)
            {
                nextDirection = Direction.Down;
                currentOffset.Y++;
            }
            else if (lastDirection == Direction.Down)
            {
                nextDirection = Direction.Up;
                currentOffset.Y--;
            }
            previousMoveSucceed = true;
        }

        public MotionDirector(Vector requestedOffset)
        {
            this.requestedOffset.X = requestedOffset.X;
            this.requestedOffset.Y = requestedOffset.Y;

            currentOffset.X = 0;
            currentOffset.Y = 0;

            horizontalMoveFailed = false;
            verticalMoveFailed = false;

            lastDirection = Direction.None;
            previousMoveSucceed = true;

            if (requestedOffset.Y == 0)
            {
                verticalMoveFailed = true;
            }
        }
        
        public Direction GetNextDirection()
        {
            if (requestedOffset.Equals(currentOffset) && previousMoveSucceed)
            {
                nextDirection = Direction.None;
            }
            else if (horizontalMoveFailed && verticalMoveFailed)
            {
                nextDirection = Direction.None;
            }
            else if ((lastDirection == Direction.Left) || (lastDirection == Direction.Right))
            {
                if (previousMoveSucceed == true)
                {
                    moveVertical();

                    if (verticalMoveFailed)
                    {
                        moveHorizontal();
                    }
                }
                else
                {
                    horizontalMoveFailed = true;
                    moveBack();
                }
            }
            else if ((lastDirection == Direction.Up) || (lastDirection == Direction.Down) || (lastDirection == Direction.None))
            {
                if (previousMoveSucceed == true)
                {
                    moveHorizontal();

                    if (horizontalMoveFailed)
                    {
                        moveVertical();
                    }
                }
                else
                {
                    verticalMoveFailed = true;
                    moveBack();
                }
            }

            lastDirection = nextDirection;

            return nextDirection;
        }
    }
}
