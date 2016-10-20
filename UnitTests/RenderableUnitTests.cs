using NUnit.Framework;
using System;
using SwinGameSDK;
using static SwinGameSDK.SwinGame;
using MyGame;

namespace UnitTests
{
    [TestFixture]
    public class RenderableUnitTests
    {
        Renderable r;
        WorldAnchor world = new WorldAnchor();
        ScreenAnchor screen = new ScreenAnchor();

        [SetUp]
        public void Init()
        {
            WorldAnchor.Instance.Pos = new Position(0, 0);
        }

        [Test]
        public void TestScreenSingleton()
        {
            TestDelegate makeTwoScreens = new TestDelegate(() => new WorldAnchor());
            Assert.Throws(typeof(InvalidOperationException), makeTwoScreens);
        }

        [Test]
        public void TestWorldSingleton()
        {
            TestDelegate makeTwoWorlds = new TestDelegate(() => new WorldAnchor());
            Assert.Throws(typeof(InvalidOperationException), makeTwoWorlds);
        }

        [Test]
        public void TestDefaultWorldAnchor()
        {
            r = new Renderable();
            Assert.AreEqual(WorldAnchor.Instance, r.Anchor);
        }

        [Test]
        public void TestSpecifyAnchor()
        {
            r = new Renderable(ScreenAnchor.Instance);
            Assert.AreEqual(ScreenAnchor.Instance, r.Anchor);
        }

        [Test]
        public void TestSpecifyPos()
        {
            r = new Renderable(new Position(100, 100));
            Assert.AreEqual(100, r.Pos.X);
            Assert.AreEqual(100, r.Pos.Y);
        }

        [Test]
        public void TestWorldAbsPos()
        {
            r = new Renderable(new Position(100, 100));
            WorldAnchor.Instance.Pos.Add(new Position(50, 50));

            Assert.AreEqual(150, r.AbsPos.X);
            Assert.AreEqual(150, r.AbsPos.Y);
        }

        [Test]
        public void TestScreenAbsPos()
        {
            r = new Renderable(ScreenAnchor.Instance, new Position(100, 100));
            WorldAnchor.Instance.Pos.Add(new Position(50, 50));

            Assert.AreEqual(100, r.AbsPos.X);
            Assert.AreEqual(100, r.AbsPos.Y);
        }
    }
}