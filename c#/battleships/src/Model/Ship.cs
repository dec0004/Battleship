// '' <summary>
// '' A Ship has all the details about itself. For example the shipname,
// '' size, number of hits taken and the location. Its able to add tiles,
// '' remove, hits taken and if its deployed and destroyed.
// '' </summary>
// '' <remarks>
// '' Deployment information is supplied to allow ships to be drawn.
// '' </remarks>
public class Ship {
    
    private ShipName _shipName;
    
    private int _sizeOfShip;
    
    private int _hitsTaken = 0;
    
    private List<Tile> _tiles;
    
    private int _row;
    
    private int _col;
    
    private Direction _direction;
    
    // '' <summary>
    // '' The type of ship
    // '' </summary>
    // '' <value>The type of ship</value>
    // '' <returns>The type of ship</returns>
    public string Name {
        get {
            if ((_shipName == ShipName.AircraftCarrier)) {
                return "Aircraft Carrier";
            }
            
            return _shipName.ToString();
        }
    }
    
    public int Size {
        get {
            return _sizeOfShip;
        }
    }
    
    public int Hits {
        get {
            return _hitsTaken;
        }
    }
    
    public int Row {
        get {
            return _row;
        }
    }
    
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
        _tiles = new List<Tile>();
        // gets the ship size from the enumarator
        _sizeOfShip = _shipName;
    }
    
    // '' <summary>
    // '' Add tile adds the ship tile
    // '' </summary>
    // '' <param name="tile">one of the tiles the ship is on</param>
    public void AddTile(Tile tile) {
        _tiles.Add(tile);
    }
    
    // '' <summary>
    // '' Remove clears the tile back to a sea tile
    // '' </summary>
    public void Remove() {
        foreach (Tile tile in _tiles) {
            tile.ClearShip();
        }
        
        _tiles.Clear();
    }
    
    public void Hit() {
        _hitsTaken = (_hitsTaken + 1);
    }
    
    // '' <summary>
    // '' IsDeployed returns if the ships is deployed, if its deplyed it has more than
    // '' 0 tiles
    // '' </summary>
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
    
    internal void Deployed(Direction direction, int row, int col) {
        _row = row;
        _col = col;
        _direction = direction;
    }
}