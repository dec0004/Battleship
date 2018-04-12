using SwinGameSDK;
// '' <summary>
// '' The GameController is responsible for controlling the game,
// '' managing user input, and displaying the current state of the
// '' game.
// '' </summary>
public class GameController
{

	private BattleShipsGame _theGame;

	private Player _human;

	private AIPlayer _ai;

	private Stack<GameState> _state = new Stack<GameState>();

	private AIOption _aiSetting;

	// '' <summary>
	// '' Returns the current state of the game, indicating which screen is
	// '' currently being used
	// '' </summary>
	// '' <value>The current state</value>
	// '' <returns>The current state</returns>
	public GameState CurrentState
	{
		get
		{
			return _state.Peek();
		}
	}

	public Player HumanPlayer
	{
		get
		{
			return _human;
		}
	}

	public Player ComputerPlayer
	{
		get
		{
			return _ai;
		}
	}

	GameController()
	{
		// bottom state will be quitting. If player exits main menu then the game is over
		_state.Push(GameState.Quitting);
		// at the start the player is viewing the main menu
		_state.Push(GameState.ViewingMainMenu);
	}

	// '' <summary>
	// '' Starts a new game.
	// '' </summary>
	// '' <remarks>
	// '' Creates an AI player based upon the _aiSetting.
	// '' </remarks>
	public static void StartGame()
	{
		if (_theGame)
		{
			IsNot;
			null;
			GameController.EndGame();
			// Create the game
			_theGame = new BattleShipsGame();
			// create the players
			switch (_aiSetting)
			{
				case AIOption.Medium:
					_ai = new AIMediumPlayer(_theGame);
					break;
				case AIOption.Hard:
					_ai = new AIHardPlayer(_theGame);
					break;
				default:
					_ai = new AIHardPlayer(_theGame);
					break;
			}
			_human = new Player(_theGame);
			AddHandler_human.PlayerGrid.Changed, AddressOf GridChanged;
			_ai.PlayerGrid.Changed += new System.EventHandler(this.GridChanged);
			_theGame.AttackCompleted += new System.EventHandler(this.AttackCompleted);
			GameController.AddNewState(GameState.Deploying);
		}

		// '' <summary>
		// '' Stops listening to the old game once a new game is started
		// '' </summary>
	}

	static void EndGame()
	{
		// RemoveHandler _human.PlayerGrid.Changed, AddressOf GridChanged
		_ai.PlayerGrid.Changed;
		new System.EventHandler(this.GridChanged);
		_theGame.AttackCompleted;
		new System.EventHandler(this.AttackCompleted);
	}

	// '' <summary>
	// '' Listens to the game grids for any changes and redraws the screen
	// '' when the grids change
	// '' </summary>
	// '' <param name="sender">the grid that changed</param>
	// '' <param name="args">not used</param>
	private static void GridChanged(object sender, EventArgs args)
	{
		GameController.DrawScreen();
		SwinGame.RefreshScreen();
	}

	private static void PlayHitSequence(int row, int column, bool showAnimation)
	{
		if (showAnimation)
		{
			AddExplosion(row, column);
		}

		Audio.PlaySoundEffect(GameSound("Hit"));
		DrawAnimationSequence();
	}

	private static void PlayMissSequence(int row, int column, bool showAnimation)
	{
		if (showAnimation)
		{
			AddSplash(row, column);
		}

		Audio.PlaySoundEffect(GameSound("Miss"));
		DrawAnimationSequence();
	}

	// '' <summary>
	// '' Listens for attacks to be completed.
	// '' </summary>
	// '' <param name="sender">the game</param>
	// '' <param name="result">the result of the attack</param>
	// '' <remarks>
	// '' Displays a message, plays sound and redraws the screen
	// '' </remarks>
	private static void AttackCompleted(object sender, AttackResult result)
	{
		bool isHuman;
		isHuman = (_theGame.Player == HumanPlayer);
		if (isHuman)
		{
			Message = ("You " + result.ToString());
		}
		else
		{
			Message = ("The AI " + result.ToString());
		}

		switch (result.Value)
		{
			case ResultOfAttack.Destroyed:
				GameController.PlayHitSequence(result.Row, result.Column, isHuman);
				Audio.PlaySoundEffect(GameSound("Sink"));
				break;
			case ResultOfAttack.GameOver:
				GameController.PlayHitSequence(result.Row, result.Column, isHuman);
				Audio.PlaySoundEffect(GameSound("Sink"));
				while (Audio.SoundEffectPlaying(GameSound("Sink")))
				{
					SwinGame.Delay(10);
					SwinGame.RefreshScreen();
				}

				if (HumanPlayer.IsDestroyed)
				{
					Audio.PlaySoundEffect(GameSound("Lose"));
				}
				else
				{
					Audio.PlaySoundEffect(GameSound("Winner"));
				}

				break;
			case ResultOfAttack.Hit:
				GameController.PlayHitSequence(result.Row, result.Column, isHuman);
				break;
			case ResultOfAttack.Miss:
				GameController.PlayMissSequence(result.Row, result.Column, isHuman);
				break;
			case ResultOfAttack.ShotAlready:
				Audio.PlaySoundEffect(GameSound("Error"));
				break;
		}
	}

	// '' <summary>
	// '' Completes the deployment phase of the game and
	// '' switches to the battle mode (Discovering state)
	// '' </summary>
	// '' <remarks>
	// '' This adds the players to the game before switching
	// '' state.
	// '' </remarks>
	public static void EndDeployment()
	{
		// deploy the players
		_theGame.AddDeployedPlayer(_human);
		_theGame.AddDeployedPlayer(_ai);
		GameController.SwitchState(GameState.Discovering);
	}

	// '' <summary>
	// '' Gets the player to attack the indicated row and column.
	// '' </summary>
	// '' <param name="row">the row to attack</param>
	// '' <param name="col">the column to attack</param>
	// '' <remarks>
	// '' Checks the attack result once the attack is complete
	// '' </remarks>
	public static void Attack(int row, int col)
	{
		AttackResult result;
		result = _theGame.Shoot(row, col);
		GameController.CheckAttackResult(result);
	}

	// '' <summary>
	// '' Gets the AI to attack.
	// '' </summary>
	// '' <remarks>
	// '' Checks the attack result once the attack is complete.
	// '' </remarks>
	private static void AIAttack()
	{
		AttackResult result;
		result = _theGame.Player.Attack();
		GameController.CheckAttackResult(result);
	}

	// '' <summary>
	// '' Checks the results of the attack and switches to
	// '' Ending the Game if the result was game over.
	// '' </summary>
	// '' <param name="result">the result of the last
	// '' attack</param>
	// '' <remarks>Gets the AI to attack if the result switched
	// '' to the AI player.</remarks>
	private static void CheckAttackResult(AttackResult result)
	{
		switch (result.Value)
		{
			case ResultOfAttack.Miss:
				if ((_theGame.Player == ComputerPlayer))
				{
					GameController.AIAttack();
				}

				break;
			case ResultOfAttack.GameOver:
				GameController.SwitchState(GameState.EndingGame);
				break;
		}
	}

	// '' <summary>
	// '' Handles the user SwinGame.
	// '' </summary>
	// '' <remarks>
	// '' Reads key and mouse input and converts these into
	// '' actions for the game to perform. The actions
	// '' performed depend upon the state of the game.
	// '' </remarks>
	public static void HandleUserInput()
	{
		// Read incoming input events
		SwinGame.ProcessEvents();
		switch (CurrentState)
		{
			case GameState.ViewingMainMenu:
				HandleMainMenuInput();
				break;
			case GameState.ViewingGameMenu:
				HandleGameMenuInput();
				break;
			case GameState.AlteringSettings:
				HandleSetupMenuInput();
				break;
			case GameState.Deploying:
				HandleDeploymentInput();
				break;
			case GameState.Discovering:
				HandleDiscoveryInput();
				break;
			case GameState.EndingGame:
				HandleEndOfGameInput();
				break;
			case GameState.ViewingHighScores:
				HandleHighScoreInput();
				break;
		}
		UpdateAnimations();
	}

	// '' <summary>
	// '' Draws the current state of the game to the screen.
	// '' </summary>
	// '' <remarks>
	// '' What is drawn depends upon the state of the game.
	// '' </remarks>
	public static void DrawScreen()
	{
		DrawBackground();
		switch (CurrentState)
		{
			case GameState.ViewingMainMenu:
				DrawMainMenu();
				break;
			case GameState.ViewingGameMenu:
				DrawGameMenu();
				break;
			case GameState.AlteringSettings:
				DrawSettings();
				break;
			case GameState.Deploying:
				DrawDeployment();
				break;
			case GameState.Discovering:
				DrawDiscovery();
				break;
			case GameState.EndingGame:
				DrawEndOfGame();
				break;
			case GameState.ViewingHighScores:
				DrawHighScores();
				break;
		}
		DrawAnimations();
		SwinGame.RefreshScreen();
	}

	// '' <summary>
	// '' Move the game to a new state. The current state is maintained
	// '' so that it can be returned to.
	// '' </summary>
	// '' <param name="state">the new game state</param>
	public static void AddNewState(GameState state)
	{
		_state.Push(state);
		Message = "";
	}

	// '' <summary>
	// '' End the current state and add in the new state.
	// '' </summary>
	// '' <param name="newState">the new state of the game</param>
	public static void SwitchState(GameState newState)
	{
		GameController.EndCurrentState();
		GameController.AddNewState(newState);
	}

	// '' <summary>
	// '' Ends the current state, returning to the prior state
	// '' </summary>
	public static void EndCurrentState()
	{
		_state.Pop();
	}

	// '' <summary>
	// '' Sets the difficulty for the next level of the game.
	// '' </summary>
	// '' <param name="setting">the new difficulty level</param>
	public static void SetDifficulty(AIOption setting)
	{
		_aiSetting = setting;
	}
}