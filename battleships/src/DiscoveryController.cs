using SwinGameSDK;
//The battle phase is handled by the DiscoveryController.
class DiscoveryController
{


	//Handles input during the discovery phase of the game.
	//Escape opens the game menu. Clicking the mouse will attack a location.
	public static void HandleDiscoveryInput()
	{
		if (SwinGame.KeyTyped(KeyCode.VK_ESCAPE))
		{
			AddNewState(GameState.ViewingGameMenu);
		}

		if (SwinGame.MouseClicked(MouseButton.LeftButton))
		{
			DiscoveryController.DoAttack();
		}

	}


	//Attack the location that the mouse if over.
	private static void DoAttack()
	{
		Point2D mouse;
		mouse = SwinGame.MousePosition();
		// Calculate the row/col clicked
		int row;
		int col;
		row = Convert.ToInt32(Math.Floor(((mouse.Y - FIELD_TOP)
							/ (CELL_HEIGHT + CELL_GAP))));
		col = Convert.ToInt32(Math.Floor(((mouse.X - FIELD_LEFT)
							/ (CELL_WIDTH + CELL_GAP))));
		if (((row >= 0)
					&& (row < HumanPlayer.EnemyGrid.Height)))
		{
			if (((col >= 0)
						&& (col < HumanPlayer.EnemyGrid.Width)))
			{
				Attack(row, col);
			}

		}

	}


	//Draws the game during the attack phase.
	public static void DrawDiscovery()
	{
		const int SCORES_LEFT = 172;
		const int SHOTS_TOP = 157;
		const int HITS_TOP = 206;
		const int SPLASH_TOP = 256;
		if (((SwinGame.KeyDown(KeyCode.VK_LSHIFT) || SwinGame.KeyDown(KeyCode.VK_RSHIFT))
					&& SwinGame.KeyDown(KeyCode.VK_C)))
		{
			DrawField(HumanPlayer.EnemyGrid, ComputerPlayer, true);
		}
		else
		{
			DrawField(HumanPlayer.EnemyGrid, ComputerPlayer, false);
		}

		DrawSmallField(HumanPlayer.PlayerGrid, HumanPlayer);
		DrawMessage();
		SwinGame.DrawText(HumanPlayer.Shots.ToString(), Color.White, GameFont("Menu"), SCORES_LEFT, SHOTS_TOP);
		SwinGame.DrawText(HumanPlayer.Hits.ToString(), Color.White, GameFont("Menu"), SCORES_LEFT, HITS_TOP);
		SwinGame.DrawText(HumanPlayer.Missed.ToString(), Color.White, GameFont("Menu"), SCORES_LEFT, SPLASH_TOP);
	}
}