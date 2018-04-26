using System;

//========================================================================
// This conversion was produced by the Free Edition of
// Instant C# courtesy of Tangible Software Solutions.
// Order the Premium Edition at https://www.tangiblesoftwaresolutions.com
//========================================================================

/// <summary>
/// The values that are visable for a given tile.
/// </summary>
public enum TileView
{
	/// <summary>
	/// The viewer can see sea
	/// </summary>
	/// <remarks>
	/// May be masking a ship if viewed via a sea adapter
	/// </remarks>
	Sea,

	/// <summary>
	/// The viewer knows that site was attacked but nothing
	/// was hit
	/// </summary>
	Miss,

	/// <summary>
	/// The viewer can see a ship at this site
	/// </summary>
	Ship,

	/// <summary>
	/// The viewer knows that the site was attacked and
	/// something was hit
	/// </summary>
	Hit
}
