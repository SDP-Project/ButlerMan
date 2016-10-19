using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace MyGame
{
    public class TileInteractor
    {
        private static Tileset _tileset;

        public static Tileset Tileset
        {
            get { return _tileset; }
            set { _tileset = value; }
        }

        public static Tile TileAt(float x, float y)
        {
			//THIS CHECK DOES NOT WORK
			/*
			if ((x < 0 || y < 0) || (x > Tileset.Cols || y > Tileset.Rows))
			return null;
			*/
			return Tileset.TileAt(x, y);
        }

        public static Tile TileAt(Direction dir, Point2D pos)
        {
            if (dir == Direction.North)
                return _tileset.TileAt(pos.X, pos.Y - 1);
            else if (dir == Direction.East)
                return _tileset.TileAt(pos.X + 1, pos.Y);
            else if (dir == Direction.South)
                return _tileset.TileAt(pos.X, pos.Y + 1);
            else
                return _tileset.TileAt(pos.X - 1, pos.Y);
        }

		public static Tile TileAt (Direction dir, Position pos)
		{
			if (dir == Direction.North)
				return _tileset.TileAt (pos.X, pos.Y - 1);
			else if (dir == Direction.East)
				return _tileset.TileAt (pos.X + 1, pos.Y);
			else if (dir == Direction.South)
				return _tileset.TileAt (pos.X, pos.Y + 1);
			else
				return _tileset.TileAt (pos.X - 1, pos.Y);
		}

        public static List<List<Tile>> Tiles
        {
            get {return _tileset.Tiles;}
        }

        public static bool CollisionAt(Point2D pt, int width, int height)
        {
            int col = (int)Math.Truncate((pt.X - Tileset.AbsPos.X) / Renderer.TILE_SIZE);
            int row = (int)Math.Truncate((pt.Y - Tileset.AbsPos.Y) / Renderer.TILE_SIZE);
            if (Tiles[col][row].IsWall)
            {return true;}

            col = (int)Math.Truncate((pt.X - Tileset.AbsPos.X + width-1) / Renderer.TILE_SIZE);
            row = (int)Math.Truncate((pt.Y - Tileset.AbsPos.Y) / Renderer.TILE_SIZE);
            if (Tiles[col][row].IsWall)
            {return true;}

            col = (int)Math.Truncate((pt.X - Tileset.AbsPos.X) / Renderer.TILE_SIZE);
            row = (int)Math.Truncate((pt.Y - Tileset.AbsPos.Y + height-1) / Renderer.TILE_SIZE);
            if (Tiles[col][row].IsWall)
            {return true;}

            col = (int)Math.Truncate((pt.X - Tileset.AbsPos.X + width-1) / Renderer.TILE_SIZE);
            row = (int)Math.Truncate((pt.Y - Tileset.AbsPos.Y + height-1) / Renderer.TILE_SIZE);
            if (Tiles[col][row].IsWall)
            {return true;}

            return false;
        }

        public static Point2D GetTileIndexAt(int col, int row)
        {
            return _tileset.GetTileIndexAt(col, row);
        }

        public static Point2D GetTileIndexFromPt(Point2D pt)
        {
            return Tileset.GetTileIndexFromPt(pt);
        }
    }
}