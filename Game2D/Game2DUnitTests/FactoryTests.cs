using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Game2D.Controler;
using Game2D.Model;
using Game2D.Model.Components;

namespace Game2DUnitTests
{
    [TestClass]
    public class FactoryTests
    {
        [TestMethod]
        public void CreateRectangularObjectTest()
        {
            Textures.init();

            string objectDescriptor;
            object product;
            Rectangle rect;
            Type T;

            objectDescriptor = "Maincharacter 20 30 40 50";
            product = Factory.CreateRectangularObject(objectDescriptor);
            T = product.GetType();
            rect = ((MainCharacter)product).BodyRect;

            Assert.IsTrue(T.Equals(typeof(MainCharacter)));
            Assert.IsTrue(rect.Top == 20);
            Assert.IsTrue(rect.Left == 30);

            objectDescriptor = " OppOnEnt 10 10 ";
            product = Factory.CreateRectangularObject(objectDescriptor);
            T = product.GetType();
            rect = ((Ghost)product).Rect;

            Assert.IsTrue(product.GetType().Equals(typeof(Ghost)));
            Assert.IsTrue(T.Equals(typeof(Ghost)));
            Assert.IsTrue(rect.Top == 10);
            Assert.IsTrue(rect.Left == 10);
        }
    }
}
