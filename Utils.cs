using System;
using System.Collections.Generic;
using System.IO;
using SwinGameSDK;

namespace MyGame
{
    public static class Utils
    {
        //Splits the cells in the passed in bitmap into a bitmap array.
        //This allows sprite sheets to be used for grouped tiles, thus reducing the number of files required.
        public static Bitmap[] SplitBitmapCells(Bitmap toSplit)
        {
            Bitmap[] result = new Bitmap[SwinGame.BitmapCellCount(toSplit)];
            int rows = SwinGame.BitmapCellRows(toSplit);
            int cols = SwinGame.BitmapCellRows(toSplit);
            int w = SwinGame.BitmapCellWidth(toSplit);
            int h = SwinGame.BitmapCellHeight(toSplit);
            Rectangle copyAt;

            int atCol, atRow;

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = SwinGame.CreateBitmap(w, h);
                SwinGame.ClearSurface(result[i], Color.Transparent);

                atCol = i % cols;
                atRow = (int)Math.Truncate((double)(i / cols));

                copyAt = SwinGame.CreateRectangle(atCol * w, atRow * h, w, h);

                SwinGame.DrawBitmap(toSplit, 0, 0, SwinGame.OptionPartBmp(copyAt, SwinGame.OptionDrawTo(result[i])));
            }

            return result;
        }

        public static int ReadInteger(StreamReader reader)
        {
            return Convert.ToInt32(reader.ReadLine());
        }
    }
}