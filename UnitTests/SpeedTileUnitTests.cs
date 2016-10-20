using NUnit.Framework;
using System;
using SwinGameSDK;
using MyGame;

namespace UnitTests
{
    [TestFixture]
    public class SpeedTileUnitTests
    {
        SpeedTile t;
        Player p;

        [SetUp]
        public void Init()
        {
            t = new SpeedTile(1);
            p = new Player();
        }

        [Test]
        public void TestIsSpeedTile()
        {
            Assert.AreEqual(t.GetType(), typeof(SpeedTile));
        }

        [Test]
        public void TestNegativePower()
        {
            int power = t.Power;
            t.Power = -1;

            Assert.AreNotEqual(power, t.Power);
            Assert.AreEqual(-1, t.Power);
        }

        [Test]
        public void TestSpeedsUpPlayer()
        {
            float speed = p.Speed;
            t.ApplyTileEffect(p);
            Assert.AreEqual(4.0f, p.MoveSpeed);
        }

        [Test]
        public void TestSlowsDownplayer()
        {
            float speed = p.Speed;
            t.Power = -1;
            t.ApplyTileEffect(p);
            Assert.AreEqual(2.0f, p.MoveSpeed);
        }
    }
}