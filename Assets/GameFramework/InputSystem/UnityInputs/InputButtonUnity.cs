using UnityEngine;
//TODO USE STRING BUILDER TO CONCAT STRINGS 
namespace GameFramework
{
    [System.Serializable]
    public class InputButtonUnity : InputButton
    {
        #region VARIABLES
        #endregion
        #region PROPERTIES
        #endregion
        #region CONSTRUCTOR
        public InputButtonUnity(string inputName) : base(inputName){}
        #endregion
        #region METHODS

        public override bool GetButtonDown(int playerId)
        {
            return CanExecute() && (playerId > 0 ? Input.GetButtonDown(inputName + "_" + playerId) : Input.GetButtonDown(inputName));
        }

        public override bool GetButtonHeld(int playerId)
        {
            return CanExecute() && (playerId > 0 ? Input.GetButton(inputName + "_" + playerId) : Input.GetButton(inputName));
        }

        public override bool GetButtonUp(int playerId)
        {
            return CanExecute() && (playerId > 0 ? Input.GetButtonUp(inputName + "_" + playerId) : Input.GetButtonUp(inputName));
        }

        public override void GainControl()
        {
            base.GainControl();
        }
        public override void ReleaseControl()
        {
            base.GainControl();
        }

        /// <summary>
        /// CHECK IF EXECUTION CAN BE DONE
        /// </summary>
        /// <returns></returns>
        public override bool CanExecute()
        {
            return base.CanExecute();
        }
        #endregion
    }
}
