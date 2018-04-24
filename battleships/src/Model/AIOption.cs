using System;

//========================================================================
// This conversion was produced by the Free Edition of
// Instant C# courtesy of Tangible Software Solutions.
// Order the Premium Edition at https://www.tangiblesoftwaresolutions.com
//========================================================================

/// <summary>
/// The different AI levels.
/// </summary>
public enum AIOption
{
	/// <summary>
	/// Easy, total random shooting
	/// </summary>
	Easy,

	/// <summary>
	/// Medium, marks squares around hits
	/// </summary>
	Medium,

	/// <summary>
	/// As medium, but removes shots once it misses
	/// </summary>
	Hard
}
