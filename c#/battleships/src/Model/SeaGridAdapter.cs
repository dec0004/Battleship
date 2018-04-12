// '' <summary>
// '' The SeaGridAdapter allows for the change in a sea grid view. Whenever a ship is
// '' presented it changes the view into a sea tile instead of a ship tile.
// '' </summary>
public class SeaGridAdapter : ISeaGrid {
    
    private SeaGrid _MyGrid;
    
    // '' <summary>
    // '' Create the SeaGridAdapter, with the grid, and it will allow it to be changed
    // '' </summary>
    // '' <param name="grid">the grid that needs to be adapted</param>
    public SeaGridAdapter(SeaGrid grid) {
        _MyGrid = grid;
        _MyGrid.Changed += new EventHandler(// TODO: Warning!!!! NULL EXPRESSION DETECTED...
        .);
    }
    
    // '' <summary>
    // '' MyGrid_Changed causes the grid to be redrawn by raising a changed event
    // '' </summary>
    // '' <param name="sender">the object that caused the change</param>
    // '' <param name="e">what needs to be redrawn</param>
    private void MyGrid_Changed(object sender, EventArgs e) {
        Changed(this, e);
    }
    
    // '' <summary>
    // '' Changes the discovery grid. Where there is a ship we will sea water
    // '' </summary>
    // '' <param name="x">tile x coordinate</param>
    // '' <param name="y">tile y coordinate</param>
    // '' <returns>a tile, either what it actually is, or if it was a ship then return a sea tile</returns>
    public TileView this[int x, int y] {
    }
}
EndPropertyImplementsISeaGrid.Changed;
Endclass Unknown {
}

    
    // '' <summary>
    // '' Indicates that the grid has been changed
    // '' </summary>
    public event EventHandler Changed;
    
    // '' <summary>
    // '' Get the width of a tile
    // '' </summary>
    public int Width {
        get {
            return _MyGrid.Width;
        }
    }
    
    public int Height {
        get {
            return _MyGrid.Height;
        }
    }
    
    public AttackResult HitTile(int row, int col) {
        return _MyGrid.HitTile(row, col);
    }