// A Ship has all the details about itself. For example the shipname,
// size, number of hits taken and the location. It's able to add tiles,
// remove tiles, hits taken and if it's deployed and destroyed.

// Deployment information is supplied to allow ships to be drawn.

public class Ship {
    
    private ShipName _shipName;
    
    private int _sizeOfShip;
    
    private int _hitsTaken = 0;
    
    private List<Tile> _tiles; // The tiles the ship is occupying.
    
    private int _row;
    
    private int _col;
    
    private Direction _direction;
    

    // The type of ship
    // Returns the name of the type of ship
    public string Name {
        get {
            if ((_shipName == ShipName.AircraftCarrier)) {
                return "Aircraft Carrier";
            }
            
            return _shipName.ToString();
        }
    }
    
    // The number of cells that this ship occupies.
    // Returns The number of hits the ship can take
    public int Size {
        get {
            return _sizeOfShip;
        }
    }
    
    // The number of hits that the ship has taken.
    // Returns The number of hits the ship has taken
    // Note: When this equals Size the ship is sunk
    public int Hits {
        get {
            return _hitsTaken;
        }
    }
    
    // The row location of the ship.
    // Returns the topmost location of the ship
    public int Row {
        get {
            return _row;
        }
    }
    
    // The column of the ship
    public int Column {
        get {
            return _col;
        }
    }
    

    public Direction Direction {
        get {
            return _direction;
        }
    }
    
    public Ship(ShipName ship) {
        _shipName = ship;
        _tiles = new List<Tile>(); // Used to store what tiles the ship is occupying.
        // Gets the ship size from the enumerator
        _sizeOfShip = _shipName;
    }
    

    // Add tile adds the ship tile
    // <param name="tile">one of the tiles the ship is on</param>
    public void AddTile(Tile tile) {
        _tiles.Add(tile);
    }
    
    // Remove clears the tile back to a sea tile
    public void Remove() {
        foreach (Tile tile in _tiles) {
            tile.ClearShip();
        }
        
        _tiles.Clear();
    }
    
    public void Hit() {
        _hitsTaken = (_hitsTaken + 1);
    }
    
    // IsDeployed returns if the ships is deployed, if its deplyed it has more than
    // 0 tiles
    public bool IsDeployed {
        get {
            return (_tiles.Count > 0);
        }
    }
    
    public bool IsDestroyed {
        get {
            return;
        }
    }
    

    // Record that the ship is now deployed.
    // <param name="direction"></param>
    // <param name="row"></param>
    // <param name="col"></param>
    internal void Deployed(Direction direction, int row, int col) {
        _row = row;
        _col = col;
        _direction = direction;
    }
}