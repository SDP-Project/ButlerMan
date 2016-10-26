using System;
using SwinGameSDK;

namespace MyGame
{
	public class Player : Entity
	{
        private static int MAX_BREATH = 180;
        private int _currentBreath;

		public Player() : this (WorldAnchor.Instance)
		{}

		public Player (Renderable anchor) : base(anchor)
		{
			MoveSpeed = Speed;
			Width = 32;
			Height = 32;
            Speed = 3;
            MoveSpeed = Speed;
            _currentBreath = MAX_BREATH;

			//Img = SwinGame.CreateBitmap(32, 32);
			//SwinGame.ClearSurface(Img, Color.Green);
		}

        public int CurrentBreath
        {
            get {return _currentBreath;}
            set {_currentBreath = value;}
        }

		public override void Step()
		{
			HandleInput ();
			HandleBoost ();
		}

		private void HandleInput()
		{
			//Changing to else if may fix future problems.
			if (SwinGame.KeyDown (KeyCode.UpKey))
			{
				Move (0, -MoveSpeed);
			}
			if (SwinGame.KeyDown (KeyCode.DownKey))
			{
				Move (0, MoveSpeed);
			}

			if (SwinGame.KeyDown (KeyCode.LeftKey))
			{
				Move (-MoveSpeed, 0);
			}
			if (SwinGame.KeyDown (KeyCode.RightKey))
			{
				Move (MoveSpeed, 0);
			}

			if (SwinGame.KeyTyped (KeyCode.ShiftKey))
			{
				StartBoost (5, 1);
			}

			if (SwinGame.KeyTyped (KeyCode.CtrlKey))
			{
				SpeedBoost (0);
			}

            /// <summary>
            /// Currently applies Tile effect every frame. Consider optimising to have Player existing on a Tile rather
            /// than having to fetch a new tile (sometimes the same one) every frame?
            /// </summary>
            TileInteractor.TileAt(AbsPos.X + (Height / 2), AbsPos.Y + (Width / 2)).ApplyTileEffect(this);
            Tile t = TileInteractor.TileAt(AbsPos.X + (Height / 2), AbsPos.Y + (Width / 2));

            if (!(t.GetType() == typeof(WaterTile)))
            {
                t.ApplyTileEffect(this);

                if (CurrentBreath < MAX_BREATH)
                    CurrentBreath++;
            }
            else
            {
                CurrentBreath--;
                if (CurrentBreath <= 0)
                {
                    GameLogic.deaths++;
                    LevelIO.LoadAllLevels();
                }
            }
		}

		public void SpeedBoost(int Boost)
		{
			MoveSpeed = Speed + Boost;
		}

		public void StartBoost(int Duration, int Boost)
		{
			BoostTime = 60 * Duration;
			SpeedBoost (Boost);
		}

        public override void Render()
        {
            base.Render();

            if (GameMain.gameState == GameState.InGame)
            {
                float pixelsPerBreath = (float)114 / (float)MAX_BREATH;
                SwinGame.DrawBitmap(SwinGame.BitmapNamed("StatusBar"), 50, 5);
                SwinGame.FillRectangle(Color.Turquoise, 57, 7, pixelsPerBreath * CurrentBreath, 12);
            }
           
            SwinGame.DrawText("Failed Errands: " + GameLogic.deaths, Color.White, 500, 5);
        }
	}
}