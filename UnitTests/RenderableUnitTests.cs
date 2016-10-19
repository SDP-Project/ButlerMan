﻿using NUnit.Framework;
using System;
using SwinGameSDK;
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
            SwinGame.OpenGraphicsWindow("Renderable Tests", 800, 600);
            WorldAnchor.Instance.Pos = SwinGame.PointAt(0, 0);
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
            r = new Renderable(SwinGame.PointAt(100, 100));
            Assert.AreEqual(SwinGame.PointAt(100, 100), r.Pos);
        }

        [Test]
        public void TestWorldAbsPos()
        {
            r = new Renderable(SwinGame.PointAt(100, 100));
            WorldAnchor.Instance.Pos = SwinGame.AddVectors(WorldAnchor.Instance.Pos, SwinGame.PointAt(50, 50));

            Assert.AreEqual(SwinGame.PointAt(150, 150), r.AbsPos);
        }

        [Test]
        public void TestScreenAbsPos()
        {
            r = new Renderable(ScreenAnchor.Instance, SwinGame.PointAt(100, 100));
            WorldAnchor.Instance.Pos = SwinGame.AddVectors(WorldAnchor.Instance.Pos, SwinGame.PointAt(50, 50));

            Assert.AreEqual(SwinGame.PointAt(100, 100), r.AbsPos);
        }
    }
}