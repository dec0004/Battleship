using System;
using SwinGameSDK;
using System.Diagnostics;
using System.IO;
using System.Linq;
// <summary>
// The battle phase is handled by the DiscoveryController.
// </summary>
internal static class DiscoveryController
{
	// <summary>
	// Handles input during the discovery phase of the game.
	// </summary>
	// <remarks>
	// Escape opens the game menu. Clicking the mouse will
	// attack a location.
	// </remarks>
	public static void HandleDiscoveryInput()
	{

		//SwinGame.SetMusicVolume();
		

		if (SwinGame.KeyTyped(KeyCode.EscapeKey))
		{
			GameController.AddNewState(GameState.ViewingGameMenu);
		}

		if (SwinGame.MouseClicked(MouseButton.LeftButton))
		{
			DoAttack();
		}
	}

	// <summary>
	// Attack the location that the mouse is over.
	// </summary>
	private static void DoAttack()
	{
		Point2D mouse = SwinGame.MousePosition();


		// Calculate the row/col clicked
		int row = 0;
		int col = 0;
		row = Convert.ToInt32(Math.Floor((mouse.Y - UtilityFunctions.FIELD_TOP) / (UtilityFunctions.CELL_HEIGHT + UtilityFunctions.CELL_GAP)));
		col = Convert.ToInt32(Math.Floor((mouse.X - UtilityFunctions.FIELD_LEFT) / (UtilityFunctions.CELL_WIDTH + UtilityFunctions.CELL_GAP)));

		if (row >= 0 && row < GameController.HumanPlayer.EnemyGrid.Height)
		{
			if (col >= 0 && col < GameController.HumanPlayer.EnemyGrid.Width)
			{
				GameController.Attack(row, col);
			}
		}
	}

	// <summary>
	// Draws the game during the attack phase.
	// </summary>s
	public static void DrawDiscovery()
	{
		const int SCORES_LEFT = 172;
		const int SHOTS_TOP = 157;
		const int HITS_TOP = 206;
		const int SPLASH_TOP = 256;

		if (((SwinGame.KeyDown(KeyCode.LeftShiftKey) | SwinGame.KeyDown(KeyCode.RightShiftKey)) & SwinGame.KeyDown(KeyCode.CKey)) != true)
		{
			UtilityFunctions.DrawField(GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, false);
		}
		else
		{
			UtilityFunctions.DrawField(GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, false);
		}

		UtilityFunctions.DrawSmallField(GameController.HumanPlayer.PlayerGrid, GameController.HumanPlayer);
		UtilityFunctions.DrawMessage();


		System.IO.TextReader input = new StreamReader("Volume.txt");

		double _vol = double.Parse(input.ReadLine());
		float newVol = (float)_vol;
		input.Close();
		SwinGame.DrawText(GameController.HumanPlayer.Shots.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, SHOTS_TOP);
		SwinGame.DrawText(GameController.HumanPlayer.Hits.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, HITS_TOP);
		SwinGame.DrawText(GameController.HumanPlayer.Missed.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, SPLASH_TOP);
		SwinGame.DrawText("-   Volume   +", Color.White, GameResources.GameFont("Menu"), 440f, 70f);
		SwinGame.DrawText("Current Volume:" + _vol, Color.White, GameResources.GameFont("Menu"), 580f, 70f);
		

		if ((SwinGame.MouseClicked(MouseButton.LeftButton) && (UtilityFunctions.IsMouseInRectangle(440, 73, 10, 10)))&& _vol > 0)
		{
				_vol = _vol - 0.1;
				File.WriteAllText("Volume.txt", String.Empty);
				TextWriter tw = new StreamWriter("Volume.txt", true);

				tw.WriteLine(_vol);
				tw.Close();
				SwinGame.SetMusicVolume(newVol);
		} else if ((SwinGame.MouseClicked(MouseButton.LeftButton) && ((UtilityFunctions.IsMouseInRectangle(515, 73, 10, 10)))) && _vol <1)
		{
				_vol = _vol + 0.1;
				File.WriteAllText("Volume.txt", String.Empty);
				TextWriter tw = new StreamWriter("Volume.txt", true);

				tw.WriteLine(_vol);
				tw.Close();
				SwinGame.SetMusicVolume(newVol);
		}



	}
}
