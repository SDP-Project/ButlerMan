using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace MyGame
{
    public class TileManager
    {
        private TileType _placingType;
        private Tile _holding;
        private Tileset _tileset;
        private Bitmap[] _bitmaps;
        private int _activeBmp;
        private int _totalBmps;

        public TileManager()
        {
            _placingType = TileType.Normal;
            _bitmaps = GameResources.GetBitmapListNamed("DungeonTileset");
            _activeBmp = 0;
            _totalBmps = _bitmaps.Length - 1;
            _holding = new Tile(ScreenAnchor.Instance);
            _holding.IsWall = true;
        }

        public Tile Holding
        {
            get {return _holding;}
            set {_holding = value;}
        }

        public Tileset Tileset
        {
            get {return _tileset;}
            set {_tileset= value;}
        }

        public void PlaceTo(Tileset tileset, Position pos)
        {
            Tile newTile;

            switch (_placingType)
            {
                case TileType.Normal:
                {
                    newTile = new Tile(tileset);
                    newTile.IsWall = true;
                    newTile.Img = _bitmaps[_activeBmp];
                    newTile.RootBitmap = "DungeonTileset";
                    newTile.RootIndex = _activeBmp;
                    break;
                }

                case TileType.SpeedUp:
                {
                    newTile = new SpeedTile(2, tileset);
                    newTile.IsWall = false;
                    newTile.Img = SwinGame.BitmapNamed("SpeedUp");
                    break;
                }

                case TileType.SpeedDown:
                {
                    newTile = new SpeedTile(-2, tileset);
                    newTile.IsWall = false;
                    newTile.Img = SwinGame.BitmapNamed("SpeedDown");
                    break;
                }

                case TileType.Water:
                {
                    newTile = new WaterTile();
                    newTile.IsWall = false;
                    newTile.Img = SwinGame.BitmapNamed("Water");
                    break;
                }

                default:
                {
                    newTile = new Tile(tileset);
                    newTile.IsWall = true;
                    break;
                }
            }
            tileset.TileAt(pos).Deregister();
            newTile.Pos = tileset.TileAt(pos).Pos;

            tileset.ReplaceTileAt(pos, newTile);
            tileset.Deregister();
            tileset.Register();
        }

        public void Remove(Tileset tileset, Position pos)
        {
            tileset.TileAt(pos).Deregister();

            Tile newTile = new Tile();
            newTile.Pos = tileset.TileAt(pos).Pos;
            newTile.IsWall = false;
            newTile.Img = null;
            newTile.RootBitmap = "nullBmp";
            newTile.RootIndex = 0;

            tileset.ReplaceTileAt(pos, newTile);
            tileset.Deregister();
            tileset.Register();
        }

        public void HandleInput()
        {
            if (SwinGame.KeyTyped(KeyCode.RKey))
            {
                GameLogic.ActiveLevel.Floods.Clear();
            }

            if (SwinGame.KeyTyped(KeyCode.FKey))
            {
                if (Tileset.IsAt(new Position(SwinGame.MouseX(), SwinGame.MouseY())))
                {
                    if (Tileset.TileAt(new Position(SwinGame.MouseX(), SwinGame.MouseY())) as WaterTile != null)
                    {
                        GameLogic.ActiveLevel.AddFloodFill(new FloodFill(Tileset.TileAt(new Position(SwinGame.MouseX(), SwinGame.MouseY()))));
                    }
                }
            }

            if (SwinGame.KeyTyped(KeyCode.SKey))
            {
                _placingType++;

                if ((int)_placingType > 4)
                {
                    _placingType = 0;
                }
            }

            if (SwinGame.MouseDown(MouseButton.LeftButton))
            {
                if (Tileset.IsAt(new Position(SwinGame.MouseX(), SwinGame.MouseY())))
                {
                    PlaceTo(Tileset, new Position(SwinGame.MouseX(), SwinGame.MouseY()));
                }
            }

            if (SwinGame.MouseDown(MouseButton.RightButton))
            {
                if (Tileset.IsAt(new Position(SwinGame.MouseX(), SwinGame.MouseY())))
                {
                    Remove(Tileset, new Position(SwinGame.MouseX(), SwinGame.MouseY()));
                }
            }

            if (SwinGame.KeyTyped(KeyCode.RightKey))
            {
                if (_activeBmp < _totalBmps)
                {
                    _activeBmp++;
                }
                else
                {
                    _activeBmp = 0;
                }
            }

            if (SwinGame.KeyTyped(KeyCode.LeftKey))
            {
                if (_activeBmp > 0)
                {
                    _activeBmp--;
                }
                else
                {
                    _activeBmp = _totalBmps;
                }
            }
        }

        public void Render()
        {
            SwinGame.DrawBitmap(_bitmaps[_activeBmp], SwinGame.MouseX(), SwinGame.MouseY());

            SwinGame.DrawText("Currently Placing: ", Color.Black, 10, 10);
            SwinGame.DrawText(_placingType.ToString(), Color.Black, 175, 10);

            SwinGame.DrawText("(S) Toggle Tile Type", Color.Black, 10, 25);

            if (_placingType == TileType.Water)
            {
                SwinGame.DrawText("(F) Place FloodFill over Water Tile", Color.Black, 10, 40);
                SwinGame.DrawText("(R) Remove all FloodFills", Color.Black, 10, 55);
            }
        }
    }
}