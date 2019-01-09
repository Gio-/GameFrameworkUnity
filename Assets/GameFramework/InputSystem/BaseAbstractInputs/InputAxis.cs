using UnityEngine;

namespace GameFramework
{
    public abstract class InputAxis : BaseInput
    {
        #region VARIABLES
        [SerializeField] protected string inputName = "Horizontal";
        #endregion
        #region PROPERTIES
        public string InputName
        {
            get { return inputName; }
        }
        #endregion

        #region CONSTRUCTOR
        public InputAxis(string inputName)
        {
            this.inputName = inputName;
        }
        #endregion
        #region METHODS
        public abstract float GetAxis(int playerId);
        public abstract float GetAxisRaw(int playerId);

        #endregion
    }
}