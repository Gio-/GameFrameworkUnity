using UnityEngine;

namespace GameFramework
{
    public abstract class InputButton : BaseInput
    {
        #region VARIABLES
        [SerializeField] protected string inputName = "Fire";
        #endregion

        #region PROPERTIES
        public string InputName
        {
            get { return inputName; }
        }
        #endregion

        #region CONSTRUCTOR
        public InputButton(string inputName)
        {
            this.inputName = inputName;
        }
        #endregion

        #region METHODS
        public abstract bool GetButtonDown(int playerId);
        public abstract bool GetButtonHeld(int playerId);
        public abstract bool GetButtonUp(int playerId);
        #endregion
    }
}
