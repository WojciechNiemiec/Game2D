using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

using Game2D.Model;
using SFML.Graphics;

namespace Game2D.Controler
{
    public sealed class Computer
    {
        public class LevelContext
        {
            public List<IMovable> Movables = null;
            public List<IMotionless> Motionless;
            public MainCharacter MainCharacterHandler;

            public LevelContext(string levelPath)
            {
                Movables = new List<IMovable>();
                Motionless = new List<IMotionless>();

                string[] levelDescriptor = File.ReadAllLines(levelPath);

                foreach (string line in levelDescriptor)
                {
                    object product;

                    product = Factory.CreateRectangularObject(line);

                    if (product.GetType().Equals(typeof(Ground)))
                    {
                        Motionless.Add((Ground)product);
                    }
                    else if (product.GetType().Equals(typeof(MainCharacter)))
                    {
                        Movables.Add((MainCharacter)product);
                        MainCharacterHandler = (MainCharacter)product;
                    }
                    else if (product.GetType().Equals(typeof(Ghost)))
                    {
                        Movables.Add((Ghost)product);
                    }
                    else throw new NotImplementedException();
                }

            }
        }

        public LevelContext context;

        public Computer()
        {
            string levelPath = "../../../Game2D/Controler/Levels/";

            context = new LevelContext(levelPath + "Level1.txt");
        }

        public void RequestActions()
        {
            foreach (IMovable element in context.Movables)
            {
                element.GetAction();
            }
        }

        public void PerformActions()
        {
            foreach (IMovable element in context.Movables)
            {
                while (element.GetIsSituated() == false)
                {
                    foreach (IMotionless collider in context.Motionless)
                    {
                        if (collider.GetType().Equals(typeof(Ground)))
                        {
                            element.CheckCollision((Ground)collider);
                        }
                        else throw new Exception();
                    }
                        
                    foreach (IMovable collider in context.Movables)
                    {
                        if (collider.GetType().Equals(typeof(Ghost)))
                        {
                            element.CheckCollision((Ghost)collider);
                        }
                        else if (collider.GetType().Equals(typeof(MainCharacter)))
                        {
                            element.CheckCollision((MainCharacter)collider);
                        }
                        else throw new Exception();
                    }

                    element.Move();
                }
            }
        }

        public void DrawObjects(RenderWindow windowHandler)
        {
            int XOffset = -(context.MainCharacterHandler.BodyRect.Left - 600);

            foreach (IMotionless element in context.Motionless)
            {
                element.Draw(windowHandler, XOffset, 0);
            }

            foreach (IMovable element in context.Movables)
            {
                element.Draw(windowHandler, XOffset, 0);
            }
        }
    }
}
