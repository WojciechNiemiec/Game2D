using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game2DUnitTests
{
    [TestClass]
    public class Draft
    {
        public interface inter
        {
            bool isEq(inter i);
            bool isEq(lol c);
            bool isEq(lel c);
        }

        public class lol: inter
        {
            public int x;

            public bool isEq(lel c)
            {
                return (c.y == x) ? true : false;
            }

            public bool isEq(lol c)
            {
                return (c.x == x) ? true : false;
            }

            public bool isEq(inter i)
            {
                throw new NotImplementedException();
            }

            public lol()
            {
                x = 5;
            }
        }

        public class lel: inter
        {
            public int y;

            public bool isEq(lel c)
            {
                return (c.y == y) ? true : false;
            }

            public bool isEq(lol c)
            {
                return (c.x == y) ? true : false;
            }

            public bool isEq(inter i)
            {
                throw new NotImplementedException();
            }

            public lel()
            {
                y = 5;
            }
        }

        [TestMethod]
        public void Test()
        {
            List<inter> list = new List<inter>();

            list.Add(new lol());
            list.Add(new lel());
            list.Add(new lel());
            list.Add(new lol());

            Assert.IsTrue(list[0].isEq((lel)list[1]));
        }
    }
}
