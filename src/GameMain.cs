using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace MyGame
{
    public class GameMain
    {
        public static GameState gameState;

        public static void Main()
        {
                SwinGame.LoadResourceBundleNamed("GameResources", "GameResources.txt", false);
            WorldAnchor world = new WorldAnchor();
            ScreenAnchor screen = new ScreenAnchor();

            //Open the game window
            SwinGame.OpenGraphicsWindow("GameMain", 800, 600);

            GameResources.LoadResources();

            gameState = GameState.InGame;
            GameLogic gameLogic = new GameLogic();

            LevelIO.LoadAllLevels();

            
            //Run the game loop
            while(false == SwinGame.WindowCloseRequested())
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();
                
                //Clear the screen and draw the framerate
                SwinGame.ClearScreen(Color.White);

                if (SwinGame.KeyTyped(KeyCode.MKey))
                {
                    LevelIO.SaveAllLevels();
                }

                if (SwinGame.KeyTyped(KeyCode.SpaceKey))
                {
                    if (gameState == GameState.InGame)
                    {
                        gameState = GameState.EditLevel;
                        LevelEditor.Instance.Register();

                    }
                    else
                    {
                        gameState = GameState.InGame;
                        GameLogic.ActiveLevel.Deregister();
                        GameLogic.ActiveLevel.Register();
                        LevelEditor.Instance.Deregister();
                    }
                }

                if (gameState == GameState.InGame)
                {
					GameLogic.ActiveLevel.Step();
                }

                if (gameState == GameState.EditLevel)
                {
                    LevelEditor.Instance.HandleInput();
                }

                Renderer.Render();

                SwinGame.RefreshScreen(60);
            }
            LevelIO.SaveAllLevels();
        }
    }
}