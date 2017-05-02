using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using Game2D.Model.Components;

namespace Game2D.Model
{
    public class MainCharacter: IMovable
    {
        public enum CharacterSide
        {
            StaysLeft,
            MovesLeft1,
            MovesLeft2,
            JumpsLeft,
            StaysRight,
            MovesRight1,
            MovesRight2,
            JumpsRight
        }

        public enum CharacterSounds
        {
            Jump,
            Kick
        }

        private bool binateSprite;
        private bool alive;
        private bool bodyCollides;
        private bool feetCollides;
        private bool shooted;
        private bool coollidesWithLadder;
        private bool isSituated;

        private const int speed = 6;
        private int fallSpeed;
        private const int animationSpeed = 5;
        private int animationIterator;
        private int health;

        private CharacterSide side;

        private Dictionary<CharacterSide, Sprite> sprite;
        private Dictionary<CharacterSounds, Sound> sound;
        private Rectangle bodyRect;
        private Rectangle feetRect;
        public MotionDirector director;

        public bool Alive { get { return alive; } }
        public Rectangle BodyRect { get { return bodyRect; } }
        public Rectangle FeetRect { get { return feetRect; } }

        public MainCharacter(Rectangle rect)
        {
            binateSprite = true;
            alive = true;
            bodyCollides = false;
            feetCollides = false;
            shooted = false;
            coollidesWithLadder = false;
            side = CharacterSide.StaysRight;

            health = 100;
            fallSpeed = 0;
            animationIterator = 0;

            try
            {
                string path = "../../../Game2D/Sounds/";

                sound = new Dictionary<CharacterSounds, Sound>();

                sound.Add(CharacterSounds.Jump, new Sound(new SoundBuffer(path + "jump.wav")));
                sound.Add(CharacterSounds.Kick, new Sound(new SoundBuffer(path + "kick.wav")));
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot load the sounds", e);
            }

            try
            {
                rect.Height = Textures.MainCharacterTextures["Right1"].Size.Y;
                rect.Width = Textures.MainCharacterTextures["Right1"].Size.X;

                sprite = new Dictionary<CharacterSide, Sprite>();

                sprite.Add(CharacterSide.StaysLeft, new Sprite(Textures.MainCharacterTextures["Left0"]));
                sprite.Add(CharacterSide.StaysRight, new Sprite(Textures.MainCharacterTextures["Right0"]));
                sprite.Add(CharacterSide.MovesLeft1, new Sprite(Textures.MainCharacterTextures["Left1"]));
                sprite.Add(CharacterSide.MovesLeft2, new Sprite(Textures.MainCharacterTextures["Left2"]));
                sprite.Add(CharacterSide.MovesRight1, new Sprite(Textures.MainCharacterTextures["Right1"]));
                sprite.Add(CharacterSide.MovesRight2, new Sprite(Textures.MainCharacterTextures["Right2"]));
                sprite.Add(CharacterSide.JumpsLeft, new Sprite(Textures.MainCharacterTextures["Left3"]));
                sprite.Add(CharacterSide.JumpsRight, new Sprite(Textures.MainCharacterTextures["Right3"]));
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot create a sprite: ", e);
            }

            bodyRect = rect;
            feetRect = new Rectangle(rect.Bottom, (rect.Left + 3), 1, (rect.Width - 6)); 
        }

        public void GetAction()
        {
            bodyCollides = false;
            isSituated = false;

            if (++animationIterator > animationSpeed)
            {
                binateSprite = !binateSprite;
                animationIterator = 0;
            }

            if (feetCollides)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                {
                    fallSpeed = 4 * (-speed);
                    feetCollides = false;
                    sound[CharacterSounds.Jump].Play();
                }
                else
                {
                    fallSpeed = 0;
                }
            }
            else
            {
                fallSpeed += 2;
            }

            feetCollides = false;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                director = new MotionDirector(new MotionDirector.Vector(speed, fallSpeed));

                if (fallSpeed == 0)
                {
                    side = (binateSprite) ? CharacterSide.MovesRight1 : CharacterSide.MovesRight2;
                }
                else
                {
                    side = CharacterSide.JumpsRight;
                }
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                director = new MotionDirector(new MotionDirector.Vector(-speed, fallSpeed));

                if (fallSpeed == 0)
                {
                    side = (binateSprite) ? CharacterSide.MovesLeft1 : CharacterSide.MovesLeft2;
                }
                else
                {
                    side = CharacterSide.JumpsLeft;
                }
            }
            else
            {
                director = new MotionDirector(new MotionDirector.Vector(0, fallSpeed));

                if (fallSpeed == 0)
                {
                    side = (side >= CharacterSide.StaysRight) ? CharacterSide.StaysRight : CharacterSide.StaysLeft;
                }
                else
                {
                    side = (side >= CharacterSide.StaysRight) ? CharacterSide.JumpsRight : CharacterSide.JumpsLeft;
                }
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
                    FeetRect.Left--;
                    break;

                case MotionDirector.Direction.Right:
                    bodyRect.Left++;
                    FeetRect.Left++;
                    break;

                case MotionDirector.Direction.Up:
                    bodyRect.Top--;
                    FeetRect.Top--;
                    break;

                case MotionDirector.Direction.Down:
                    bodyRect.Top++;
                    FeetRect.Top++;
                    break;

                default:
                    throw new Exception();
            }
        }

        public void CheckCollision(MainCharacter Collider)
        {
            //pass
        }

        public void CheckCollision(Ghost Collider)
        {
            if (this.feetRect.CheckCollisions(Collider.BodyRect))
            {
                feetCollides = true;

                if (sound[CharacterSounds.Kick].Status != SoundStatus.Playing)
                {
                    sound[CharacterSounds.Kick].Play();
                }
            }

            if (this.bodyRect.CheckCollisions(Collider.BodyRect))
            {
                bodyCollides = true;
                alive = false;
            }
        }

        public void CheckCollision(Ground Collider)
        {
            if (this.feetRect.CheckCollisions(Collider.Rect))
            {
                feetCollides = true;
            }

            if (this.bodyRect.CheckCollisions(Collider.Rect))
            {
                bodyCollides = true;
            }
        }

        public void Draw(RenderWindow windowHandler, int xOffset, int yOffset)
        {
            sprite[side].Position = new Vector2f(bodyRect.Left + xOffset, bodyRect.Top + yOffset);
            windowHandler.Draw(sprite[side]);
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
    }
}
