using System;

namespace MyGame
{
    public class Position
    {
        private float _x;
        private float _y;

        public Position(float x, float y)
        {
            _x = x;
            _y = y;
        }

        public float X
        {
            get { return _x; }
            set { _x = value; }
        }

        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public int col
        {
            get { return (int)Math.Truncate((_x - TileInteractor.Tileset.AbsPos.X) / Renderer.TILE_SIZE); }
            set { _x = value; }
        }

        public int row
        {
            get { return (int)Math.Truncate((_y - TileInteractor.Tileset.AbsPos.Y) / Renderer.TILE_SIZE); }
            set { _y = value; }
        }

        public Tuple<float, float> Pos
        {
            get { return Tuple.Create(_x, _y); }
            set
            {
                _x = value.Item1;
                _y = value.Item2;
            }
        }
    }
}