using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Game2D.Model.Components;

namespace Game2DUnitTests
{
    [TestClass]
    public class TextureTests
    {
        [TestMethod]
        public void InitTest()
        {
            Textures.init();
        }
    }
}
