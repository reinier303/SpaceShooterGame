using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class ButtonState
    {
        public bool value;
        public float holdTime = 0;
    }

    public enum Directions
    {
        Right = 1,
        Left = -1
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public class InputState : MonoBehaviour
    {
        public Directions direction = Directions.Right;
        public float absVelX = 0f;
        public float absVelY = 0f;

        private Rigidbody2D body;
        private Dictionary<Buttons, ButtonState> buttonStates = new Dictionary<Buttons, ButtonState>();

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            absVelX = Mathf.Abs(body.velocity.x);
            absVelY = Mathf.Abs(body.velocity.y);
        }

        public void SetButtonValue(Buttons key, bool value)
        {
            if (!buttonStates.ContainsKey(key))
                buttonStates.Add(key, new ButtonState());

            var state = buttonStates[key];

            if (state.value && !value)
            {
                state.holdTime = 0;
            }
            else if (state.value && value)
            {
                state.holdTime += Time.deltaTime;
            }

            state.value = value;
        }

        public bool GetButtonValue(Buttons key)
        {
            if (buttonStates.ContainsKey(key))
                return buttonStates[key].value;
            else
                return false;
        }

        public float GetButtonHoldTime(Buttons key)
        {
            if (buttonStates.ContainsKey(key))
                return buttonStates[key].holdTime;
            else
                return 0;
        }
    }
}
