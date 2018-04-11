// '' <summary>
// '' AttackResult gives the result after a shot has been made.
// '' </summary>
public class AttackResult {
    
    private ResultOfAttack _Value;
    
    private Ship _Ship;
    
    private string _Text;
    
    private int _Row;
    
    private int _Column;
    
    // '' <summary>
    // '' The result of the attack
    // '' </summary>
    // '' <value>The result of the attack</value>
    // '' <returns>The result of the attack</returns>
    public ResultOfAttack Value {
        get {
            return _Value;
        }
    }
    
    public Ship Ship {
        get {
            return _Ship;
        }
    }
    
    public string Text {
        get {
            return _Text;
        }
    }
    
    public int Row {
        get {
            return _Row;
        }
    }
    
    public int Column {
        get {
            return _Column;
        }
    }
    
    public AttackResult(ResultOfAttack value, string text, int row, int column) {
        _Value = value;
        _Text = text;
        _Ship = null;
        _Row = row;
        _Column = column;
    }
    
    // '' <summary>
    // '' Set the _Value to the PossibleAttack value, and the _Ship to the ship
    // '' </summary>
    // '' <param name="value">either hit, miss, destroyed, shotalready</param>
    // '' <param name="ship">the ship information</param>
    public AttackResult(ResultOfAttack value, Ship ship, string text, int row, int column) : 
            this(value, text, row, column) {
        _Ship = ship;
    }
    
    // '' <summary>
    // '' Displays the textual information about the attack
    // '' </summary>
    // '' <returns>The textual information about the attack</returns>
    public override string ToString() {
        if ((_Ship == null)) {
            return Text;
        }
        
        return (Text + (" " + _Ship.Name));
    }
}