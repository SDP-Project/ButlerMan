﻿using System;

namespace MyGame
{
	public class WaterTile: Tile
	{
		public WaterTile () : this(WorldAnchor.Instance)
		{
		}

        public WaterTile(Renderable anchor) : base(anchor)
        {
        }
	}
}
