// '' <summary>
// '' Tile knows its location on the grid, if it is a ship and if it has been 
// '' shot before
// '' </summary>
public class Tile {
    
    private int _RowValue;
    
    private int _ColumnValue;
    
    // the column value of the tile
    private Ship _Ship = null;
    
    private bool _Shot = false;
    
    public bool Shot {
        get {
            return _Shot;
        }
        set {
            _Shot = value;
        }
    }
    
    public int Row {
        get {
            return _RowValue;
        }
    }
    
    public int Column {
        get {
            return _ColumnValue;
        }
    }
    
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
    
    public Tile(int row, int col, Ship ship) {
        _RowValue = row;
        _ColumnValue = col;
        _Ship = ship;
    }
    
    // '' <summary>
    // '' Clearship will remove the ship from the tile
    // '' </summary>
    public void ClearShip() {
        _Ship = null;
    }
    
    // '' <summary>
    // '' View is able to tell the grid what the tile is
    // '' </summary>
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