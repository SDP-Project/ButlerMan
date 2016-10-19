using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace MyGame
{
    public static class GameResources
    {
        private static Dictionary<string, Bitmap[]> _bitmaps;

        public static void LoadResources()
        {
            _bitmaps = new Dictionary<string, Bitmap[]>();
            AddBitmapList(SwinGame.BitmapNamed("DungeonTileset"));
        }

        public static void AddBitmapList(Bitmap toAdd)
        {
            string name = SwinGame.BitmapName(toAdd).ToLower();
            Bitmap[] bitmaps = Utils.SplitBitmapCells(toAdd);

            _bitmaps.Add(name, bitmaps);
        }

        public static Bitmap GetBitmap(string rootBitmap, int index)
        {
            if (_bitmaps.ContainsKey(rootBitmap.ToLower()))
            {
                Bitmap[] list = _bitmaps[rootBitmap.ToLower()];
                Bitmap bmp = list[index];
                return bmp;
            }
            return null;
        }

        public static Bitmap[] GetBitmapListNamed(string rootBitmap)
        {
            return _bitmaps[rootBitmap.ToLower()];
        }
    }
}