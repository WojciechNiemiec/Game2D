using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;

using Game2D.Model;
using Game2D.Model.Components;

namespace Game2D.Model
{
    public class Ghost: IMovable
    {
        public enum GhostSide
        {
            Left,
            Right
        }

        private bool binateTexture;
        private bool alive;
        private bool bodyCollides;
        private bool feetCollides;
        private bool groundCollides;
        private bool isSituated;

        private const int speed = 2;
        private int fallSpeed;
        private const int animationSpeed = 10;
        private int animationIterator;
        private GhostSide side;

        private Sprite sprite;
        private Rectangle bodyRect;
        private Rectangle feetRect;
        private Rectangle groundRect;
        public MotionDirector director;


        public Rectangle BodyRect { get { return bodyRect; } }
        public const int Damage = 20;

        private void prepareNewTexture()
        {
            animationIterator++;

            if (side == GhostSide.Left)
            {
                if (animationIterator < animationSpeed / 2)
                {
                    sprite = new Sprite(Textures.GhostTextures["Left0"]);
                }
                else
                {
                    sprite = new Sprite(Textures.GhostTextures["Left1"]);
                }
            }
            else if (side == GhostSide.Right)
            {
                if (animationIterator < animationSpeed / 2)
                {
                    sprite = new Sprite(Textures.GhostTextures["Right0"]);
                }
                else
                {
                    sprite = new Sprite(Textures.GhostTextures["Right1"]);
                }
            }

            if (animationIterator > animationSpeed)
            {
                animationIterator = 0;
            }
        }

        public Ghost(Rectangle rect)
        {
            binateTexture = true;
            alive = true;
            bodyCollides = false;
            feetCollides = true;
            groundCollides = false;
            fallSpeed = 0;
            animationIterator = 0;
            side = GhostSide.Right;

            rect.Height = Textures.GhostTextures["Right1"].Size.Y;
            rect.Width = Textures.GhostTextures["Right1"].Size.X;

            sprite = new Sprite(Textures.GhostTextures["Right1"]);
            sprite.Position = new Vector2f(rect.Left, rect.Top);

            bodyRect = rect;
            feetRect = new Rectangle(rect.Bottom, (rect.Left + (int)rect.Width / 2), 1, (rect.Width / 2));
            groundRect = new Rectangle(rect.Bottom, (rect.Left), 1, (rect.Width));
        }

        public void GetAction()
        {
            isSituated = false;

            if (groundCollides == false)
            {
                fallSpeed += 2;
            }
            else
            {
                fallSpeed = 0;

                if (bodyCollides == true)
                {
                    side = (side == GhostSide.Left) ? GhostSide.Right : GhostSide.Left;
                }
            }
            
            bodyCollides = false;
            feetCollides = false;
            groundCollides = false;

            if (side == GhostSide.Right)
            {
                director = new MotionDirector(new MotionDirector.Vector(speed, fallSpeed));
            }
            else if (side == GhostSide.Left)
            {
                director = new MotionDirector(new MotionDirector.Vector((-speed), fallSpeed));
            }
        }

        public void Move()
        {
            if (fallSpeed > 0 && feetCollides)
            {
                fallSpeed = 0;
                director.HightReached = true;
            }

            director.MoveSucceed = !bodyCollides;
            MotionDirector.Direction move = director.NextMove;
            bodyCollides = false;

            switch (move)
            {
                case MotionDirector.Direction.None:
                    isSituated = true;
                    break;

                case MotionDirector.Direction.Left:
                    bodyRect.Left--;
                    feetRect.Left--;
                    groundRect.Left--;
                    break;

                case MotionDirector.Direction.Right:
                    bodyRect.Left++;
                    feetRect.Left++;
                    groundRect.Left++;
                    break;

                case MotionDirector.Direction.Up:
                    bodyRect.Top--;
                    feetRect.Top--;
                    groundRect.Top--;
                    break;

                case MotionDirector.Direction.Down:
                    bodyRect.Top++;
                    feetRect.Top++;
                    groundRect.Top++;
                    break;

                default:
                    throw new Exception();
            }
            if ((move == MotionDirector.Direction.Right) && (side == GhostSide.Left))
            {
                side = GhostSide.Right;
            }
            else if ((move == MotionDirector.Direction.Left) && (side == GhostSide.Right))
            {
                side = GhostSide.Left;
            }
        }

        public void CheckCollision(MainCharacter Collider)
        {
            if (groundRect.CheckCollisions(Collider.BodyRect))
            {
                groundCollides = true;
            }

            if (feetRect.CheckCollisions(Collider.BodyRect))
            {
                feetCollides = true;
            }

            if (bodyRect.CheckCollisions(Collider.BodyRect))
            {
                bodyCollides = true;
            }
        }

        public void CheckCollision(Ghost Collider)
        {
            //throw new NotImplementedException();
        }

        public void CheckCollision(Ground Collider)
        {
            if (groundRect.CheckCollisions(Collider.Rect))
            {
                groundCollides = true;
            }
            else groundCollides = false;

            if (feetRect.CheckCollisions(Collider.Rect))
            {
                feetCollides = true;
            }
            else feetCollides = false;

            if (bodyRect.CheckCollisions(Collider.Rect) || (!feetCollides && groundCollides))
            {
                bodyCollides = true;
            }
        }

        public bool GetIsSituated()
        {
            return isSituated;
        }

        public void CheckCollision(IMovable Collider)
        {
            throw new NotImplementedException();
        }

        public void CheckCollision(IMotionless collider)
        {
            throw new NotImplementedException();
        }

        public void Draw(RenderWindow windowHandler, int xOffset, int yOffset)
        {
            prepareNewTexture();
            sprite.Position = new Vector2f(bodyRect.Left + xOffset, bodyRect.Top + yOffset);
            windowHandler.Draw(sprite);
        }
    }
}
