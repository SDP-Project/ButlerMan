using System;
using SwinGameSDK;

namespace MyGame
{
    public class PlayerManager
    {
        private Player _holding;
        private Level _level;

        public PlayerManager()
        {
        }

        public Player Holding
        {
            get {return _holding;}
            set {_holding = value;}
        }

        public Level Level
        {
            get {return _level;}
            set {_level = value;}
        }

        public void HandleInput()
        {
            Point2D mousePos = SwinGame.MousePosition();
            if (SwinGame.MouseClicked(MouseButton.LeftButton))
            {
                if (Level.Tileset.IsAt(mousePos) && !Level.HasEntityAt(mousePos))
                {
                    _holding = new Player();
                    _holding.Pos = Level.Tileset.TileAt(mousePos).Pos;
                    Level.AddEntity(_holding);
                }
            }

            if (SwinGame.MouseClicked(MouseButton.RightButton))
            {
                if (Level.Tileset.IsAt(SwinGame.MousePosition()))
                {
                    if (_holding != null)
                    {
                        _holding.Deregister();
                    }
                }
            }
        }

        public void Render()
        {
            /// <summary>
            /// When we have Butler Sprite, thumbnail will go here
            /// </summary>
            //SwinGame.DrawBitmap(playerBmp, SwinGame.MouseX(), SwinGame.MouseY());

            SwinGame.DrawText("Currently Placing: Player", Color.Black, 10, 10);
            SwinGame.DrawText("Abilities: Speed Boost", Color.Black, 10, 30);
        }
    }
}