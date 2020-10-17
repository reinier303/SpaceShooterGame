using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class PlayerMovement : MonoBehaviour
    {

        [Header("Movement Variables")]
        public float BoostSpeed = 1.5f;
        public float BaseSpeed;
        public float Acceleration;
        public float Deceleration;

        private float currentSpeed;

        private Vector3 target;
        private Camera cam;
        private bool Stay;

        private GameManager gameManager;
        private Player player;
        private InputManager inputManager;

        public ParticleSystem EngineFire;

        private Dictionary<string, ModuleData> playerModules;

        private void Awake()
        {
            cam = Camera.main;
            Stay = false;
        }

        private void Start()
        {
            gameManager = GameManager.Instance;
            player = gameManager.RPlayer;
            inputManager = gameManager.RInputManager;
            playerModules = player.Data.PlayerModules;
            BaseSpeed = playerModules["MovementSpeed"].GetStatValue();
            currentSpeed = BaseSpeed;
        }

        // Update is called once per frame
        private void Update()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            float axisX = inputManager.GetAxisSmoothHorizontal(currentSpeed, Acceleration, Deceleration, BoostSpeed);
            float axisY = inputManager.GetAxisSmoothVertical(currentSpeed, Acceleration, Deceleration, BoostSpeed);

            Vector3 input = new Vector3(axisX, axisY, 0) * currentSpeed * Time.deltaTime;

            transform.position += input;
        }

        #region MouseMovement
        private void MoveToMouse()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            target = hit.point;

            float step = currentSpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, target, step);
        }

        private void StayInPlace()
        {
            //Stay In Place
            if (Input.GetMouseButtonDown(1))
            {
                currentSpeed = 0;
            }
            if (Input.GetMouseButtonUp(1))
            {
                currentSpeed = BaseSpeed;
            }
        }

        #endregion

        private void Rotate()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            target = hit.point;

            var dir = target - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }


    }
}