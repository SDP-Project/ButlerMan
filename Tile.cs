using System;
using SwinGameSDK; 
using System.IO;

namespace MyGame
{
	public class Tile : BitmapRenderable
    {
        private bool _isWall;
        private string _rootBitmap;
        private int _rootIndex;

        public Tile() : this (WorldAnchor.Instance)
        {}

        public Tile (Renderable anchor) : base(anchor)
        {
            _isWall = false;
        }

        public bool IsWall
        {
            get {return _isWall;}
            set {_isWall = value;}
        }

        public string RootBitmap
        {
            get {return _rootBitmap;}
            set {_rootBitmap = value;}
        }

        public int RootIndex
        {
            get {return _rootIndex;}
            set {_rootIndex = value;}
        }

        public virtual void ApplyTileEffect(Player p)
        {}
    }
}