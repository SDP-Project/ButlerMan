﻿using System;
using SwinGameSDK;

namespace MyGame
{
	public abstract class Entity : BitmapRenderable
	{
		private float _speed;
		float _moveSpeed;
		int _boostTime;
		private int _width;
		private int _height;

		public Entity() : this (WorldAnchor.Instance)
		{}

		public Entity (Renderable anchor) : base(anchor)
		{
			Img = null;
			_speed = 1;
			MoveSpeed = _speed;
			_width = 32;
			_height = 32;
			
			//Img = SwinGame.CreateBitmap(32, 32);
			//SwinGame.ClearSurface(Img, Color.Green);
		}

		public float Speed
		{
			get {return _speed;}
			set {_speed = value;}
		}

		public int Width
		{
			get { return _width; }
			set { _width = value; }
		}

		public int Height
		{
			get { return _height; }
			set { _height = value; }
		}

		public float MoveSpeed
		{
			get { return _moveSpeed; }
			set { _moveSpeed = value; }
		}

		public int BoostTime
		{
			get { return _boostTime; }
			set { _boostTime = value; }
		}

		public override void Render ()
		{
		if (Img != null)
			{
			SwinGame.DrawBitmap(Img, AbsPos.X, AbsPos.Y);
			}
		}

		public abstract void Step();

		public void HandleBoost ()
		{
			if (BoostTime == 1)
			{
				BoostTime = 0;
				MoveSpeed = Speed;
			}
			else if (BoostTime > 1)
			{
				BoostTime -= 1;
				//SwinGame.DrawText ((BoostTime/60 + 1).ToString (), Color.Black, 400, 400);
			}
		}

		//Pass the amount you want to move in each axis.
		public void Move(float x, float y)
		{
			Position tryMove = new Position(x, 0);

			//Horizontal
			if (!TileInteractor.CollisionAt(AbsPos.CheckAdd(tryMove), Width, Height))
			{
				Pos.X += tryMove.X;
			}
				
			tryMove.X = 0;
			tryMove.Y = y;

			//Vertical
			if (!TileInteractor.CollisionAt (AbsPos.CheckAdd(tryMove), Width, Height))
			{
                Pos.Y += tryMove.Y;
			}
		}

		//Overload which only moves if both directions are free and returns false if they arent.
		public bool MoveOrCollide(float x, float y)
		{
			Position tryMove = new Position(x, y);

			if (!TileInteractor.CollisionAt(AbsPos.CheckAdd(tryMove), Width, Height) && !TileInteractor.CollisionAt (AbsPos.CheckAdd(tryMove), Width, Height))
			{
                Pos.X += tryMove.X;
                Pos.Y += tryMove.Y;

				return false;
			}

			return true;
		}
	}
}

