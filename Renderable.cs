using System;
using SwinGameSDK;

namespace MyGame
{
    public class Renderable
    {
        private Renderable _anchor; //Renderables are drawn relative to the position of their anchor
        private Position _pos;

        public Renderable() : this (WorldAnchor.Instance, new Position(0, 0))
        {
        }

        public Renderable (Renderable anchor) : this (anchor, new Position(0, 0))
        {
        }

        public Renderable(Position pos) : this (WorldAnchor.Instance, pos)
        {
        }

        public Renderable(Renderable anchor, Position pos)
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

        public Position Pos
        {
            get {return _pos;}
            set {_pos = value;}
        }

        public virtual Position AbsPos
        {
            get
            {
                //HACK: Added a check to remove infinite looping/stack overflow
                if (Anchor.Anchor == this)
                    return Pos;

                return Pos.CheckAdd(Anchor.AbsPos);
            }
        }

        public virtual void Render()
        {}
    }
}