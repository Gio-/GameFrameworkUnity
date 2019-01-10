using UnityEngine;
//TODO USE STRING BUILDER TO CONCAT STRINGS 
namespace GameFramework
{
    [System.Serializable]
    public class InputAxisUnity :InputAxis
    {
        #region VARIABLES
        protected System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
        #endregion
        #region CONSTRUCTOR
        public InputAxisUnity(string inputName):base(inputName)
        {
            this.inputName = inputName;
        }
        #endregion
        #region METHODS
        public override float GetAxis(int playerId)
        {
            if (!CanExecute())
                return 0;
            else
            {
                return playerId > 0 ? Input.GetAxis(strBuilder.Clear().Append(inputName).Append("_").Append(playerId).ToString()) : Input.GetAxis(inputName);
            }
        }
        public override float GetAxisRaw(int playerId)
        {
            if (!CanExecute())
                return 0;
            else
                return playerId > 0 ? Input.GetAxisRaw(strBuilder.Clear().Append(inputName).Append("_").Append(playerId).ToString()) : Input.GetAxisRaw(inputName);
        }
        
        #endregion
    }
}