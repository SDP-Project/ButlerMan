using System;
using SwinGameSDK;

namespace MyGame
{
    public class SpeedTile : Tile
    {
        private static int SPEED_DURATION = 2;
        private int _power;

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

        public override void ApplyTileEffect(Player p)
        {
            p.StartBoost(SPEED_DURATION, _power);
        }

        public override void Render()
        {
            base.Render();
        }
    }
}