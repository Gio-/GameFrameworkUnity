///
/// Game Framework
///  

/// Definiton of all possible game state. Add
/// new one based on your needs.

namespace GameFramework
{
    /// <summary>
    /// Define the current State of the Game. 
    /// </summary>
    public enum GameStateTypes
    {
        /// The Game is not running but is ready to
        /// start.
        Ready = 1 << 1,
        /// Game is running and players are ready
        /// to play or they are already playing.
        Running = 1 << 2,

        /// Game is over and players lose this
        /// round.
        GameOver = 1 << 3,

        /// Game is over and players win this 
        /// round.
        GameWin = 1 << 4,

        /// Game is not over yet, but it is 
        /// temporarily stopped.
        Pause = 1 << 5
    }   

    /// <summary>
    /// Static class that holding and manage the current state of 
    /// game.abstract
    /// </summary>
    public static class GameState
    {
        /// Current State of the Game
        private static GameStateTypes   m_currentGameState; 

        /// <summary>
        /// Current State of the game.
        /// </summary>
        /// <value>Return a GameStateType value</value>
        public  static GameStateTypes   CurrentGameState 
        { 
            get { return m_currentGameState; } 
            set { ChangeGameState(value);    }
        }

        public delegate void  GameStateChangeHandler(GameStateTypes newGameState);
        public static   event GameStateChangeHandler OnGameStateChange;   

        /// <summary>
        /// Set a Current State of the Game
        /// </summary>
        /// <param name="newGameState">New Game state to set</param>
        public static void ChangeGameState(GameStateTypes newGameState)
        {
            if(m_currentGameState == newGameState)
                return;    

            m_currentGameState = newGameState;  

            /// Throw an event when game state 
            /// change.
            if(OnGameStateChange != null)
                OnGameStateChange(m_currentGameState);
        }


        public static bool IsStateRunning(GameStateTypes state)
        {
            return Utilities.EnumIsInBitmaskEnum((int)state, (int)CurrentGameState);
        }

    } 
}
