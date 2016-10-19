using System;
using SwinGameSDK;

namespace MyGame
{
	public class BitmapRenderable : Renderable
	{
		private Bitmap _img;

		public BitmapRenderable (Renderable anchor) : base (anchor)
		{
			_img = null;
		}

		public Bitmap Img
		{
			get { return _img; }
			set { _img = value; }
		}

		public override void Render()
		{
			if (Img != null)
			{
				SwinGame.DrawBitmap(_img, AbsPos.X, AbsPos.Y);
			}
		}
	}
}