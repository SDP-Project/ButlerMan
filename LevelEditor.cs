using System;
using SwinGameSDK;

namespace MyGame
{
    public class LevelEditor : Renderable
    {
        public static LevelEditor _instance;

        private TileManager _tMan;
        private PlayerManager _pMan;
        private EnemyManager _eMan;
        private PlacingType _typeToPlace;
        private Level _level;

        public LevelEditor () : base(ScreenAnchor.Instance)
        {
            if (_instance == null)
            {
                _instance = this;
                _tMan = new TileManager();
                _pMan = new PlayerManager();
                _eMan = new EnemyManager();
                _typeToPlace = PlacingType.Tiles;
            }
            else
            {
                throw new InvalidOperationException("Cannot have more than one instance of Level Editor");
            }
             
        }

        public static LevelEditor Instance
        {
            get {return _instance;}
        }

        public PlacingType TypeToPlace
        {
            get {return _typeToPlace;}
            set {_typeToPlace = value;}
        }

        public Level Level
        {
            get {return _level;}
            set
            {
                _level = value;
                _tMan.Tileset = value.Tileset;
                _pMan.Level = value;
                _eMan.Level = value;
            }
        }

        public void HandleInput()
        {
            switch (TypeToPlace)
            {
                case PlacingType.Tiles: _tMan.HandleInput();
                break;

                case PlacingType.Player: _pMan.HandleInput();
                break;

                case PlacingType.Enemies: _eMan.HandleInput();
                break;
            }

            if (SwinGame.MouseClicked(MouseButton.RightButton))
            {
                Point2D mousePos = SwinGame.MousePosition();

                if (Level.Tileset.IsAt(new Position(mousePos.X, mousePos.Y)))
                {
                    if (Level.HasEntityAt(new Position(mousePos.X, mousePos.Y)))
                    {
                        Entity toRemove = Level.EntityAt(new Position(mousePos.X, mousePos.Y));
                        Level.Entities.Remove(toRemove);

                        if (toRemove.GetType() == typeof(Patroller))
                        {
                            _eMan.SelectedPatroller = null;
                        }

                        toRemove.Deregister();
                        toRemove = null;
                    }
                }
            }

            /// <summary>
            /// Move Camera
            /// </summary>
            if (SwinGame.KeyDown(KeyCode.LeftKey))
            {
                WorldAnchor.Instance.Pos.X += 1;
            }

            if (SwinGame.KeyDown(KeyCode.RightKey))
            {
                WorldAnchor.Instance.Pos.X -= 1;
            }

            if (SwinGame.KeyDown(KeyCode.UpKey))
            {
                WorldAnchor.Instance.Pos.Y += 1;
            }

            if (SwinGame.KeyDown(KeyCode.DownKey))
            {
                WorldAnchor.Instance.Pos.Y -= 1;
            }
            /// <summary>
            /// End Move Camera
            /// </summary>



            /// <summary>
            /// Modify TypeToPlace
            /// </summary>
            if (SwinGame.KeyTyped(KeyCode.TKey))
            {
                TypeToPlace = PlacingType.Tiles;
            }

            if (SwinGame.KeyTyped(KeyCode.PKey))
            {
                TypeToPlace = PlacingType.Player;
            }

            if (SwinGame.KeyTyped(KeyCode.EKey))
            {
                TypeToPlace = PlacingType.Enemies;
            }
            /// <summary>
            /// End Modify TypeToPlace
            /// </summary>


            /// <summary>
            /// Modify Level
            /// </summary>
            if (SwinGame.KeyTyped(KeyCode.NKey))
            {
                Tileset newTileset = new Tileset(WorldAnchor.Instance);
                for (int i = 0; i < 10; i++)
                {
                    newTileset.AddColumn();
                    newTileset.AddRow();
                }

                newTileset.Deregister();
                Level newLevel = new Level(newTileset);
                GameLogic.AddLevel(newLevel);
            }

            if (SwinGame.KeyTyped(KeyCode.Num1Key))
            {
                GameLogic.PreviousLevel();
            }

            if (SwinGame.KeyTyped(KeyCode.Num2Key))
            {
                GameLogic.NextLevel();
            }

            if (SwinGame.KeyTyped(KeyCode.CKey))
            {
                Instance.Level.Tileset.AddColumn();
            }

            if (SwinGame.KeyTyped(KeyCode.RKey))
            {
                Instance.Level.Tileset.AddRow();
            }
            /// <summary>
            /// End Modify Level
            /// </summary>
        }

        public override void Render()
        {
            SwinGame.DrawText("(T) Place Tiles", Color.Black, 550, 10);
            SwinGame.DrawText("(E) Place Enemies", Color.Black, 550, 25);
            SwinGame.DrawText("(P) Place Players", Color.Black, 550, 40);
            SwinGame.DrawText("(C) Add Column", Color.Black, 550, 55);
            SwinGame.DrawText("(R) Add Row", Color.Black, 550, 70);
            SwinGame.DrawText("(N) Add a new Level", Color.Black, 550, 85);
            SwinGame.DrawText("(1) and (2) Prev / Next Level", Color.Black, 550, 100);
            SwinGame.DrawText("Levels auto-save on close", Color.Black, 550, 115);
            SwinGame.DrawText("Arrow keys move camera", Color.Black, 550, 130);

            switch (TypeToPlace)
            {
                case PlacingType.Tiles: _tMan.Render();
                break;

                case PlacingType.Player: _pMan.Render();
                break;

                case PlacingType.Enemies: _eMan.Render();
                break;
            }
        }
    }
}