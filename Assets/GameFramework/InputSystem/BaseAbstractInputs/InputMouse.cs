using UnityEngine;

namespace GameFramework
{
    public abstract class InputMouse : BaseInput
    {
        #region METHODS
        public abstract bool IsmousePresent();
        public abstract bool GetMouseButton(int button);
        public abstract bool GetMouseButtonDown(int button);
        public abstract bool GetMouseButtonUp(int button);
        public abstract Vector3 GetMousePosition();
        public abstract Vector2 GetMouseScrollDelta();
        #endregion
    }
}