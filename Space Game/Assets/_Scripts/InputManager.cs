using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class InputManager : MonoBehaviour
    {
        private GameManager gameManager;
        [Header("General")]
        public KeyCode PauseButton;
        public KeyCode PauseButton2;

        [Header("Movement")]
        public KeyCode BoostButton;
        public KeyCode BoostButton2;
        public KeyCode UpButton;
        public KeyCode UpButton2;
        public KeyCode DownButton;
        public KeyCode DownButton2;
        public KeyCode LeftButton;
        public KeyCode LeftButton2;
        public KeyCode RightButton;
        public KeyCode RightButton2;

        private float axisX;
        private float axisY;

        private bool boosting;
        private float boost;

        private void Start()
        {
            gameManager = GameManager.Instance;
            boost = 1;
        }

        private void Update()
        {
            PauseInput();
        }

        private void PauseInput()
        {
            if (Input.GetKeyDown(PauseButton) || Input.GetKeyDown(PauseButton2))
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


        public float GetAxisSmoothHorizontal(float speed, float acceleration, float deceleration, float boostSpeed)
        {
            boost = Boost(boostSpeed);

            //Right button input
            if (Input.GetKey(RightButton) || Input.GetKey(RightButton2))
            {
                axisX += acceleration * boost * Time.deltaTime;
                if (axisX > speed * boost)
                {
                    axisX = speed * boost;
                }
            }
            else if(axisX > 0)
            {
                axisX -= deceleration * boost * Time.deltaTime;
            }

            //Left button input
            if (Input.GetKey(LeftButton) || Input.GetKey(LeftButton2))
            {
                axisX -= acceleration * boost * Time.deltaTime;
                if (axisX < -speed * boost)
                {
                    axisX = -speed * boost;
                }
            }
            else if (axisX < 0)
            {
                axisX += deceleration * boost * Time.deltaTime;
            }
            return axisX;
        }


        public float GetAxisSmoothVertical(float speed, float acceleration, float deceleration, float boostSpeed)
        {
            boost = Boost(boostSpeed);

            //Right button input
            if (Input.GetKey(UpButton) || Input.GetKey(UpButton2))
            {
                axisY += acceleration * boost * Time.deltaTime;
                if (axisY > speed * boost)
                {
                    axisY = speed * boost;
                }
            }
            else if (axisY > 0)
            {
                axisY -= deceleration * boost * Time.deltaTime;
            }

            //Left button input
            if (Input.GetKey(DownButton) || Input.GetKey(DownButton2))
            {
                axisY -= acceleration * boost * Time.deltaTime;
                if (axisY < -speed * boost)
                {
                    axisY = -speed * boost;
                }
            }
            else if (axisY < 0)
            {
                axisY += deceleration * boost * Time.deltaTime;
            }

            return axisY;
        }

        public float Boost(float boostSpeed)
        {
            if(boost <= boostSpeed && Input.GetKey(BoostButton) || Input.GetKey(BoostButton2))
            {
                boosting = true;
            }
            else
            {
                boosting = false;
            }
            if(boosting)
            {
                boost += boostSpeed * Time.deltaTime / 10;
            }
            else if (boost > 1)
            {
                boost -= boostSpeed * Time.deltaTime / 10;

            }
            return boost;
        }
    }
}

