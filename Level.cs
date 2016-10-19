using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace MyGame
{
    public class Level
    {
        private List<Entity> _entities;
        private Tileset _tileset;

        public Level (Tileset tileset)
        {
            _tileset = tileset;
            _entities = new List<Entity>();
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

        public void AddEntity(Entity toAdd)
        {
            _entities.Add(toAdd);
        }
			
        public Entity EntityAt(Point2D pt)
        {
            foreach (Entity e in Entities)
            {
                if (SwinGame.PointInRect(pt, SwinGame.CreateRectangle(e.AbsPos.X, e.AbsPos.Y, e.Width, e.Height)))
                {
                    e.Deregister();
                    return e;
                }
            }
            return null;
        }

        public bool HasEntityAt(Point2D pt)
        {
            foreach (Entity e in Entities)
            {
                if (SwinGame.PointInRect(pt, SwinGame.CreateRectangle(e.AbsPos.X, e.AbsPos.Y, e.Width, e.Height)))
                {
                    return true;
                }
            }
            return false;
        }

		public void Step()
        {
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