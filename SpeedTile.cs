using System;
using SwinGameSDK;

namespace MyGame
{
    public class SpeedTile : Tile
    {
        private static int SPEED_DURATION = 2;
        private int _power;
        private Color _color;

        public SpeedTile(int power) : this (power, WorldAnchor.Instance)
        {           
        }

        public SpeedTile(int power, Renderable anchor) : base(anchor)
        {
            _power = power;

            //if (_power >= 0)
            //{
                //_color = SwinGame.RGBAColor(0, 255, 0, 80); //Green
           // }
           // else
           // {
          //      _color = SwinGame.RGBAColor(255, 0, 0, 80); //Red
            //}
        }

        public int Power
        {
            get {return _power;}
            set {_power = value;}
        }

        public override void ApplyTileEffect(Player p)
        {
            p.StartBoost(SPEED_DURATION, _power);
        }

        public override void Render()
        {
            base.Render();
            SwinGame.FillRectangle(_color, AbsPos.X, AbsPos.Y, Renderer.TILE_SIZE, Renderer.TILE_SIZE);
        }
    }
}