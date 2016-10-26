using System;
using SwinGameSDK;

namespace MyGame
{
    public abstract class Enemy: Entity
    {
        public Enemy() : this (WorldAnchor.Instance)
        {}

        public Enemy (Renderable anchor) : base(anchor)
        {
			Speed = 1;
			Width = 32;
			Height = 32;
        }

        public bool CollidingWithPlayer()
        {
            Player player = GameLogic.Player;
            Rectangle enemyRect = SwinGame.CreateRectangle(Pos.X, Pos.Y, Width, Height);
            Rectangle playerRect = SwinGame.CreateRectangle(player.Pos.X, player.Pos.Y, player.Width, player.Height);
            return SwinGame.RectanglesIntersect(enemyRect, playerRect);
        }

        public abstract void GetPath();

        /// <summary>
        /// Indicates if the AI has reached its target Tile. If it has, a new target Tile is fetched.
        /// This method also handles the actual movement.
        /// </summary>
		public bool MoveToTile(Tile tile)
        {
            if (CollidingWithPlayer())
            {
                GameLogic.deaths++;
                LevelIO.LoadAllLevels();
                return false;
            }
			bool collision;
			int x;
			int y;
			//Original Code.
			/*
            if (tile.Pos.X > Pos.X)
                Pos = SwinGame.PointAt(Pos.X + _speed, Pos.Y);
            else if (tile.Pos.X < Pos.X)
                Pos = SwinGame.PointAt(Pos.X - _speed, Pos.Y);

            if (tile.Pos.Y > Pos.Y)
                Pos = SwinGame.PointAt(Pos.X, Pos.Y + _speed);
            else if (tile.Pos.Y < Pos.Y)
                Pos = SwinGame.PointAt(Pos.X, Pos.Y - _speed);
			*/

            /// <summary>
            /// Represents the X-Axis direction to move (i.e. Left / Still / Right).
            /// </summary>
			x = 0;
			if (tile.Pos.X > Pos.X)
				x = 1;
			else if (tile.Pos.X < Pos.X)
				x = -1;

            /// <summary>
            /// Represents the Y-Axis direction to move (i.e. Up / Still / Down);
            /// </summary>
			y = 0;
			if (tile.Pos.Y > Pos.Y)
				y = 1;
			else if (tile.Pos.Y < Pos.Y)
				y = -1;
				
			collision = MoveOrCollide(MoveSpeed * x, MoveSpeed * y);

			return collision;
        }
    }
}
