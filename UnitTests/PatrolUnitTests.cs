using NUnit.Framework;
using System;
using SwinGameSDK;
using MyGame;

namespace UnitTests
{
    [TestFixture]
    public class PatrolUnitTests
    {
        Patroller p;
        Tileset t;

        [SetUp]
        public void Init()
        {
            p = new Patroller();
            p.Pos = new Position(32, 32);
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
            Assert.AreEqual(32, p.Pos.X);
            Assert.AreEqual(32, p.Pos.Y);
        }

        [Test]
        public void TestAddNode()
        {
            Tile toAdd = TileInteractor.TileAt(64, 64);
            p.AddTileToPath(toAdd, TileInteractor.Tileset);
            Assert.IsTrue(p.Path.Contains(toAdd));
        }

        [Test]
        public void TestDefaultAddCurrentTileToPath()
        {
            Tile toAdd = TileInteractor.TileAt(64, 64);
            Tile currentTile = TileInteractor.TileAt(33, 33);
            p.AddTileToPath(toAdd, TileInteractor.Tileset);
            Assert.IsTrue(p.Path.Contains(currentTile));
        }

        [Test]
        public void TestMovesToNextNode()
        {
            Tile firstNode = TileInteractor.TileAt(64, 64);
            Tile secondNode = TileInteractor.TileAt(96, 96);
            p.AddTileToPath(firstNode, TileInteractor.Tileset);
            p.AddTileToPath(secondNode, TileInteractor.Tileset);

            for (int i = 0; i < 33; i++)
            {
                p.Step();
            }

            Assert.AreEqual(secondNode, p.TargetTile);
        }

        [Test]
        public void TestLoopsToOriginalPosition()
        {
            Tile firstNode = TileInteractor.TileAt(64, 64);
            Tile currentTile = TileInteractor.TileAt(33, 33);
            p.AddTileToPath(firstNode, TileInteractor.Tileset);

            for (int i = 0; i < 33; i++)
            {
                p.Step();
            }

            Assert.AreEqual(currentTile, p.TargetTile);
        }
    }
}