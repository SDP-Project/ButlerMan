using System;
using System.Collections.Generic;
using System.Linq;
using SwinGameSDK;

namespace MyGame
{
    public class Tileset : Renderable
    {
        private List<List<Tile>> _tiles;
        private int _cols, _rows;

        public Tileset () : this(WorldAnchor.Instance)
        {
        }

        public Tileset(Renderable anchor) : base(anchor)
        {
            _tiles = new List<List<Tile>>();
        }

        public List<List<Tile>> Tiles
        {
            get {return _tiles;}
            set {_tiles = value;}
        }

        public int Cols
        {
            get {return _cols;}
            set {_cols = value;}
        }

        public int Rows
        {
            get {return _rows;}
            set {_rows = value;}
        }

        public bool IsAt(Point2D pt)
        {
            return SwinGame.PointInRect(pt, AbsPos.X, AbsPos.Y, 
                                        Renderer.TILE_SIZE * _cols, 
                                        Renderer.TILE_SIZE * _rows);
        }

        public Tile TileAt(float x, float y)
        {
            return TileAt(SwinGame.PointAt(x, y));
        }

        //Tile locations are fixed so they can be locate directly and without iteration
        public Tile TileAt(Point2D pt)
        {
            int col = (int)Math.Truncate((pt.X - AbsPos.X) / Renderer.TILE_SIZE);
            int row = (int)Math.Truncate((pt.Y - AbsPos.Y) / Renderer.TILE_SIZE);

            return Tiles[col][row];
        }		

        public Point2D GetTileIndexAt(int col, int row)
        {
            return SwinGame.PointAt(_tiles[col][row].Pos.X, _tiles[col][row].Pos.Y);
        }

        public Point2D GetTileIndexFromPt(Point2D pt)
        {
            int col = (int)Math.Truncate((pt.X - AbsPos.X) / Renderer.TILE_SIZE);
            int row = (int)Math.Truncate((pt.Y - AbsPos.Y) / Renderer.TILE_SIZE);

            return SwinGame.PointAt(col, row);
        }

        public void ReplaceTileAt(Point2D pt, Tile t)
        {
            Point2D index = GetTileIndexFromPt(pt);
            Tiles[(int)index.X][(int)index.Y] = null; 
            Tiles[(int)index.X][(int)index.Y] = t; 
        }

        //Draws grid showing tile locations - mainly for debugging purposes
        public override void Render()
        {
            foreach (List<Tile> list in _tiles)
            {
                foreach (Tile t in list)
                {
                    SwinGame.DrawRectangle(Color.Black, 
                                           t.AbsPos.X, 
                                           t.AbsPos.Y, 
                                           Renderer.TILE_SIZE, 
                                           Renderer.TILE_SIZE);
                }
            }
        }

        public override void Register()
        {
            foreach (List<Tile> list in _tiles)
            {
                foreach (Tile t in list)
                {
                    Renderer.Register(t);
                }
            }

            base.Register();
        }

        public override void Deregister()
        {
            foreach (List<Tile> list in _tiles)
            {
                foreach (Tile t in list)
                {
                    Renderer.Deregister(t);
                }
            }

            base.Deregister();
        }

        //Creates a new list with length equal to the number of rows
        public void AddColumn()
        {
            Cols += 1;

            List<Tile> newCol = new List<Tile>();
            int x, y;
            Tile newTile;

            for (int i = 0; i < Rows; i++)
            {
                x = (Cols - 1) * Renderer.TILE_SIZE;
                y = i * Renderer.TILE_SIZE;

                newTile = new Tile(this);
                newTile.Pos = SwinGame.PointAt(x, y);

                newCol.Add(newTile);
            }

            Tiles.Add(newCol);
        }

        //Adds an extra element to the end of each list
        public void AddRow()
        {
            Rows += 1;

            int x, y;
            Tile newTile;

            for (int i = 0; i < Cols; i++)
            {
                x = i * Renderer.TILE_SIZE;
                y = (Rows - 1) * Renderer.TILE_SIZE;

                newTile = new Tile(this);
                newTile.Pos = SwinGame.PointAt(x, y);

                Tiles[i].Add(newTile);
            }
        }
    }
}