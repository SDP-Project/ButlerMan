using NUnit.Framework;
using System;
using SwinGameSDK;
using MyGame;

namespace UnitTests
{
    [TestFixture]
    public class PatrolUnitTests
    {
        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void TestNumbers()
        {
            int x = 5;
            int y = 5;

            Assert.AreEqual(x, y);
        }
    }
}
