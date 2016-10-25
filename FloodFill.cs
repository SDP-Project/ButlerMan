using System;
using System.Collections.Generic;
using SwinGameSDK;
using System.Linq;

namespace MyGame
{
	public class FloodFill
	{
		private Dictionary<Tile, Double> _tiles = new Dictionary<Tile, Double> ();
		private Tile _source;
		private static Double MAX_WATER_HEIGHT = Double.MaxValue;

		public FloodFill (Tile source)
		{
			_source = source;

			AddTile (source);
		}

		public void AddTile (Tile t)
		{
			if (ValidTile(t)) {
				WaterTile newTile = new WaterTile ();
                newTile.Img = SwinGame.BitmapNamed("Water");
                newTile.Pos = t.Pos;
				TileInteractor.ReplaceTileAt (t.Pos, newTile);
				_tiles [newTile] = 0;
			}
		}

	    public bool ValidTile(Tile t)
	    {
	        return t != null && !t.IsWall &&
	               !_tiles.ContainsKey(TileInteractor.TileAt(t.Pos) && t.GetType() != typeof(SpeedTile) &&
	                                   t.GetType() != typeof(EndOfLevelTile));
	    }

		public void Increment ()
		{
			foreach (Tile key in _tiles.Keys.ToList ())
				if (_tiles [key] < MAX_WATER_HEIGHT)
					_tiles [key] = _tiles [key] + 1;
		}

		public void Expand ()
		{
			foreach (Tile key in _tiles.Keys.ToList ())
				if (_tiles [key] < MAX_WATER_HEIGHT)
					AddNeighbours (key);
		}

		public void AddNeighbours (Tile t)
		{
			// TODO: Refactor to either spread to all surrounding tiles, including diagonals
			// OR add below tiles iteratively.
			AddTile (TileInteractor.TileAt (Direction.North, t.Pos));
			AddTile (TileInteractor.TileAt (Direction.East, t.Pos));
			AddTile (TileInteractor.TileAt (Direction.South, t.Pos));
			AddTile (TileInteractor.TileAt (Direction.West, t.Pos));
		}

		public Tile Source {
			get { return _source; }
		}

		public Dictionary<Tile, Double> Tiles {
			get { return _tiles; }
			set { _tiles = value; }
		}
	}
}