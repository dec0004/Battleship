using System.IO;
using SwinGameSDK;
//Controls displaying and collecting high score data.
//Data is saved to a file.
class HighScoreController
{

	private const int NAME_WIDTH = 3;

	private const int SCORES_LEFT = 490;

	//The score structure is used to keep the name and score of the top players together.
	private struct Score
	{

		public string Name;

		public int Value;

		//Allows scores to be compared to facilitate sorting
		//parameter 'obj': the object to compare to
		//returns a value that indicates the sort order
		public static int CompareTo(object obj)
		{
			if ((obj.GetType() == Score))
			{
				Score other = ((Score)(obj));
				return (other.Value - this.Value);
			}
			else
			{
				return 0;
			}

		}
	}

	private List<Score> _Scores = new List<Score>();

	private static void LoadScores()
	{
		string filename;
		filename = SwinGame.PathToResource("highscores.txt");
		StreamReader input;
		input = new StreamReader(filename);
		// Read in the # of scores
		int numScores;
		numScores = Convert.ToInt32(input.ReadLine());
		_Scores.Clear();
		int i;
		for (i = 1; (i <= numScores); i++)
		{
			Score s;
			string line;
			line = input.ReadLine();
			s.Name = line.Substring(0, NAME_WIDTH);
			s.Value = Convert.ToInt32(line.Substring(NAME_WIDTH));
			_Scores.Add(s);
		}

		input.Close();
	}

	//Saves the scores back to the highscores text file.
	//The format is # of scores NNNSSS, where NNN is the name and SSS is the score
	private static void SaveScores()
	{
		string filename;
		filename = SwinGame.PathToResource("highscores.txt");
		StreamWriter output;
		output = new StreamWriter(filename);
		output.WriteLine(_Scores.Count);
		foreach (Score s in _Scores)
		{
			output.WriteLine((s.Name + s.Value));
		}

		output.Close();
	}

	//Draws the high scores to the screen.
	public static void DrawHighScores()
	{
		const int SCORES_HEADING = 40;
		const int SCORES_TOP = 80;
		const int SCORE_GAP = 30;
		if ((_Scores.Count == 0))
		{
			HighScoreController.LoadScores();
		}

		SwinGame.DrawText("   High Scores   ", Color.White, GameFont("Courier"), SCORES_LEFT, SCORES_HEADING);
		// For all of the scores
		int i;
		for (i = 0; (i
					<= (_Scores.Count - 1)); i++)
		{
			Score s;
			s = _Scores.Item[i];
			// for scores 1 - 9 use 01 - 09
			if ((i < 9))
			{
				SwinGame.DrawText((" "
								+ ((i + 1) + (":   "
								+ (s.Name + ("   " + s.Value))))), Color.White, GameFont("Courier"), SCORES_LEFT, (SCORES_TOP
								+ (i * SCORE_GAP)));
			}
			else
			{
				SwinGame.DrawText(((i + 1) + (":   "
								+ (s.Name + ("   " + s.Value)))), Color.White, GameFont("Courier"), SCORES_LEFT, (SCORES_TOP
								+ (i * SCORE_GAP)));
			}

		}

	}

	//Handles the user input during the top score screen.
	public static void HandleHighScoreInput()
	{
		if ((SwinGame.MouseClicked(MouseButton.LeftButton)
					|| (SwinGame.KeyTyped(KeyCode.VK_ESCAPE) || SwinGame.KeyTyped(KeyCode.VK_RETURN))))
		{
			EndCurrentState();
		}

	}

	//Read the user's name for their highsSwinGame.
	//parameter 'value': the player's score
	//This verifies is the score is a high score

	public static void ReadHighScore(int value)
	{
		const int ENTRY_TOP = 500;
		if ((_Scores.Count == 0))
		{
			HighScoreController.LoadScores();
		}

		// is it a high score
		if ((value > _Scores.Item[(_Scores.Count - 1)].Value))
		{
			Score s = new Score();
			s.Value = value;
			AddNewState(GameState.ViewingHighScores);
			int x;
			x = (SCORES_LEFT + SwinGame.TextWidth(GameFont("Courier"), "Name: "));
			SwinGame.StartReadingText(Color.White, NAME_WIDTH, GameFont("Courier"), x, ENTRY_TOP);
			// Read the text from the user
			while (SwinGame.ReadingText())
			{
				SwinGame.ProcessEvents();
				DrawBackground();
				HighScoreController.DrawHighScores();
				SwinGame.DrawText("Name: ", Color.White, GameFont("Courier"), SCORES_LEFT, ENTRY_TOP);
				SwinGame.RefreshScreen();
			}

			s.Name = SwinGame.TextReadAsASCII();
			if ((s.Name.Length < 3))
			{
				s.Name = (s.Name + new string(((char)(" ")), (3 - s.Name.Length)));
			}

			_Scores.RemoveAt((_Scores.Count - 1));
			_Scores.Add(s);
			_Scores.Sort();
			EndCurrentState();
		}

	}
}