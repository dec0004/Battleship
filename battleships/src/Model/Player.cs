// Player has its own _PlayerGrid, and can see an _EnemyGrid, it can also check if
// all ships are deployed and if all ships are detroyed. A Player can also attach.
public class Player {
    
    protected static Random _Random = new Random();
    
    private Dictionary<ShipName, Ship> _Ships = new Dictionary<ShipName, Ship>();
    
    private SeaGrid _playerGrid = new SeaGrid(_Ships);
    
    private ISeaGrid _enemyGrid;
    
    protected BattleShipsGame _game;
    
    private int _shots;
    
    private int _hits;
    
    private int _misses;
    
    // Returns the game that the player is part of.
    // Returns the game that the player is playing
    public BattleShipsGame Game {
        get {
            return _game;
        }
        set {
            _game = value;
        }
    }

    // Sets the grid of the enemy player
    public ISeaGrid Enemy {
        set {
            _enemyGrid = value;
        }
    }
    
    public Player(BattleShipsGame controller) {
        _game = controller;
        // for each ship add the ships name so the seagrid knows about them
        foreach (ShipName name in Enum.GetValues(typeof(ShipName))) {
            if ((name != ShipName.None)) {
                _Ships.Add(name, new Ship(name));
            }
            
        }
        
        this.RandomizeDeployment();
    }
    
    // The EnemyGrid is a ISeaGrid because you shouldn't be allowed to see the enemies ships
    public ISeaGrid EnemyGrid {
        get {
            return _enemyGrid;
        }
        set {
            _enemyGrid = value;
        }
    }

    // The PlayerGrid is just a normal SeaGrid where the players ships can be deployed and seen
    public SeaGrid PlayerGrid {
        get {
            return _playerGrid;
        }
    }

    // ReadyToDeploy returns true if all ships are deployed
    public bool ReadyToDeploy {
        get {
            return _playerGrid.AllDeployed;
        }
    }
    
    public bool IsDestroyed {
        get {
            // Check if all ships are destroyed... -1 for the none ship
            return;
        }
    }
    
    // Returns the Player's ship with the given name.
    // <param name="name">the name of the ship to return</param>
    // Note: The none ship returns nothing/null
    public Ship Ship {
        get {
            if ((name == ShipName.None)) {
                return null;
            }
            
            return _Ships.Item[name];
        }
    }

    // Returns the number of shots the player has made
    public int Shots {
        get {
            return _shots;
        }
    }
    
    public int Hits {
        get {
            return _hits;
        }
    }
    
    // Returns the number of shots that have missed ships
    public int Missed {
        get {
            return _misses;
        }
    }
    
    public int Score {
        get {
            if (IsDestroyed) {
                return 0;
            }
            else {
                return ((Hits * 12) 
                            - (Shots 
                            - (PlayerGrid.ShipsKilled * 20)));
            }
            
        }
    }
    
    // Makes it possible to enumerate over the ships the player has.
    // Returns a ship enumerator
    public IEnumerator<Ship> GetShipEnumerator() {
        Ship[,] result;
        _Ships.Values.CopyTo(result, 0);
        List<Ship> lst = new List<Ship>();
        lst.AddRange(result);
        return lst.GetEnumerator();
    }

    // Makes it possible to enumerate over the ships the player has.
    // Returns a ship enumerator
    public IEnumerator GetEnumerator() {
        Ship[,] result;
        _Ships.Values.CopyTo(result, 0);
        List<Ship> lst = new List<Ship>();
        lst.AddRange(result);
        return lst.GetEnumerator();
    }
    
    // Virtual Attack allows the player to shoot
    public virtual AttackResult Attack() {
        // human does nothing here...
        return null;
    }
    
    // Shoot at a given row/column
    // <param name="row">the row to attack</param>
    // <param name="col">the column to attack</param>
    // Returns the result of the attack
    internal AttackResult Shoot(int row, int col) {
        _shots++;
        AttackResult result;
        result = EnemyGrid.HitTile(row, col);
        switch (result.Value) {
            case ResultOfAttack.Destroyed:
            case ResultOfAttack.Hit:
                _hits++;
                break;
            case ResultOfAttack.Miss:
                _misses++;
                break;
        }
        return result;
    }
    
    public virtual void RandomizeDeployment() {
        bool placementSuccessful;
        Direction heading;
        // Reiterate for each ship to deploy in ship list
        foreach (ShipName shipToPlace in Enum.GetValues(typeof(ShipName))) {
            if ((shipToPlace == ShipName.None)) {

			 continue;
                // TODO: Continue For... Warning!!! not translated
            }
            
            placementSuccessful = false;
            if (!placementSuccessful)
			{
                int dir = _Random.Next(2);
                int x = _Random.Next(0, 11);
                int y = _Random.Next(0, 11);
                if ((dir == 0)) {
                    heading = Direction.UpDown;
                }
                else {
                    heading = Direction.LeftRight;
                }
                
                // try to place ship, if position unplaceable, generate new coordinates
                try {
                    PlayerGrid.MoveShip(x, y, shipToPlace, heading);
                    placementSuccessful = true;
                }
                catch (System.Exception placementSuccessful) {
                    false;
                }
                
            }
            
        }
        
    }
}