using System;
using UnityEngine;

namespace GameFramework
{
    public abstract class PlayerInput : MonoBehaviour
    {

        #region VARIABLES
        [SerializeField] protected GameStateTypes m_globalGameStatesAllowed;
        [SerializeField] protected bool isMultiPlayer = false;
        [ShowIf("isMultiPlayer",true)]
        [SerializeField]
        protected int playerID = 0;
        [SerializeField] protected bool m_isEnabled = true;
        [SerializeField] protected bool m_haveControl = true;

        #region INPUT VARIABLES
        protected BaseInput[] axisList;
        protected BaseInput[] buttonsList;
        protected InputMouse inputMouse;
        #endregion
        #endregion

        #region PROPERTIES
        public bool IsEnabled
        {
            get { return m_isEnabled; }
        }
        public bool HaveControl
        {
            get { return m_haveControl; }
        }
        public int PlayerId
        {
            get { return (isMultiPlayer ? playerID : -1); }
        }
        #endregion

        #region METHODS
        #region GET BUTTON/AXIS VALUES 
        public abstract bool GetMouseButton(int mouseButton);
        public abstract bool GetMouseButtonDown(int mouseButton);
        public abstract bool GetMouseButtonUp(int mouseButton);
        public abstract Vector3 GetMousePosition();
        public abstract Vector2 GetMouseScrollDelta();

        public abstract float GetAxis(string axisName);
        public abstract float GetAxisRaw(string axisName);

        public abstract bool GetButtonDown(string buttonName);
        public abstract bool GetButtonUp(string buttonName);
        public abstract bool GetButtonHeld(string buttonName);

        protected abstract InputAxis FindAxis(string axisName);
        protected abstract InputButton FindButton(string buttonName);

        

        #endregion
        #region GAIN CONTROL 
        public virtual void GainControl()
        {
            m_haveControl = true;
            foreach (InputButton bt in buttonsList)
            {
                GainControl(bt);
            }
            foreach (InputAxis ax in axisList)
            {
                GainControl(ax);
            }
        }
        protected virtual void GainControl(InputButton inputButton)
        {
            inputButton.GainControl();
        }
        protected virtual void GainControl(InputAxis inputAxis)
        {
            inputAxis.GainControl();
        }
        #endregion
        #region RELEASE CONTROL 
        public virtual void ReleaseControl()
        {
            m_haveControl = false;
            foreach (InputButton bt in buttonsList)
            {
                ReleaseControl(bt);
            }
            foreach (InputAxis ax in axisList)
            {
                ReleaseControl(ax);
            }
        }
        protected virtual void ReleaseControl(InputButton inputButton)
        {
            inputButton.ReleaseControl();
        }
        protected virtual void ReleaseControl(InputAxis inputAxis)
        {
            inputAxis.ReleaseControl();
        }
        #endregion
        protected virtual bool CanExecute()
        {
            return IsEnabled && HaveControl && GameState.CurrentGameState.Equals(m_globalGameStatesAllowed);
        }
        #endregion
    }
}

