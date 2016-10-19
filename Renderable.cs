using System;
using SwinGameSDK;

namespace MyGame
{
    public class Renderable
    {
        private Renderable _anchor; //Renderables are drawn relative to the position of their anchor
        private Point2D _pos;

        public Renderable() : this (WorldAnchor.Instance, new Point2D())
        {
        }

        public Renderable (Renderable anchor) : this (anchor, new Point2D())
        {
        }

        public Renderable(Point2D pos) : this (WorldAnchor.Instance, pos)
        {
        }

        public Renderable(Renderable anchor, Point2D pos)
        {
            Anchor = anchor;
            Pos = pos;
            Renderer.Register(this); //Registers with the renderer so it can be drawn
        }

        public virtual void Register()
        {
            Renderer.Register(this);
        }

        public virtual void Deregister()
        {
            Renderer.Deregister(this);
        }

        public Renderable Anchor
        {
            get {return _anchor;}
            set {_anchor = value;}
        }

        public Point2D Pos
        {
            get {return _pos;}
            set {_pos = value;}
        }

        public virtual Point2D AbsPos
        {
            get
            {
                //HACK: Added a check to remove infinite looping/stack overflow
                if (Anchor.Anchor == this)
                    return Pos;

                return SwinGame.AddVectors(Pos, Anchor.AbsPos);
            }
        }

        public virtual void Render()
        {}
    }
}