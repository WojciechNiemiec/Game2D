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
        //zmiany zmiany zmiany zmiany
        public enum GhostSide
        {
            Left1,
            Left2,
            Right1,
            Right2
        }

        private bool binateSprite;
        private bool alive;
        private bool bodyCollides;
        private bool feetCollides;
        private bool groundCollides;
        private bool isSituated;

        private const int speed = 2;
        private int fallSpeed;
        private const int animationSpeed = 2;
        private int animationIterator;
        private GhostSide side;

        private Dictionary<GhostSide, Sprite> sprite;
        private Rectangle bodyRect;
        private Rectangle feetRect;
        private Rectangle groundRect;
        public MotionDirector director;

        public bool Alive { get { return alive; } }
        public Rectangle BodyRect { get { return bodyRect; } }
        public const int Damage = 20;

        private void chooseSprite()
        {
            if (animationIterator++ > animationSpeed)
            {
                animationIterator = 0;
                binateSprite = !binateSprite;
            }

            switch (side)
            {
                case GhostSide.Left1:
                case GhostSide.Left2:
                    side = (binateSprite) ? GhostSide.Left1 : GhostSide.Left2;
                    break;
                case GhostSide.Right1:
                case GhostSide.Right2:
                    side = (binateSprite) ? GhostSide.Right1 : GhostSide.Right2;
                    break;
            }
        }

        public Ghost(Rectangle rect)
        {
            binateSprite = true;
            alive = true;
            bodyCollides = false;
            feetCollides = true;
            groundCollides = false;
            fallSpeed = 0;
            animationIterator = 0;
            side = GhostSide.Right1;

            rect.Height = Textures.GhostTextures["Right1"].Size.Y;
            rect.Width = Textures.GhostTextures["Right1"].Size.X;

            sprite = new Dictionary<GhostSide, Sprite>();
            sprite.Add(GhostSide.Left1, new Sprite(Textures.GhostTextures["Left1"]));
            sprite.Add(GhostSide.Left2, new Sprite(Textures.GhostTextures["Left2"]));
            sprite.Add(GhostSide.Right1, new Sprite(Textures.GhostTextures["Right1"]));
            sprite.Add(GhostSide.Right2, new Sprite(Textures.GhostTextures["Right2"]));

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
                    side = (side == GhostSide.Left1 || side == GhostSide.Left2) ? GhostSide.Right1 : GhostSide.Left1;
                }
            }
            
            bodyCollides = false;
            feetCollides = false;
            groundCollides = false;

            chooseSprite();

            switch (side)
            {
                case GhostSide.Left1:
                case GhostSide.Left2:
                    director = new MotionDirector(new MotionDirector.Vector((-speed), fallSpeed));
                    break;
                case GhostSide.Right1:
                case GhostSide.Right2:
                    director = new MotionDirector(new MotionDirector.Vector(speed, fallSpeed));
                    break;
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

            if ((move == MotionDirector.Direction.Right) && (side == GhostSide.Left1))
            {
                side = GhostSide.Right1;
            }
            else if ((move == MotionDirector.Direction.Left) && (side == GhostSide.Right1))
            {
                side = GhostSide.Left1;
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

            if (bodyRect.CheckCollisions(Collider.FeetRect))
            {
                alive = false;
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

        public void Draw(RenderWindow windowHandler, int xOffset, int yOffset)
        {
            sprite[side].Position = new Vector2f(bodyRect.Left + xOffset, bodyRect.Top + yOffset);
            windowHandler.Draw(sprite[side]);
        }
    }
}
