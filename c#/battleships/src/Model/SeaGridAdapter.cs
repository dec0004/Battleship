// The SeaGridAdapter allows for the change in a sea grid view. Whenever a ship is
// presented it changes the view into a sea tile instead of a ship tile.
// The purpose of this is to hide the enemy's ship from the player.
public class SeaGridAdapter : ISeaGrid {
    
    private SeaGrid _MyGrid;
    
    // Create the SeaGridAdapter, with the grid, and it will allow it to be changed
    // <param name="grid">the grid that needs to be adapted</param>
    public SeaGridAdapter(SeaGrid grid) {
        _MyGrid = grid;
        _MyGrid.Changed += new EventHandler(// TODO: Warning!!!! NULL EXPRESSION DETECTED...
        .);
    }
    
    // MyGrid_Changed causes the grid to be redrawn by raising a changed event
    // <param name="sender">the object that caused the change</param>
    // <param name="e">what needs to be redrawn</param>
    private void MyGrid_Changed(object sender, EventArgs e) {
        Changed(this, e);
    }
    
    // Changes the discovery grid. Where there is a ship we will sea water
    // <param name="x">tile x coordinate</param>
    // <param name="y">tile y coordinate</param>
    // Returns a tile, either what it actually is, or if it was a ship then return a sea tile</returns>
    public TileView this[int x, int y] {
    }
}
EndPropertyImplementsISeaGrid.Changed;
Endclass Unknown {
}

    
    // Indicates that the grid has been changed
    public event EventHandler Changed;
    
    // Get the width of a tile
    public int Width {
        get {
            return _MyGrid.Width;
        }
    }

    // Get the height of the tile
    public int Height {
        get {
            return _MyGrid.Height;
        }
    }

    // HitTile calls upon _MyGrid to hit a tile at the row, col
    // <param name="row">the row its hitting at</param>
    // <param name="col">the column its hitting at</param>
    // Returns the result from hitting that tile
    public AttackResult HitTile(int row, int col) {
        return _MyGrid.HitTile(row, col);
    }