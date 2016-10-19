using System;
using System.IO;
using System.Runtime;
using SwinGameSDK;

namespace MyGame
{
    public class Wanderer: Enemy
    {
        private static Random _rand = new Random();
        private Tile _targetTile;
		private Direction _direction;

        public Wanderer()
		{
            _targetTile = null;
        }

		public override void Step()
        {
			Wander ();
        }

		private void Wander ()
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

            // If AI does not have a target Tile or has reached its target Tile,  get a new one.
            if (_targetTile == null || (X == tileX && Y == tileY))
			{
				GetPath();
			}

            /// <summary>
            /// Checks if the AI has reached its target Tile.
            /// MOVEMENT IS ALSO HANDLED HERE.
            /// </summary>
			if (MoveToTile (_targetTile))
			{
				_targetTile = null;
			}
		}

        /// <summary>
        /// Redundant method - leaving here in case it's required.
        /// </summary>
		/*public void GetMove ()
		{
			if (TileInteractor.CollisionAt(SwinGame.PointAt(Pos.X, Pos.Y), Width, Height) || _targetTile == null)
				GetPath ();
			else
				Move(_targetTile);
		}*/

        /// <summary>
        /// Fetches a random, adjacent Tile and assigns it as the target Tile.
        /// </summary>
        public override void GetPath()
        {
            _targetTile = null;

            while (_targetTile == null || _targetTile.IsWall)
            {
                _direction = GetRandomDirection();
                _targetTile = GetTargetTile();
            }
        }

        private Direction GetRandomDirection()
        {
            Direction[] dirs = (Direction[])Enum.GetValues(typeof(Direction));
            int rand = _rand.Next(dirs.Length);
            return (Direction)dirs.GetValue(rand);
        }

        public Tile GetTargetTile()
        {
            if (_direction == Direction.North)
                return TileInteractor.TileAt(AbsPos.X, AbsPos.Y - Renderer.TILE_SIZE);
            else if (_direction == Direction.NorthEast)
                return TileInteractor.TileAt(AbsPos.X + Renderer.TILE_SIZE, AbsPos.Y - Renderer.TILE_SIZE);
            else if (_direction == Direction.East)
                return TileInteractor.TileAt(AbsPos.X + Renderer.TILE_SIZE, AbsPos.Y);
            else if (_direction == Direction.SouthEast)
                return TileInteractor.TileAt(AbsPos.X + Renderer.TILE_SIZE,  AbsPos.Y + Renderer.TILE_SIZE);
            else if (_direction == Direction.South)
                return TileInteractor.TileAt(AbsPos.X, AbsPos.Y + Renderer.TILE_SIZE);
            else if (_direction == Direction.SouthWest)
                return TileInteractor.TileAt(AbsPos.X - Renderer.TILE_SIZE, AbsPos.Y + Renderer.TILE_SIZE);
            else if (_direction == Direction.West)
                return TileInteractor.TileAt(AbsPos.X - Renderer.TILE_SIZE, AbsPos.Y);
            else
                return TileInteractor.TileAt(AbsPos.X - Renderer.TILE_SIZE, AbsPos.Y - Renderer.TILE_SIZE);
        }

		public Direction Direction
		{
			get { return _direction; }
			set { _direction = value; }
		}

		public Tile TargetTile
		{
			get { return _targetTile; }
			set { _targetTile = value; }
		}
    }
}