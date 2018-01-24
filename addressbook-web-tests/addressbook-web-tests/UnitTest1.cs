using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAddressbookTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodSquare()
        {
            Square s1 = new Square(4);
            Square s2 = new Square(17);
            Square s3 = s1;

            //Assert.AreEqual(s1.getSize(), 4);
            //Assert.AreEqual(s2.getSize(), 17);
            //Assert.AreEqual(s3.getSize(), 4);

            Assert.AreEqual(s1.Size, 4);
            Assert.AreEqual(s2.Size, 17);
            Assert.AreEqual(s3.Size, 4);

            s3.Size = 10;
            Assert.AreEqual(s1.Size, 10);

            s2.Colored = true;
        }

        [TestMethod]
        public void TestMethodCircle()
        {
            Circle s1 = new Circle(4);
            Circle s2 = new Circle(17);
            Circle s3 = s1;

            Assert.AreEqual(s1.Radius, 4);
            Assert.AreEqual(s2.Radius, 17);
            Assert.AreEqual(s3.Radius, 4);

            s3.Radius = 10;
            Assert.AreEqual(s1.Radius, 10);

            s2.Colored = true;
        }
    }
}
