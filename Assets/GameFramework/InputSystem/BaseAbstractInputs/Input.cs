using UnityEngine;

namespace GameFramework
{
    
    public abstract class BaseInput 
    {
        #region VARIABLES
        public GameStateTypes allowedStateOverride;
        [SerializeField] protected bool m_isEnabled = true;
        protected bool m_haveControl = true;
        #endregion

        #region METHODS
        public virtual void GainControl()
        {
            m_haveControl = true;
        }
        public virtual void ReleaseControl()
        {
            m_haveControl = false;
        }
        public virtual bool CanExecute()
        {
            if (!m_isEnabled || !m_haveControl)
                return false;
            if (allowedStateOverride != 0)
                return GameState.CurrentGameState.Equals(allowedStateOverride);
            else
                return true;
        }
        #endregion
    }
}