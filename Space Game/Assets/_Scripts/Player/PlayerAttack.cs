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

        [SerializeField] private Weapon currentWeapon, secondaryWeapon;

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

            //Initialize weapon datas
            currentWeapon.GetWeaponData(rPlayer.Data, 0);
            secondaryWeapon.GetWeaponData(rPlayer.Data, 1);

            foreach (WeaponData data in rPlayer.Data.Weapons)
            {
                Debug.Log(data.WeaponName);
            }
            //Set weapon index
            currentWeapon.WeaponIndex = 0;
            secondaryWeapon.WeaponIndex = 1;

            //Add weapons to list
            Weapons.Add(currentWeapon);
            Weapons.Add(secondaryWeapon);

            rPlayer.SetWeapons(Weapons);
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
                Fire(0);
            }
            if(Input.GetMouseButton(1))
            {
                Fire(1);
            }
        }

        private void Fire(int mouseButton)
        {
            if (canFire)
            {
                muzzleFlash.SetActive(false);
                muzzleFlash.SetActive(true);

                if(mouseButton == 0)
                {
                    currentWeapon.Fire(RObjectPooler, transform);
                    canFire = false;
                    StartCoroutine(FireCooldownTimer(mouseButton));
                }
                else
                {
                    secondaryWeapon.Fire(RObjectPooler, transform);
                    canFire = false;
                    StartCoroutine(FireCooldownTimer(mouseButton));
                }
            }
        }

        private IEnumerator FireCooldownTimer(int mouseButton)
        {
            if(mouseButton == 0)
            {
                yield return new WaitForSeconds(1 / currentWeapon.RWeaponData.Modules["FireRate"].GetStatValue());
            }
            else
            {
                yield return new WaitForSeconds(1 / secondaryWeapon.RWeaponData.Modules["FireRate"].GetStatValue());
            }
            canFire = true;
        }
    }
}