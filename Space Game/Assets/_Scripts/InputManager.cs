using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class InputManager : MonoBehaviour
    {
        private GameManager gameManager;

        public KeyCode BoostButton;
        public KeyCode PauseButton;
        public KeyCode UpButton;
        public KeyCode DownButton;
        public KeyCode LeftButton;
        public KeyCode RightButton;

        private void Start()
        {
            gameManager = GameManager.Instance;
        }

        private void Update()
        {
            PauseInput();
        }

        private void PauseInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale == 1)
                {
                    gameManager.PauseGame();
                }
                else
                {
                    gameManager.UnpauseGame();
                }
            }
        }

        public float GetAxisSmoothHorizontal(float acceleration, float deceleration)
        {
            float axis = 0;

            //Right button input
            if(Input.GetKey(RightButton))
            {
                axis = Mathf.Clamp01(axis + acceleration * Time.deltaTime);
            }
            else
            {
                axis = Mathf.Clamp01(axis - deceleration * Time.deltaTime);
            }

            //Left button input
            if (Input.GetKey(LeftButton))
            {
                axis = (Mathf.Clamp01(axis + acceleration * Time.deltaTime)) * -1;
            }
            else
            {
                axis = (Mathf.Clamp01(axis - deceleration * Time.deltaTime)) * -1;
            }
            return axis;
        }
}
}

