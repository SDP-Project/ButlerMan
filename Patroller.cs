using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace MyGame
{
    public class Patroller : Enemy
    {
        private Tile _targetTile;
        private List<Tile> _path;
        private int _targetNode;

        public Patroller ()
        {
            _targetTile = null;
            _path = new List<Tile>();
            _targetNode = 0;
        }

        public Tile TargetTile
        {
            get {return _targetTile;}
        }

        public List<Tile> Path
        {
            get {return _path;}
        }

        public void AddTileToPath(Tile t, Tileset tileset)
        {
            //First Tile in path is Tile AI exists on
            if (_path.Count == 0)
            {
                _path.Add(tileset.TileAt(AbsPos.X + 1, AbsPos.Y + 1));
                _targetTile = tileset.TileAt(AbsPos.X + 1, AbsPos.Y + 1);
            }

            _path.Add(t);
        }

        public void DeletePath()
        {
            _path.Clear();
        }

        public override void Step()
        {
            int X = (int)Math.Truncate(AbsPos.X);
            int Y = (int)Math.Truncate(AbsPos.Y);

            int tileX = 0;
            int tileY = 0;

            /// <summary>
            /// Store target Tile positions for use in determining collisions.
            /// </summary>
            if (_targetTile != null)
            {
                tileX = (int) Math.Truncate(_targetTile.AbsPos.X);
                tileY = (int) Math.Truncate(_targetTile.AbsPos.Y);
            }

            // If AI has reached its target Tile, get a new one.
            if ((X == tileX && Y == tileY))
            {
                MoveToNextNode();
            }

            MoveToTile(_targetTile);
        }

        public void MoveToNextNode()
        {
            if (_targetNode == _path.Count - 1) //If currently at last Tile in path
            {
                _targetTile = _path[0];
                _targetNode = 0;
            }
            else //Target Tile becomes next Tile in path
            {
                _targetNode++;
                _targetTile = _path[_targetNode];
            }
        }

        /// <summary>
        /// Redundant method.
        /// </summary>
        public override void GetPath()
        {
        }
    }
}