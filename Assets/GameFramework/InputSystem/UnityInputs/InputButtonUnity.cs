using UnityEngine;
//TODO USE STRING BUILDER TO CONCAT STRINGS 
namespace GameFramework
{
    [System.Serializable]
    public class InputButtonUnity : InputButton
    {
        #region VARIABLES
        protected System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
        #endregion
        #region PROPERTIES
        #endregion
        #region CONSTRUCTOR
        public InputButtonUnity(string inputName) : base(inputName){}
        #endregion
        #region METHODS

        public override bool GetButtonDown(int playerId)
        {
            return CanExecute() && (playerId > 0 ? Input.GetButtonDown(strBuilder.Clear().Append(inputName).Append("_").Append(playerId).ToString()) : Input.GetButtonDown(inputName));
        }

        public override bool GetButtonHeld(int playerId)
        {
            return CanExecute() && (playerId > 0 ? Input.GetButton(strBuilder.Clear().Append(inputName).Append("_").Append(playerId).ToString()) : Input.GetButton(inputName));
        }

        public override bool GetButtonUp(int playerId)
        {
            return CanExecute() && (playerId > 0 ? Input.GetButtonUp(strBuilder.Clear().Append(inputName).Append("_").Append(playerId).ToString()) : Input.GetButtonUp(inputName));
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
