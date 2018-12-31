///
/// Game Framework
///  

/// To create a GameMode for your game, derive from this
/// class, check in GameManager the current state of the game 
/// mode and change logic 

namespace GameFramework
{
    /// <summary>
    /// Current GameMode State
    /// </summary>
    public enum GameModeState
    {
        /// Nothing happen
        Idle,

        /// The game over condition it occurred 
        Over,

        /// The game win  condition it occurred
        Win
    }

    /// <summary>
    /// GameMode abstract class containing all the logic to define
    /// three base state in a game: Win, Lose, Current State.
    /// </summary>
    public abstract class GameMode : UnityEngine.ScriptableObject
    {        
        /// <summary>
        /// When GameMode Start, often used to subscribe GameMode
        /// to events and initialize some variables.
        /// </summary>
        public abstract void Enable();

        /// <summary>
        /// Check the current state of the game mode.
        /// </summary>
        /// <returns>Retrun a GameModeState enum defining the condition of the game</returns>
        public abstract GameModeState CheckGameModeCondition();

        /// <summary>
        /// When GameMode Stop, often used to unsubscribe the GameMode
        /// from events.
        /// </summary>
        public abstract void Disable();
    }
}
