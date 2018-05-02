using System;
using System.Collections.Generic;

//<summary>
// The AIEasyPlayer is a type of AIPlayer that will just aim randomly every time
// </summary>
public class AIEasyPlayer : AIPlayer
{

	private Stack<Location> _Targets = new Stack<Location>();

	public AIEasyPlayer(BattleShipsGame controller) : base(controller)
	{
	}

	// <summary>
	// GenerateCoordinates should generate random shooting coordinates
	// only when it has not found a ship, or has destroyed a ship and 
	// needs new shooting coordinates
	// </summary>
	// <param name="row">the generated row</param>
	// <param name="column">the generated column</param>
	protected override void GenerateCoords(ref int row, ref int column)
	{
		do
		{
		    SearchCoords(ref row, ref column);

		} while (row < 0 || column < 0 || row >= EnemyGrid.Height || column >= EnemyGrid.Width || EnemyGrid.get_Item(row, column) != TileView.Sea); //while inside the grid and not a sea tile do the search
	}

	// <summary>
	// SearchCoords will randomly generate shots within the grid as long as its not hit that tile already
	// </summary>
	// <param name="row">the generated row</param>
	// <param name="column">the generated column</param>
	private void SearchCoords(ref int row, ref int column)
	{
		row = _Random.Next(0, EnemyGrid.Height);
		column = _Random.Next(0, EnemyGrid.Width);
	}

    protected override void ProcessShot(int row, int col, AttackResult result)
    {
        // Do nothing
    }

}
