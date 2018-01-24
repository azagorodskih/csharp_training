using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace addressbook_web_tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
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
        }
    }
}
