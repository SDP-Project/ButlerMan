using System;
namespace MyGame
{
    public class EndOfLevelTile : Tile
    {
        public EndOfLevelTile() : this (WorldAnchor.Instance)
        {}

        public EndOfLevelTile(Renderable anchor) : base(anchor)
        {}

        public override void ApplyTileEffect(Player p)
        {
            GameLogic.NextLevel();
        }
    }
}