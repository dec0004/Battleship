// AttackResult gives the result after a shot has been made.
public class AttackResult {
    
    private ResultOfAttack _Value;
    
    private Ship _Ship;
    
    private string _Text;
    
    private int _Row;
    
    private int _Column;
    
    // Returns the result of the attack
    public ResultOfAttack Value {
        get {
            return _Value;
        }
    }
    
    // The ship, if any, involved in this result
    // Returns the ship, if any, involved in this result
    public Ship Ship {
        get {
            return _Ship;
        }
    }
    
    // Returns a textual description of the result
    public string Text {
        get {
            return _Text;
        }
    }

    // The row where the attack occurred
    public int Row {
        get {
            return _Row;
        }
    }

    // The column where the attack occurred
    public int Column {
        get {
            return _Column;
        }
    }

    // Set the _Value to the PossibleAttack value
    // <param name="value">either hit, miss, destroyed, shotalready</param>
    public AttackResult(ResultOfAttack value, string text, int row, int column) {
        _Value = value;
        _Text = text;
        _Ship = null;
        _Row = row;
        _Column = column;
    }
    
    // Set the _Value to the PossibleAttack value, and the _Ship to the ship
    // <param name="value">either hit, miss, destroyed, shotalready</param>
    // <param name="ship">the ship information</param>
    public AttackResult(ResultOfAttack value, Ship ship, string text, int row, int column) : 
            this(value, text, row, column) {
        _Ship = ship;
    }
    
    // Displays the textual information about the attack
    // Returns The textual information about the attack
    public override string ToString() {
        if ((_Ship == null)) {
            return Text;
        }
        
        return (Text + (" " + _Ship.Name));
    }
}