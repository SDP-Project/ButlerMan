using NUnit.Framework;
using System;
using SwinGameSDK;
using MyGame;

namespace UnitTests
{
    [TestFixture]
    public class WandererUnitTests
    {
        Player p;
        Wanderer w;
        Tileset t;

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
        public void TestMoveRight()
        {
            w.Move(1, 0);

            Assert.AreEqual(1, w.Pos.X);
            Assert.AreEqual(0, w.Pos.Y);
        }

        [Test]
        public void InitialisationTest()
        {
            Assert.AreEqual(0, w.Pos.X);
            Assert.AreEqual(0, w.Pos.Y);
        }

        [Test]
        public void TestChangePosition()

        {
            w.Pos.Add(new Position(50, 100));

            Assert.AreEqual(50, w.Pos.X);
            Assert.AreEqual(100, w.Pos.Y);
        }

        [Test]
        public void MovementTest()
        {

            w.Pos = new Position(200, 200);
            w.Direction = Direction.SouthEast;
            w.TargetTile = TileInteractor.Tileset.Tiles[1][1];

            Position oldPos = new Position(w.Pos.X, w.Pos.Y);

            w.Step();

            Assert.AreNotEqual(oldPos.X, w.Pos.X);
            Assert.AreNotEqual(oldPos.Y, w.Pos.Y);
        }

        [Test]
        public void TestGetsTargetTile()
        {
            w.Pos = new Position(200, 200);
            w.Direction = Direction.SouthEast;
            w.TargetTile = w.GetTargetTile();

            Assert.IsNotNull(w.TargetTile);
        }
    }
}