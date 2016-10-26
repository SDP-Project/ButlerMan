using System;
using System.Collections.Generic;
using System.IO;
using SwinGameSDK;

namespace MyGame
{
    public static class LevelIO
    {
        public static void LoadAllLevels()
        {
            GameLogic.Levels.Clear();
            StreamReader readLevelCount = new StreamReader("Resources/level.txt");
            int numLevels = Utils.ReadInteger(readLevelCount);
            readLevelCount.Close();

            for (int i = 0; i < numLevels; i++)
            {
                GameLogic.AddLevel(LoadLevel(i));
            }
        }

        public static void SaveAllLevels()
        {
            StreamWriter saveLevelCount = new StreamWriter("Resources/level.txt");
            saveLevelCount.WriteLine(GameLogic.Levels.Count);
            saveLevelCount.Close();

            for (int i = 0; i < GameLogic.Levels.Count; i++)
            {
                SaveLevel(GameLogic.Levels[i], i);
            }
        }

        public static Level LoadLevel(int index)
        {
            StreamReader levelPath = new StreamReader("Resources/Level" + index + ".txt");
            Tileset newTileset = LoadTileset(levelPath);
            TileInteractor.Tileset = newTileset;
            Level newLevel = new Level(newTileset);
            newLevel.Floods = LoadFloods(newLevel, levelPath);
            newLevel.Entities = LoadEntities(levelPath, newTileset);
            newLevel.Deregister();
            levelPath.Close();
            return newLevel;
        }

        public static Tileset LoadTileset(StreamReader reader)
        {
            int x = Utils.ReadInteger(reader);
            int y = Utils.ReadInteger(reader);

            Tileset result = new Tileset(WorldAnchor.Instance);
            result.Pos = new Position(x, y);

            int cols = Utils.ReadInteger(reader);;
            int rows = Utils.ReadInteger(reader);

            for (int i = 0; i < cols; i++)
            {
                result.AddColumn();
            }

            for (int i = 0; i < rows; i++)
            {
                result.AddRow();
            }

            int col, row;

            for (col = 0; col < cols; col++)
            {
                for (row = 0; row < rows; row++)
                {
                    result.Tiles[col][row] = LoadTile(result, reader); 
                }
            }

            return result;
        }

        public static Tile LoadTile(Renderable anchor, StreamReader reader)
        {
            Tile result;
            string type = reader.ReadLine();
            string wall = reader.ReadLine();

            if (type == "MyGame.SpeedTile")
            {
                int power = Utils.ReadInteger(reader);
                result = new SpeedTile(power, anchor);
                if (power > 0)
                    result.Img = SwinGame.BitmapNamed("SpeedUp");
                else
                    result.Img = SwinGame.BitmapNamed("SpeedDown");
            }
            else if (type == "MyGame.WaterTile")
            {
                result = new WaterTile(anchor);
                result.Img = SwinGame.BitmapNamed("Water");
            }
            else if (type == "MyGame.EndOfLevelTile")
            {
                result = new EndOfLevelTile(anchor);
                Bitmap newBmp = SwinGame.CreateBitmap(32, 32);
                SwinGame.ClearSurface(newBmp, Color.Orange);
                result.Img = newBmp;
            }
            else //Is normal Tile
            {
                result = new Tile(anchor);
            }
            
            int x = Utils.ReadInteger(reader);
            int y = Utils.ReadInteger(reader);

            result.Pos = new Position(x, y);

            string rootBitmap = reader.ReadLine();
            int rootIndex = Utils.ReadInteger(reader);

            if (type == "MyGame.Tile")
            {
                result.Img = GameResources.GetBitmap(rootBitmap, rootIndex);
            }

            if (wall == "True")
            {
                result.IsWall = true;
            }
            else
            {
                result.IsWall = false;
            }


            result.RootBitmap = rootBitmap;
            result.RootIndex = rootIndex;

            return result;
        }

        public static List<Entity> LoadEntities(StreamReader reader, Tileset tileset)
        {
            int numEntities = Utils.ReadInteger(reader);
            List<Entity> result = new List<Entity>();

            for (int i = 0; i < numEntities; i++)
            {
                result.Add(LoadEntity(reader, tileset));
            }
           
            return result;
        }

        public static Entity LoadEntity(StreamReader reader, Tileset tileset)
        {
            string entityType = reader.ReadLine();
            Entity newEntity;

            switch (entityType)
            {
                case "MyGame.Player": newEntity = new Player();
                                      newEntity.Img = SwinGame.BitmapNamed("Player");
                break;

                case "MyGame.Wanderer": newEntity = new Wanderer();
                                        newEntity.Img = SwinGame.BitmapNamed("Skull");
                break;

                case "MyGame.WallFollower": newEntity = new WallFollower();
                                            newEntity.Img = SwinGame.BitmapNamed("DirtyPlate");
                break;

                case "MyGame.Patroller": 
                {
                    newEntity = new Patroller();
                    newEntity.Img = SwinGame.BitmapNamed("RedKnight");
                    break;
                }
                default: throw new InvalidOperationException("Unknown Entity type");
            }

            int atX = Utils.ReadInteger(reader);
            int atY = Utils.ReadInteger(reader);

            newEntity.Pos = new Position(atX, atY);

            if (entityType == "MyGame.Patroller")
            {
                LoadPatroller(reader, (Patroller)newEntity, tileset);
            }

            return newEntity;
        }

        public static void LoadPatroller(StreamReader reader, Patroller pat, Tileset tileset)
        {
            int pathCount = Utils.ReadInteger(reader);

            for (int i = 0; i < pathCount - 1; i++)
            {
                int col = Utils.ReadInteger(reader);
                int row = Utils.ReadInteger(reader);

                pat.AddTileToPath(tileset.Tiles[col][row], tileset);
            }
        }

        public static void SaveLevel(Level toSave, int index)
        {
            StreamWriter levelPath = new StreamWriter("Resources/Level" + index + ".txt");
            SaveTileset(toSave.Tileset, levelPath);
            SaveFloods(toSave, levelPath);
            SaveEntities(levelPath, toSave.Entities, toSave.Tileset);

            levelPath.Close();
        }

        private static void SaveFloods(Level level, StreamWriter writer)
        {
            writer.WriteLine(level.Floods.Count);

            foreach (FloodFill f in level.Floods)
            {
                writer.WriteLine(f.Source.Pos.X);
                writer.WriteLine(f.Source.Pos.Y);
            }
        }

        private static List<FloodFill> LoadFloods(Level level, StreamReader reader)
        {
            int numFloods = Utils.ReadInteger(reader);
            float x, y;

            List<FloodFill> result = new List<FloodFill>();

            for (int i = 0; i < numFloods; i++)
            {
                x = Utils.ReadInteger(reader);
                y = Utils.ReadInteger(reader);

                result.Add(new FloodFill(level.Tileset.TileAt(x + 1, y + 1)));
            }
            return result;
        }

        private static void SaveTileset(Tileset toSave, StreamWriter writer)
        {
            writer.WriteLine(toSave.Pos.X);
            writer.WriteLine(toSave.Pos.Y);
            writer.WriteLine(toSave.Cols);
            writer.WriteLine(toSave.Rows);

            foreach (List<Tile> list in toSave.Tiles)
            {
                foreach (Tile t in list)
                {
                    SaveTile(t, writer);
                }
            }
        }

        private static void SaveTile(Tile toSave, StreamWriter writer)
        {
            writer.WriteLine(toSave.GetType());

            writer.WriteLine(toSave.IsWall);

            if (toSave.GetType() == typeof(SpeedTile))
            {
                SaveSpeedTile((SpeedTile)toSave, writer);
            }

            writer.WriteLine(toSave.Pos.X);
            writer.WriteLine(toSave.Pos.Y);

            if (toSave.RootBitmap == null)
            {
                writer.WriteLine("nullBmp");
            }
            else 
            {
                writer.WriteLine(toSave.RootBitmap);
            }
            writer.WriteLine(toSave.RootIndex);
        }

        private static void SaveSpeedTile(SpeedTile toSave, StreamWriter writer)
        {
            writer.WriteLine(toSave.Power);
        }

        private static void SaveEntities(StreamWriter writer, List<Entity> entities, Tileset tileset)
        {
            writer.WriteLine(entities.Count);

            foreach (Entity e in entities)
            {
                writer.WriteLine(e.GetType());

                if (e.GetType() == typeof(Patroller))
                {
                    SavePatroller(writer, (Patroller)e, tileset);
                }
                else
                {
                    writer.WriteLine(e.Pos.X);
                    writer.WriteLine(e.Pos.Y);
                }
            }
        }

        private static void SavePatroller(StreamWriter writer, Patroller pat, Tileset tileset)
        {
            writer.WriteLine(pat.Path[0].Pos.X);
            writer.WriteLine(pat.Path[0].Pos.Y);
            writer.WriteLine(pat.Path.Count);

            //Don't save first one in list because it's the tile the AI is on and will be loaded automatically
            for (int i = 1; i < pat.Path.Count; i++)
            {
                Position tileIndex = tileset.GetTileIndexFromPt(new Position(pat.Path[i].Pos.X + 1, pat.Path[i].Pos.Y + 1));
                writer.WriteLine(tileIndex.X); //Column
                writer.WriteLine(tileIndex.Y); //Row
            }
        }
    }
}