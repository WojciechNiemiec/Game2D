using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Game2D.Controler;
using Game2D.Model;
using Game2D.Model.Components;

namespace Game2DUnitTests
{
    [TestClass]
    public class ComputerTest
    {
        [TestMethod]
        public void LevelContextConstructorTest()
        {
            Textures.init();
            string path = "../../../Game2D/Controler/Levels/Level1.txt";

            Computer.LevelContext context = new Computer.LevelContext(path);
        }
    }
}
