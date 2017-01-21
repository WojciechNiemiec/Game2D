using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using Game2D.Model.Components;

namespace Game2D.Model
{
    public class Ground: IMotionless
    {
        private Rectangle rect;
        private Sprite dirt;
        private Sprite grass;

        public Rectangle Rect { get { return rect; } }

        public Ground(Rectangle rect)
        {
            uint heightBase = Textures.GroundTextures["Dirt"].Size.Y;
            uint widthBase = Textures.GroundTextures["Dirt"].Size.X;

            if ((rect.Height % heightBase) != 0)
            {
                rect.Height = rect.Height / heightBase;
                rect.Height = rect.Height * heightBase;
            }

            if ((rect.Width % widthBase) != 0)
            {
                rect.Width = rect.Width / widthBase;
                rect.Width = rect.Width * widthBase;
            }
            
            dirt = new Sprite(Textures.GroundTextures["Dirt"], new IntRect(0, 0, (int)rect.Width, (int)(rect.Height)));
            grass = new Sprite(Textures.GroundTextures["Grass"], new IntRect(0, 0, (int)(rect.Width), (int)heightBase));

            dirt.Position = new SFML.Window.Vector2f(rect.Left, rect.Top);
            grass.Position = new SFML.Window.Vector2f(rect.Left, rect.Top);

            this.rect = rect;
        }

        public void Draw(RenderWindow windowHandler, int xOffset, int yOffset)
        {
            dirt.Position = new SFML.Window.Vector2f(Rect.Left + xOffset, Rect.Top + yOffset);
            grass.Position = dirt.Position;
            windowHandler.Draw(dirt);
            windowHandler.Draw(grass);  
        }
    }
}
