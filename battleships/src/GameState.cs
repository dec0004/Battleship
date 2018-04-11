// '' <summary>
// '' The GameStates represent the state of the Battleships game play.
// '' This is used to control the actions and view displayed to
// '' the player.
// '' </summary>
public enum GameState
{
	ViewingMainMenu, // '' The player is viewing the main menu.

    ViewingGameMenu, // '' The player is viewing the game menu

    ViewingHighScores, // '' The player is looking at the high scores

    AlteringSettings, // '' The player is altering the game settings

    Deploying, // '' Players are deploying their ships

    Discovering, // '' Players are attempting to locate each others ships

    EndingGame, // '' One player has won, showing the victory screen

    Quitting, // '' The player has quit. Show ending credits and terminate the game
}