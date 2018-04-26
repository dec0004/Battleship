using System;
using SwinGameSDK;

namespace MyGame
{
    public class GameMain
    {
        public void Main()
        {
            // Opens a new Graphics Window
            // Load Resources
            LoadResources();
            // Game Loop
            for (
            ; (((SwinGame.WindowCloseRequested() == true)
                        || (CurrentState == GameState.Quitting))
                        == false);
            )
            {
                HandleUserInput();
                DrawScreen();
            }

            // Free Resources and Close Audio, to end the program.
            FreeResources();
        }
    }
}
