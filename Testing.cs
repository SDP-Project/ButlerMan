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
            Point2D first = SwinGame.PointAt(10, 10);
            Point2D second = SwinGame.PointAt(10, 10);

            Assert.AreEqual(first.X, second.X);
        }
    }
}
