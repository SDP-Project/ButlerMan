/*using NUnit.Framework;
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
		TileLocator locator;

		[SetUp]
		public void Init()
		{
			w = new Wanderer(SwinGame.PointAt(0, 0));
			t = new Tileset();

			for (int i = 0; i < 10; i++) {
				t.AddColumn ();
				t.AddRow ();
			}

			locator = new TileLocator(t);
		}

		[Test]
		public void InitialisationTest()
		{
			Assert.AreEqual(SwinGame.PointAt(0, 0), w.Pos);
		}

		[Test]
		public void MovementTest()
		{
			w.TargetTile = TileLocator.TileAt(w.Pos.X + 1, w.Pos.Y + 1);

			w.Direction = Direction.SouthWest;

			while (!TileLocator.IsAt (w.Pos, w.TargetTile.Pos))
			{
				w.Move();
			}

			Assert.AreEqual(w.TargetTile.Pos, w.Pos);
		}
	}
}*/