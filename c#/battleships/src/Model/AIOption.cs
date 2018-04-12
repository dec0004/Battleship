//  The different AI levels.
public enum AIOption {
    Easy, // Easy, total random shooting

    Medium, // Medium, marks squares around hits

    Hard, // As medium, but removes shots once it misses. Also once it hits successfully, will continue to try and find rest of ship
	//does not shoot randomly
}