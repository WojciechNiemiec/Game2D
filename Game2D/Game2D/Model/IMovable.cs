using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;

namespace Game2D.Model
{
    public interface IMovable
    {
        void GetAction();
        void Move();
        void CheckCollision(MainCharacter Collider);
        void CheckCollision(Ghost Collider);
        void CheckCollision(Ground Collider);
        void Draw(RenderWindow windowHandler, int xOffset, int yOffset);
        bool GetIsSituated();
        void CheckCollision(IMotionless collider);
    }
}
