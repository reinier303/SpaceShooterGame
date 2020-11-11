using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

namespace SpaceGame
{
    public class PlayerAttack : MonoBehaviour
    {
        //TEMP: Gamemanager delegates 
        public ObjectPooler RObjectPooler;
        private Player rPlayer;

        private bool canFire;
        public float fireCooldown;

        public List<Weapon> Weapons;

        [SerializeField] private Weapon currentWeapon;

        [SerializeField] private GameObject muzzleFlash;

        private void Awake()
        {
            rPlayer = GetComponent<Player>();
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            canFire = true;
            currentWeapon = rPlayer.StartWeapon;
            currentWeapon.GetWeaponData(rPlayer.Data, 0);
            rPlayer.SetCurrentWeapon(currentWeapon);
            currentWeapon.InitializeUI();
        }

        public Weapon GetCurrentWeapon()
        {
            return currentWeapon;
        }

        // Update is called once per frame
        private void Update()
        {
            if(Time.timeScale == 0)
            {
                return;
            }
            //TEMP: Get input from inputManager
            if (Input.GetMouseButton(0))
            {
                Fire();
            }
        }

        private void Fire()
        {
            if (canFire)
            {
                muzzleFlash.SetActive(false);
                muzzleFlash.SetActive(true);
                Debug.Log(currentWeapon.GetType());
                currentWeapon.Fire(RObjectPooler, transform);

                canFire = false;
                StartCoroutine(FireCooldownTimer());
            }
        }

        private IEnumerator FireCooldownTimer()
        {
            yield return new WaitForSeconds(1 / currentWeapon.RWeaponData.Modules["FireRate"].GetStatValue());
            canFire = true;
        }
    }
}