using System;
using System.Collections.Generic;
using System.Linq;
using SwinGameSDK;

namespace MyGame
{
    public class GameLogic
    {
        public static List<Level> Levels = new List<Level>();
        public static Level ActiveLevel;
        public static int deaths = 0;

        public GameLogic ()
        {
            LevelEditor levelEditor = new LevelEditor();
            LevelEditor.Instance.Deregister();
        }

        public static Player Player
        {
            get {return ActiveLevel.Player;}
        }

        public static void NextLevel()
        {
            int currentLevel = Levels.IndexOf(ActiveLevel);

            foreach (Level l in Levels)
            {
                l.Deregister();
            }

            LevelIO.LoadAllLevels();

            if (currentLevel == Levels.Count - 1) // Wrap back to first level
            {
                ActiveLevel = Levels[0];
            }
            else
            {
                ActiveLevel = Levels[currentLevel + 1];
            }
            ActiveLevel.Register();
            LevelEditor.Instance.Level = ActiveLevel;
            TileInteractor.Tileset = ActiveLevel.Tileset;
        }

        public static void PreviousLevel()
        {
            int currentLevel = Levels.IndexOf(ActiveLevel);
            ActiveLevel.Deregister();

            if (currentLevel == 0)
            {
                ActiveLevel = Levels.Last();
            }
            else
            {
                ActiveLevel = Levels[currentLevel - 1];
            }
            ActiveLevel.Register();
            LevelEditor.Instance.Level = ActiveLevel;
            TileInteractor.Tileset = ActiveLevel.Tileset;
        }

        public static void AddLevel(Level toAdd)
        {
            Levels.Add(toAdd);

            if (ActiveLevel != null)
            {
                ActiveLevel.Deregister();
            }

            ActiveLevel = toAdd;
            //ActiveLevel.Register();
            LevelEditor.Instance.Level = ActiveLevel;
            TileInteractor.Tileset = ActiveLevel.Tileset;
        }
    }
}