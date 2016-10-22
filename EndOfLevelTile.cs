using System;
namespace MyGame
{
    public class EndOfLevelTile : Tile
    {
        public override void ApplyTileEffect(Player p)
        {
            GameLogic.NextLevel();
        }
    }
}