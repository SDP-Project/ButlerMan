using NUnit.Framework;
using System;
using SwinGameSDK;
using MyGame;

namespace UnitTests
{
    [TestFixture]
    public class WandererUnitTests
    {
        Wanderer w;
        Tileset t;

        [SetUp]
        public void Init()
        {
            w = new Wanderer();
            w.Pos = new Position(32, 32);
            t = new Tileset();

            for (int i = 0; i < 10; i++) {
                t.AddColumn ();
                t.AddRow ();
            }

            TileInteractor.Tileset = t;
        }

        [Test]
        public void InitialisationTest()
        {
            Assert.AreEqual(32, w.Pos.X);
            Assert.AreEqual(32, w.Pos.Y);
        }

        [Test]
        public void TestChangePosition()

        {
            w.Pos.Add(new Position(50, 100));

            Assert.AreEqual(82, w.Pos.X);
            Assert.AreEqual(132, w.Pos.Y);
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