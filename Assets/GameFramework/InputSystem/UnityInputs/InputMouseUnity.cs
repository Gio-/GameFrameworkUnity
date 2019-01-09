using UnityEngine;

namespace GameFramework
{
    [System.Serializable]
    public class InputMouseUnity : InputMouse
    {
        #region METHODS
        public override bool GetMouseButton(int button)
        {
            return CanExecute() && (Input.GetMouseButton(button));
        }

        public override bool GetMouseButtonDown(int button)
        {
            return CanExecute() && (Input.GetMouseButtonDown(button));
        }

        public override bool GetMouseButtonUp(int button)
        {
            return CanExecute() && (Input.GetMouseButtonUp(button));
        }

        public override Vector3 GetMousePosition()
        {

            return CanExecute()? Input.mousePosition : Vector3.zero;
        }

        public override Vector2 GetMouseScrollDelta()
        {
            return CanExecute() ? Input.mouseScrollDelta : Vector2.zero;
        }

        public override bool IsmousePresent()
        {
            bool ret = Input.mousePresent;
            if (!ret)
                Debug.LogWarning("WARNING: Trying to get mouse info but mouse is not connected");
            return Input.mousePresent;
        }

        public override bool CanExecute()
        {
            return base.CanExecute() && IsmousePresent();
        }
        #endregion
    }
}