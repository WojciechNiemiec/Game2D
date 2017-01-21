using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Game2D.Model;
using Game2D.Model.Components;

namespace Game2DUnitTests
{
    [TestClass]
    public class MainCharacterTests
    {
        [TestMethod]
        public void MoveAndGroundTest()
        {
            Textures.init();
            MainCharacter character = new MainCharacter(new Rectangle(200, 200));
            Ground ground = new Ground(new Rectangle(200, 139, 60, 60)); //WARNING: Size of Ground may be changed

            character.director = new MotionDirector(new MotionDirector.Vector(-3, -1));

            Assert.IsTrue(character.BodyRect.Left == 200);

            character.CheckCollision(ground);
            character.Move();

            Assert.IsTrue(character.BodyRect.Left == 199);

            character.CheckCollision(ground);
            character.Move();

            Assert.IsTrue(character.BodyRect.Left == 200);
            Assert.IsTrue(character.BodyRect.Top == 200);

            character.CheckCollision(ground);
            character.Move();

            Assert.IsTrue(character.BodyRect.Left == 200);
            Assert.IsTrue(character.BodyRect.Top == 199);

            character.CheckCollision(ground);
            character.Move();

            Assert.IsTrue(character.BodyRect.Left == 199);
            Assert.IsTrue(character.BodyRect.Top == 199);

            character.CheckCollision(ground);
            character.Move();

            Assert.IsTrue(character.BodyRect.Left == 200);
            Assert.IsTrue(character.BodyRect.Top == 199);
            
            character.CheckCollision(ground);
            character.Move();

            Assert.IsTrue(character.BodyRect.Left == 199);
            Assert.IsTrue(character.BodyRect.Top == 199);

            character.CheckCollision(ground);
            character.Move();

            Assert.IsTrue(character.BodyRect.Left == 200);
            Assert.IsTrue(character.BodyRect.Top == 199);

            character.CheckCollision(ground);
            character.Move();

            Assert.IsTrue(character.BodyRect.Left == 200);
            Assert.IsTrue(character.BodyRect.Top == 199);
        }
    }
}
