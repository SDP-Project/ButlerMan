﻿using System;
using SwinGameSDK;

namespace MyGame
{
    public class ScreenAnchor : Renderable
    {
        private static ScreenAnchor _instance;

        public ScreenAnchor()
        {
            if (_instance == null)
            {
                Pos = new Position(0, 0);
                _instance = this;
                Anchor = this;
            }
            else
            {
                throw new InvalidOperationException("Cannot have more than one instance of Screen Anchor");
            }
        }

        public static ScreenAnchor Instance
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