using System;
using UnityEngine;

namespace GameFramework
{
    public class PlayerInputUnity : PlayerInput
    {

        #region VARIABLES
        [SerializeField]
        protected new InputAxisUnity[] axisList = { new InputAxisUnity("Horizontal") , new InputAxisUnity("Vertical") };
        [SerializeField]
        protected new InputButtonUnity[] buttonsList = { new InputButtonUnity("Jump"), new InputButtonUnity("Fire")};
        [SerializeField]
        protected new InputMouseUnity inputMouse = new InputMouseUnity();
        #endregion

        #region METHODS
        public override bool GetMouseButton(int mouseButton)
        {
            return (CanExecute() || inputMouse.allowedStateOverride != 0) ? inputMouse.GetMouseButton(mouseButton) : false;
        }
        public override bool GetMouseButtonDown(int mouseButton) {
            return (CanExecute() || inputMouse.allowedStateOverride != 0) ? inputMouse.GetMouseButtonDown(mouseButton) : false;
        }
        public override bool GetMouseButtonUp(int mouseButton) {
            return (CanExecute() || inputMouse.allowedStateOverride != 0) ? inputMouse.GetMouseButtonUp(mouseButton) : false;
        }
        public override Vector3 GetMousePosition() {
            return (CanExecute() || inputMouse.allowedStateOverride != 0)? inputMouse.GetMousePosition():Vector3.zero;
        }
        public override Vector2 GetMouseScrollDelta() {
            return (CanExecute() || inputMouse.allowedStateOverride != 0) ? inputMouse.GetMouseScrollDelta() : Vector2.zero;
        }


        public override float GetAxis(string axisName)
        {
            float axisVal = 0;
            InputAxis axis = FindAxis(axisName);
            if (axis != null && IsEnabled && HaveControl)
            {
                //IF AXIS HAS A EXECUTION GAMESTATE OVERRIDE I WILL CHECK FOR THAT STATE
                if ((axis.allowedStateOverride != 0)) 
                        axisVal = axis.GetAxis(PlayerId);
                //ELSE I CHECK IF CAN BE EXECUTED BY THIS CLASS (MEANS THAT IT WILL CHECK GAMESTATES ALLOWED FROM THIS CLASS)
                else if (CanExecute())
                    axisVal = axis.GetAxis(PlayerId);

            }
            return axisVal;
        }
        public override float GetAxisRaw(string axisName)
        {
            float axisVal = 0;
            InputAxis axis = FindAxis(axisName);
            if (axis != null)
            {
                if (CanExecute() || axis.allowedStateOverride != 0)
                    axisVal = axis.GetAxisRaw(PlayerId);

            }

            return axisVal;
        }

        public override bool GetButtonDown(string buttonName)
        {
            bool isDown = false;
            InputButton button = FindButton(buttonName);

            if (button != null)
            {
                if (CanExecute() || button.allowedStateOverride != 0)
                    isDown = button.GetButtonDown(PlayerId);
            }

            return isDown;
        }
        public override bool GetButtonUp(string buttonName)
        {
            bool isDown = false;
            InputButton button = FindButton(buttonName);
            if (button != null)
            {
                if (CanExecute() || button.allowedStateOverride != 0)
                    isDown = button.GetButtonUp(PlayerId);
            }

            return isDown;
        }
        public override bool GetButtonHeld(string buttonName)
        {
            bool isDown = false;
            InputButton button = FindButton(buttonName);
            if (button != null)
            {
                if (CanExecute() || button.allowedStateOverride != 0)
                    isDown = button.GetButtonHeld(PlayerId);
            }

            return isDown;
        }

        protected override InputAxis FindAxis(string axisName)
        {
            foreach (InputAxis ax in axisList)
            {
                if (ax.InputName == axisName)
                    return ax;
            }
            return null;
        }
        protected override InputButton FindButton(string buttonName)
        {
            foreach (InputButton bt in buttonsList)
            {
                if (bt.InputName == buttonName)
                    return bt;
            }
            return null;
        }
        #endregion
    }
}

