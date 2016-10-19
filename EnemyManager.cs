using System;
using System.Linq;
using SwinGameSDK;

namespace MyGame
{
    public class EnemyManager
    {
        private EnemyType _placingType;
        private Level _level;
        private Patroller _selectedPatroller;

        public EnemyManager()
        {
            _placingType = EnemyType.Wanderer;
            _selectedPatroller = null;
        }

        public Patroller SelectedPatroller
        {
            get {return _selectedPatroller;}
            set {_selectedPatroller = value;}
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
                    Entity toAdd;
                    switch (_placingType)
                    {
                        case EnemyType.Wanderer:
                        {
                            toAdd = new Wanderer();
                            break;
                        }

                        case EnemyType.WallFollower:
                        {
                            toAdd = new WallFollower();
                            break;
                        }

                        case EnemyType.Patroller:
                        {
                            toAdd = new Patroller();
                            _selectedPatroller = toAdd as Patroller;
                            break;
                        }

                        default:
                        {
                            toAdd = new Wanderer();
                            break;
                        }
                    }
                   
                    toAdd.Pos = Level.Tileset.TileAt(mousePos).Pos;
                    Level.AddEntity(toAdd);
                }
                else if (Level.HasEntityAt(mousePos))
                {
                    if (Level.EntityAt(mousePos).GetType() == typeof(Patroller))
                    {
                        _selectedPatroller = Level.EntityAt(mousePos) as Patroller;
                        Renderer.Register(_selectedPatroller);
                    }
                }
            }

            if (SwinGame.MouseClicked(MouseButton.RightButton))
            {
                if (Level.Tileset.IsAt(SwinGame.MousePosition()))
                {
                }
            }

            if (SwinGame.KeyTyped(KeyCode.SKey))
            {
                _placingType++;

                if ((int)_placingType > 2)
                {
                    _placingType = 0;
                }
            }

            if (SwinGame.KeyTyped(KeyCode.AKey))
            {
                if (_placingType == EnemyType.Patroller)
                {
                    if (_selectedPatroller != null && DirectRouteToTile())
                    {
                        _selectedPatroller.AddTileToPath(TileInteractor.TileAt(SwinGame.MouseX(), SwinGame.MouseY()), Level.Tileset);
                    }
                }
            }
        }

        public bool DirectRouteToTile()
        {
            if (_selectedPatroller.Path.Count == 0)
                return true;

            if (TileInteractor.Tileset.TileAt(SwinGame.MousePosition()).IsWall)
                return false;
            
            int smaller, bigger;

            int newTileCol = (int)TileInteractor.GetTileIndexFromPt(SwinGame.MousePosition()).X;
            int newTileRow = (int)TileInteractor.GetTileIndexFromPt(SwinGame.MousePosition()).Y;

            int linksFromTileCol = (int)TileInteractor.GetTileIndexFromPt(SwinGame.PointAt(_selectedPatroller.Path.Last().AbsPos.X + 1, _selectedPatroller.Path.Last().AbsPos.Y + 1)).X;
            int linksFromTileRow = (int)TileInteractor.GetTileIndexFromPt(SwinGame.PointAt(_selectedPatroller.Path.Last().AbsPos.X + 1, _selectedPatroller.Path.Last().AbsPos.Y + 1)).Y;

            if (newTileCol != linksFromTileCol && newTileRow != linksFromTileRow)
            {
                return false;
            }

            if (newTileCol == linksFromTileCol)
            {
                if (newTileRow < linksFromTileRow)
                {
                    smaller = newTileRow;
                    bigger = linksFromTileRow;
                }
                else
                {
                    smaller = linksFromTileRow;
                    bigger = newTileRow;
                }

                for (int i = smaller; i <= bigger; i++)
                {
                    if (TileInteractor.Tileset.Tiles[newTileCol][i].IsWall)
                        return false;
                }
                return true;
            }

            //New Tile Row is equal to Links From Tile Row
            if (newTileCol < linksFromTileCol)
            {
                smaller = newTileCol;
                bigger = linksFromTileCol;
            }
            else
            {
                smaller = linksFromTileCol;
                bigger = newTileCol;
            }

            for (int i = smaller; i <= bigger; i++)
            {
                if (TileInteractor.Tileset.Tiles[i][newTileRow].IsWall)
                    return false;
            }
            return true;
        }

        public void RenderPatrollerPath()
        {
            if (_selectedPatroller != null)
            {
                for (int i = 0; i < _selectedPatroller.Path.Count; i++)
                {
                    SwinGame.DrawText(i.ToString(), Color.Black, _selectedPatroller.Path[i].AbsPos.X + 10, _selectedPatroller.Path[i].AbsPos.Y + 10);
                }
            }
        }

        public void Render()
        {
            /// <summary>
            /// When there are more enemy types with proper Bitmaps, thumbnail code will be here.
            /// </summary>
            //SwinGame.DrawBitmap(enemyBmp, SwinGame.MouseX(), SwinGame.MouseY());

            SwinGame.DrawText("Currently Placing: " + _placingType, Color.Black, 10, 10);
            SwinGame.DrawText("(S) Cycle Enemy Types", Color.Black, 10, 25);

            if (_placingType == EnemyType.Patroller)
            {
                RenderPatrollerPath();
                SwinGame.DrawText("(A) Add a new node to Patrol path", Color.Black, 10, 40);
            }
        }
    }
}