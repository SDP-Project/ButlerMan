using System;
using SwinGameSDK;

namespace MyGame
{
    public class WorldAnchor : Renderable
    {
        private static WorldAnchor _instance;

        public WorldAnchor()
        {
            if (_instance == null)
            {
                _instance = this;
                Anchor = this;
            }
            else
            {
                throw new InvalidOperationException("Cannot have more than one instance of World Anchor");
            }
        }

        public static WorldAnchor Instance 
        {
            get 
            {
                return _instance;
            }
        }

        public override Position AbsPos
        {
            get
            {
                return Pos;
            }
        }
    }
}