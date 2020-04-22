using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShooter
{
    public enum Buttons
    {
        W,
        A,
        D,
        S,
        LeftShift,
        LeftMouse,
    }

    public enum Condition
    {
        GreaterThan,
        LessThan
    }

    [System.Serializable]
    public class InputAxisState
    {
        public string axisName;
        public float offValue;
        public Buttons button;
        public Condition condition;

        public bool value
        {
            get
            {
                var val = Input.GetAxis(axisName);
                switch (condition)
                {
                    case Condition.GreaterThan:
                        return val > offValue;
                    case Condition.LessThan:
                        return val < offValue;
                }
                return false;
            }
        }
    }

    public class InputManager : MonoBehaviour
    {
        public InputAxisState[] inputs;
        public InputState inputState;

        // Update is called once per frame
        void Update()
        {
            foreach (var input in inputs)
            {
                inputState.SetButtonValue(input.button, input.value);
            }
        }
    }
}
