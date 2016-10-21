using System;
using System.Collections.Generic;

namespace MyGame
{
	public class FloodFill
	{
		private Dictionary<WaterTile, int> _tiles;
		private Tile _source;
		private static int MAX_WATER_HEIGHT = 100;

		public FloodFill (Tile source)
		{
			_source = source;

			AddTile (source);
		}

		public void AddTile (Tile t)
		{
			if (t != null && !t.IsWall && !_tiles.ContainsKey((WaterTile)TileInteractor.TileAt(t.Pos)))
			{
				WaterTile newTile = new WaterTile ();
				TileInteractor.ReplaceTileAt (t.Pos, newTile);
				_tiles [newTile] = 1;
			}
		}

		public void Increment ()
		{
			foreach (KeyValuePair<WaterTile, int> kvp in _tiles)
				if (kvp.Value < MAX_WATER_HEIGHT)
					_tiles[kvp.Key] = kvp.Value + 1; // Accessing by index is needed to update the value.
		}

		public void Expand ()
		{
			foreach (KeyValuePair<WaterTile, int> kvp in _tiles)
				if (kvp.Value == MAX_WATER_HEIGHT)
					AddNeighbours (kvp.Key);
		}

		public void AddNeighbours (Tile t)
		{
			AddTile (TileInteractor.TileAt (Direction.North, t.Pos));
			AddTile (TileInteractor.TileAt (Direction.East, t.Pos));
			AddTile (TileInteractor.TileAt (Direction.South, t.Pos));
			AddTile (TileInteractor.TileAt (Direction.West, t.Pos));
		}

		public Tile Source
		{
			get { return _source; }
		}

		public Dictionary<WaterTile, int> Tiles
		{
			get { return _tiles; }
			set { _tiles = value; }
		}
	}
}
