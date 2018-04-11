// '' <summary>
// '' The result of an attack.
// '' </summary>
public enum ResultOfAttack {
    Hit, // '' The player hit something

    Miss, // '' The player missed

    Destroyed, // '' The player destroyed a ship

    ShotAlready, // '' That location was already shot.

    GameOver, // '' The player killed all of the opponents ships
}