using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SwinGameSDK;

//<summary>
// Controls displaying and collecting high score data.
// </summary>
// <remarks>
// Data is saved to a file.
// </remarks>
internal static class HighScoreController
{
    private const int NAME_WIDTH = 5; //changed to accept 5 character high scores
    private const int SCORES_LEFT = 490;

    private static string _AI_Difficulty_str; // Used to calculate the player's score. Higher difficulty = more points

    public static string AI_Difficulty_str
    {
        get
        {
            return "(" + _AI_Difficulty_str + ")";
        }
        set
        {
            _AI_Difficulty_str = value;
        }
    }

    // <summary>
    // The score structure is used to keep the name and
    // score of the top players together.
    // </summary>
    private struct Score : IComparable
    {
        public string Name;
        public int Value;
        public string Difficulty;

        // <summary>
        // Allows scores to be compared to facilitate sorting
        // </summary>
        // <param name="obj">the object to compare to</param>
        // <returns>a value that indicates the sort order</returns>
        public int CompareTo(object obj)
        {
            if (obj is Score)
            {
                Score other = (Score)obj;

                return other.Value - this.Value;
            }
            else
            {
                return 0;
            }
        }
    }

    private static List<Score> _Scores = new List<Score>();

    // <summary>
    // Loads the scores from the highscores text file.
    // </summary>
    // <remarks>
    // The format is
    // # of scores
    // NNNSSS
    // 
    // Where NNN is the name and SSS is the score
    // </remarks>
    private static void LoadScores()
    {
        string filename = SwinGame.PathToResource("highscores.txt");

        StreamReader input = new StreamReader(filename);

        //Read in the # of scores
        int numScores = Convert.ToInt32(input.ReadLine());

        _Scores.Clear();

        int i = 0;

        Score s = new Score();
        for (i = 1; i <= numScores; i++)
        {
            string line = input.ReadLine();


            s.Name = line.Substring(0, NAME_WIDTH);

            string tempLine = line.Split('(', ')')[1]; // Get the difficulty level
            s.Difficulty = "(" + tempLine + ")"; // Add paranthesis so it's formatted correctly. It'll be (easy), (medium), or (hard)

            // Changed it so it gets the value after the difficulty
            // which will be written as (easy), (med) or (hard);
            s.Value = Convert.ToInt32(line.Substring(line.IndexOf(")") + 1)); // Start reading right after the ')'
            _Scores.Add(s);
        }
        input.Close();
    }

    // <summary>
    // Saves the scores back to the highscores text file.
    // </summary>
    // <remarks>
    // The format is
    // # of scores
    // NNNSSS
    // 
    // Where NNN is the name and SSS is the score
    // </remarks>
    private static void SaveScores()
    {
        string filename = SwinGame.PathToResource("highscores.txt");

        StreamWriter output = new StreamWriter(filename);

        output.WriteLine(_Scores.Count);

        foreach (Score s in _Scores)
        {
            output.WriteLine(s.Name + s.Difficulty + s.Value); // TODO: CHECK IF THIS WORKS
        }

        output.Close();
    }







    // <summary>
    // Draws the high scores to the screen.
    // </summary>
    public static void DrawHighScores()
    {
        const int SCORES_HEADING = 40;
        const int SCORES_TOP = 80;
        const int SCORE_GAP = 30;

        if (_Scores.Count == 0)
        {
            LoadScores();
        }

        SwinGame.DrawText("   High Scores   ", Color.White, GameResources.GameFont("Courier"), SCORES_LEFT, SCORES_HEADING);

        //For all of the scores
        int i = 0;
        for (i = 0; i < _Scores.Count; i++)
        {
            Score s = _Scores[i];


            //for scores 1 - 9 use 01 - 09
            // TODO: ADD Difficulty as text here
            if (i < 9)
            { 
                SwinGame.DrawText(" " + (i + 1) + ":   " + s.Name + "   " + s.Value, Color.White, GameResources.GameFont("Courier"), SCORES_LEFT, SCORES_TOP + i * SCORE_GAP);
                SwinGame.DrawText(s.Difficulty, Color.White, GameResources.GameFont("Courier"), SCORES_LEFT + 150, SCORES_TOP + i * SCORE_GAP);
            }
            else
            {
                SwinGame.DrawText((i + 1) + ":   " + s.Name + "   " + s.Value, Color.White, GameResources.GameFont("Courier"), SCORES_LEFT, SCORES_TOP + i * SCORE_GAP);
                SwinGame.DrawText(s.Difficulty, Color.White, GameResources.GameFont("Courier"), SCORES_LEFT + 150, SCORES_TOP + i * SCORE_GAP);

            }
        }
    }






    // <summary>
    // Handles the user input during the top score screen.
    // </summary>
    // <remarks></remarks>
    public static void HandleHighScoreInput()
    {
        if (SwinGame.MouseClicked(MouseButton.LeftButton) || SwinGame.KeyTyped(KeyCode.EscapeKey) || SwinGame.KeyTyped(KeyCode.ReturnKey))
        {
            GameController.EndCurrentState();
        }
    }

    // <summary>
    // Read the user's name for their highsSwinGame.
    // </summary>
    // <param name="value">the player's sSwinGame.</param>
    // <remarks>
    // This verifies if the score is a highsSwinGame.
    // </remarks>
    public static void ReadHighScore(int value)
    {
        const int ENTRY_TOP = 500;

        if (_Scores.Count == 0)
        {
            LoadScores();
        }

        // Is it a high score
        if (value > _Scores[_Scores.Count - 1].Value)
        {
            Score s = new Score();

            // Change value depending on difficulty chosen by the player
            // Hard = 50% higher score
            // Medium = 25% higher score
            if (AI_Difficulty_str == "(hard)")
            {
                s.Value = Convert.ToInt32(value * 1.5); // Need to convert the value to an integer since multiplying requires a double
            }
            else if (AI_Difficulty_str == "(medium)")
            {
                s.Value = Convert.ToInt32(value * 1.25); // Need to convert the value to an integer since multiplying requires a double
            }
            else
            {
                s.Value = value;
            }

            // Added difficulty - Saves the difficulty of the AI once you've beaten them
            s.Difficulty = AI_Difficulty_str; // Add paranthesis so it's formatted correctly. It'll be (easy), (medium), or (hard)

            GameController.AddNewState(GameState.ViewingHighScores);

            int x = SCORES_LEFT + SwinGame.TextWidth(GameResources.GameFont("Courier"), "Name: ");

            SwinGame.StartReadingText(Color.White, NAME_WIDTH, GameResources.GameFont("Courier"), x, ENTRY_TOP);

            // Read the text from the user
            while (SwinGame.ReadingText())
            {
                SwinGame.ProcessEvents();

                UtilityFunctions.DrawBackground();
                DrawHighScores();
                SwinGame.DrawText("Name: ", Color.White, GameResources.GameFont("Courier"), SCORES_LEFT, ENTRY_TOP);
                SwinGame.RefreshScreen();
            }

            s.Name = SwinGame.TextReadAsASCII();

            if (s.Name.Length < 3)
            {
                s.Name = s.Name + new string(' ', 3 - s.Name.Length);
            }

            _Scores.RemoveAt(_Scores.Count - 1);
            _Scores.Add(s);
            _Scores.Sort();
            SaveScores();

            GameController.EndCurrentState();
        }
    }
}
