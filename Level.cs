using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace MyGame
{
    public class Level
    {
        private List<Entity> _entities;
        private Tileset _tileset;
        private List<FloodFill> _floods;

        public Level (Tileset tileset)
        {
            _tileset = tileset;
            _entities = new List<Entity>();
            _floods = new List<FloodFill>();
        }

        public List<Entity> Entities
        {
            get {return _entities;}
            set {_entities = value;}
        }

        public Tileset Tileset
        {
            get {return _tileset;}
            set {_tileset = value;}
        }

        public List<FloodFill> Floods
        {
            get {return _floods;}
            set {_floods = value;}
        }

        public void AddFloodFill(FloodFill toAdd)
        {
            _floods.Add(toAdd);
        }

        public void AddEntity(Entity toAdd)
        {
            _entities.Add(toAdd);
        }
			
        public Entity EntityAt(Position pt)
        {
            foreach (Entity e in Entities)
            {
                if (SwinGame.PointInRect(SwinGame.PointAt(pt.X, pt.Y), SwinGame.CreateRectangle(e.AbsPos.X, e.AbsPos.Y, e.Width, e.Height)))
                {
                    e.Deregister();
                    return e;
                }
            }
            return null;
        }

        public bool HasEntityAt(Position pt)
        {
            foreach (Entity e in Entities)
            {
                if (SwinGame.PointInRect(SwinGame.PointAt(pt.X, pt.Y), SwinGame.CreateRectangle(e.AbsPos.X, e.AbsPos.Y, e.Width, e.Height)))
                {
                    return true;
                }
            }
            return false;
        }

		public void Step()
        {
            foreach (FloodFill f in _floods)
            {
                f.Increment();
                f.Expand();
            }

            foreach (Entity e in _entities)
            {
			    e.Step();
            }
        }

        public void Register()
        {
            Tileset.Register();

            foreach (Entity e in Entities)
            {
                e.Register();
            }
        }

        public void Deregister()
        {
            Tileset.Deregister();

            foreach (Entity e in Entities)
            {
                e.Deregister();
            }
        }
    }
}