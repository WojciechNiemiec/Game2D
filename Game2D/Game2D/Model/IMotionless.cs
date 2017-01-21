using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;

namespace Game2D.Model
{
    public interface IMotionless
    {
        void Draw(RenderWindow windowHandler, int xOffset, int yOffset);
    }
}
