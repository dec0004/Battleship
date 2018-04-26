using System;

//========================================================================
// This conversion was produced by the Free Edition of
// Instant C# courtesy of Tangible Software Solutions.
// Order the Premium Edition at https://www.tangiblesoftwaresolutions.com
//========================================================================

/// <summary>
/// The ISeaGrid defines the read only interface of a Grid. This
/// allows each player to see and attack their opponents grid.
/// </summary>
public interface ISeaGrid
{

	int Width {get;}

	int Height {get;}

	/// <summary>
	/// Indicates that the grid has changed.
	/// </summary>
	event EventHandler Changed;

	/// <summary>
	/// Provides access to the given row/column
	/// </summary>
	/// <param name="row">the row to access</param>
	/// <param name="column">the column to access</param>
	/// <value>what the player can see at that location</value>
	/// <returns>what the player can see at that location</returns>
//INSTANT C# NOTE: C# does not support parameterized properties - the following property has been rewritten as a function:
//ORIGINAL LINE: ReadOnly Property Item(ByVal row As Integer, ByVal column As Integer) As TileView
	TileView get_Item(int row, int column);

	/// <summary>
	/// Mark the indicated tile as shot.
	/// </summary>
	/// <param name="row">the row of the tile</param>
	/// <param name="col">the column of the tile</param>
	/// <returns>the result of the attack</returns>
	AttackResult HitTile(int row, int col);
}
