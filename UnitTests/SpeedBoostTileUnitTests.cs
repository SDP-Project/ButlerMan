using NUnit.Framework;
using System;
using MyGame;

namespace UnitTests
{
    [TestFixture]
    public class SpeedBoostTileUnitTests
    {
        SpeedTile s;
        Player p;

        [SetUp]
        public void Init()
        {
            s = new SpeedTile(1);
            p = new Player();
        }

        [Test]
        public void TestSpeedsUpPlayer()
        {
            Assert.AreEqual(3, p.MoveSpeed);
            s.ApplyTileEffect(p);
            Assert.AreEqual(4, p.MoveSpeed);
        }

        [Test]
        public void TestSlowsDownPlayer()
        {
            s.Power = -1;

            Assert.AreEqual(3, p.MoveSpeed);
            s.ApplyTileEffect(p);
            Assert.AreEqual(2, p.MoveSpeed);
        }

        [Test]
        public void TestIsATile()
        {
            Assert.IsNotNull(s as Tile);
        }
    }
}