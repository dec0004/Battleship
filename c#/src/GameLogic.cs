using System;
using SwinGameSDK;
internal static class GameLogic
{
	private static int volume;
	public static void Main()
	{
		SwinGame.OpenAudio();
		//Opens a new Graphics Window
		SwinGame.OpenGraphicsWindow("Battle Ships", 800, 600);

		//Load Resources
		GameResources.LoadResources();

		SwinGame.PlayMusic(GameResources.GameMusic("Background"));
		Audio.PlayMusic(GameResources.GameMusic("Rage"));
		//Game Loop
		do
		{

			GameController.HandleUserInput();
			GameController.DrawScreen();
		} while (!(SwinGame.WindowCloseRequested() == true || GameController.CurrentState == GameState.Quitting));

		SwinGame.StopMusic();

		//Free Resources and Close Audio, to end the program.
		GameResources.FreeResources();
	}
}
