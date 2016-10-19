using System;
using System.Collections.Generic;

namespace MyGame
{
    public static class Renderer
    {
        private static int tile_size = 32;
        private static List<Renderable> _renderables = new List<Renderable>();

        public static int TILE_SIZE
        {
            get {return tile_size;}
        }

        public static List<Renderable> Renderables
        {
            get {return _renderables;}
        }

        public static void Register(Renderable r)
        {
            _renderables.Add(r);
        }

        public static void Deregister(Renderable r)
        {
            _renderables.Remove(r);;
        }

        public static void Render()
        {
            foreach (Renderable r in _renderables)
            {
                r.Render();
            }
        }
    }
}