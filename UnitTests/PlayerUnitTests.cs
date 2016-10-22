using NUnit.Framework;
using System;
using SwinGameSDK;
using MyGame;

namespace UnitTests
{
    [TestFixture]
    public class PlayerUnitTests
    {
        Player p;
        Tileset t;
        Wanderer w;

        [SetUp]
        public void Init()
        {
            p = new Player();
            p.Pos = new Position(32, 32);
            w = new Wanderer();
            w.Pos = new Position(0, 0);
            t = new Tileset();

            for (int i = 0; i < 10; i++) {
                t.AddColumn ();
                t.AddRow ();
            }

            TileInteractor.Tileset = t;
        }

        [Test]
        public void TestPosition()
        {
            Assert.AreEqual(32, p.Pos.X);
            Assert.AreEqual(32, p.Pos.Y);
        }

        [Test]
        public void TestPlayerPosition()
        {
            p.Move(1, 0);

            Assert.AreEqual(33, p.Pos.X);
            Assert.AreEqual(32, p.Pos.Y);
        }

        [Test]
        public void TestMoveUp()
        {
            p.Move(0, -1);

            Assert.AreEqual(32, p.Pos.X);
            Assert.AreEqual(31, p.Pos.Y);
        }

        [Test]
        public void TestMoveRight()
        {
            p.Move(1, 0);

            Assert.AreEqual(33, p.Pos.X);
            Assert.AreEqual(32, p.Pos.Y);
        }

        [Test]
        public void TestMoveDown()
        {
            p.Move(0, 1);

            Assert.AreEqual(32, p.Pos.X);
            Assert.AreEqual(33, p.Pos.Y);
        }

        [Test]
        public void TestMoveLeft()
        {
            p.Move(-1, 0);

            Assert.AreEqual(31, p.Pos.X);
            Assert.AreEqual(32, p.Pos.Y);
        }

        [Test]
        public void TestCantMoveThroughWall()
        {
            p.Pos = new Position(0, 0);
            TileInteractor.Tileset.Tiles[1][0].IsWall = true;

            Position oldPos = new Position(p.Pos.X, p.Pos.Y);

            p.Move(1, 0);

            Assert.AreEqual(oldPos.X, p.Pos.X);
            Assert.AreEqual(oldPos.Y, p.Pos.Y);
        }
    }
}