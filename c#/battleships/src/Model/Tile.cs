
// Tile knows its location on the grid, if it is a ship and if it has been 
// shot before
// </summary>
public class Tile {
    
    private int _RowValue; // The row value of the tile

    private int _ColumnValue; // The column value of the tile

    private Ship _Ship = null; // The ship the tile belongs to

    private bool _Shot = false; // The tile has been shot at

    // Has the tile been shot?
    // Return true if the tile was shot
    public bool Shot {
        get {
            return _Shot;
        }
        set {
            _Shot = value;
        }
    }
    
    // The row of the tile in the grid
    // Return the row index of the tile
    public int Row {
        get {
            return _RowValue;
        }
    }
    
    
    // The column of the tile in the grid
    // Return the column of the tile in the grid
    public int Column {
        get {
            return _ColumnValue;
        }
    }
    
    // Ship allows for a tile to check if there is ship and add a ship to a tile
    public Ship Ship {
        get {
            return _Ship;
        }
        set {
            if ((_Ship == null)) {
                _Ship = value;
                if (value) {
                    IsNot;
                    null;
                    _Ship.AddTile(this);
                }
                
            }
            else {
                throw new InvalidOperationException(("There is already a ship at [" 
                                + (Row + (", " 
                                + (Column + "]")))));
            }
            
        }
    }

    // The tile constructor will know where it is on the grid, and is its a ship
    // <param name="row">the row on the grid</param>
    // <param name="col">the col on the grid</param>
    // <param name="ship">what ship it is</param>
    public Tile(int row, int col, Ship ship) {
        _RowValue = row;
        _ColumnValue = col;
        _Ship = ship;
    }
    
    // Clearship will remove the ship from the tile
    public void ClearShip() {
        _Ship = null;
    }
    
    // View is able to tell the grid what the tile is
    public TileView View {
        get {
            // if there is no ship in the tile
            if ((_Ship == null)) {
                // and the tile has been hit
                if (_Shot) {
                    return TileView.Miss;
                }
                else {
                    // and the tile hasn't been hit
                    return TileView.Sea;
                }
                
            }
            else {
                // if there is a ship and it has been hit
                if (_Shot) {
                    return TileView.Hit;
                }
                else {
                    // if there is a ship and it hasn't been hit
                    return TileView.Ship;
                }
                
            }
            
        }
    }
    
    // Shoot allows a tile to be shot at, and if the tile has been hit before
    // it will give an error
    internal void Shoot() {
        if ((false == Shot)) {
            Shot = true;
            if (_Ship) {
                IsNot;
                null;
                _Ship.Hit();
            }
            
        }
        else {
            throw new ApplicationException("You have already shot this square");
        }
        
    }
}