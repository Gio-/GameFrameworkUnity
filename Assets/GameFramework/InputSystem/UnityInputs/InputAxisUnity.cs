using UnityEngine;
//TODO USE STRING BUILDER TO CONCAT STRINGS 
namespace GameFramework
{
    [System.Serializable]
    public class InputAxisUnity :InputAxis
    {
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
                return playerId > 0 ? Input.GetAxis(inputName + "_" + playerId) : Input.GetAxis(inputName);
            }
        }
        public override float GetAxisRaw(int playerId)
        {
            if (!CanExecute())
                return 0;
            else
                return playerId > 0 ? Input.GetAxisRaw(inputName + "_" + playerId) : Input.GetAxisRaw(inputName);
        }

        public void GainControl()
        {
            base.GainControl();
        }
        public void ReleaseControl()
        {
            base.ReleaseControl();
        }
        public bool CanExecute()
        {
            return base.CanExecute();
        }
        #endregion
    }
}