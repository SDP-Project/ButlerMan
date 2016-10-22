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
        }

        public int Power
        {
            get {return _power;}
            set {_power = value;}
        }

        public Color Color
        {
            get {return _color;}
            set {_color = value;}
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