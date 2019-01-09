using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;

public class GameManager : MonoBehaviour
{
    #region Privates
    [Header("Game Mode Definition")]
    [SerializeField]
    /// Current GameMode
    private GameMode        m_gameMode;           
    /// Current GameMode State                   
    private GameModeState   m_currentGameModeState;  
    /// Variable to Check if Game Mode is active and enabled 
    /// to update.               
    private bool            m_gameModeEnabled;

    [Header("Game State Definition")]                    
    [SerializeField]
    /// Defining which state is designated to be the "Game Is Running" 
    /// state.
    private GameStateTypes  m_runningGameState = GameStateTypes.Running;
    #endregion

    #region Events
    public delegate void GameModeEndsHandler(GameModeState endType);
    public event GameModeEndsHandler OnGameModeEndsEvent;
    #endregion

    #region Methods
    /// <summary>
    /// Change the current GameMode 
    /// </summary>
    /// <param name="newGameMode">New GameMode to set</param>
    public void ChangeGameMode(GameMode newGameMode)
    {
        if(m_gameMode != null)
            m_gameMode.Disable();

        m_gameMode              = Instantiate(newGameMode);
        m_gameModeEnabled       = true;
        m_currentGameModeState  = GameModeState.Idle;
    }

    /// <summary>
    /// Restart current GameMode
    /// </summary>
    public void RestartGameMode()
    {
        if(m_gameMode == null)
            return;

        m_gameMode.Enable();
        m_gameModeEnabled       = true;
        m_currentGameModeState  = GameModeState.Idle;
    }

    /// <summary>
    /// Update GameModes and analyze the status.
    /// </summary>
    private void UpdateGameMode()
    {
        /// if there's a gameMode and it is enabled    
        /// it can start analyze its status.
        if(m_gameMode != null && m_gameModeEnabled)
        {
            /// if current recorded status is Idle, then check
            /// again to update. Otherwise, disable the gameMode 
            /// marking it as disabled and throw an event.     
            if(m_currentGameModeState == GameModeState.Idle)
            {
                m_currentGameModeState = m_gameMode.CheckGameModeCondition();
            }
            else
            {
                m_gameModeEnabled = false;

                if(OnGameModeEndsEvent != null)
                    OnGameModeEndsEvent(m_currentGameModeState);
            }
        }
    }
    #endregion

    #region MonoBehviour
    public void Awake()
    {        
        /// Check if there is a GameMode, if not print a
        /// Warning otherwise create a copy the GameMode.
        if(m_gameMode == null)
        {
            Debug.LogWarning("[GameManager](Awake): No GameMode for this game");
        }
        else
        {
            m_gameMode = Instantiate(m_gameMode);
        }
    }

    public void OnEnable()
    {
        /// Start the GameMode
        RestartGameMode();
    }

    public void OnDisable()
    {
        /// Stop the GameMode
        if(m_gameMode != null)
        {
            m_gameModeEnabled = false;
            m_gameMode.Disable();
        } 
    }

    public void Update()
    {      
        if(!GameState.IsStateRunning(m_runningGameState))
            return;

        /// Update GameMode Status
       UpdateGameMode();
    }
    #endregion
}
