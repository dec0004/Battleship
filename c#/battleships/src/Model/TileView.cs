// The values that are visible for a given tile.
public enum TileView {

    Sea, // The viewer can see sea. May be masking a ship if viewed via a sea adapter
   
    Miss, // The viewer knows that site was attacked but nothing was hit 

    Ship, // The viewer can see a ship at this site

    Hit, // The viewer knows that the site was attacked and something was hit
}