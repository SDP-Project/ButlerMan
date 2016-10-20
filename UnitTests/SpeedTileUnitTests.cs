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

        [SetUp]
        public void Init()
        {
            t = new SpeedTile(5);
        }

        [Test]
        public void TestIsSpeedTile()
        {
            Assert.AreEqual(t.GetType(), typeof(SpeedTile));
        }

        [Test]
        public void TestPlayerSpeedUpOnTile()
        {

        }

        [Test]
        public void TestPlayerNoSpeedUpOnNoTile()
        {

        }

        [Test]
        public void TestEnemySpeedUpOnTile()
        {

        }

        [Test]
        public void TestEnemyNoSpeedUpOnNoTile()
        {

        }

        [Test]
        public void TestPlayerSlowDownOnTile()
        {

        }

        [Test]
        public void TestEnemySlowDownOnTile()
        {

        }
    }
}