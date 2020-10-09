using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

                float spread = currentWeapon.RWeaponData.Modules["ProjectileSpread"].GetStatValue();
                float count = (int)currentWeapon.RWeaponData.Modules["ProjectileCount"].GetStatValue();

                float angleIncrease;

                //Make sure there are no divisions by 0 in case of single projectile
                if (count == 1)
                {
                    angleIncrease = 0;
                    spread = 0;
                }
                else
                {
                    angleIncrease = spread / (count - 1);
                }

                for (int i = 0; i < count; i++)
                {
                    //Calculate new rotation
                    float newRotation = (transform.eulerAngles.z - angleIncrease * i) + (spread / 2);

                    //Spawn Projectile with extra rotation based on projectile count
                    GameObject projectileObject = RObjectPooler.SpawnFromPool(currentWeapon.ProjectileName, transform.position + (transform.up / 3),
                    Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newRotation));

                    //Initialize projectile
                    PlayerProjectile projectile = projectileObject.GetComponent<PlayerProjectile>();
                    projectile.Modules = currentWeapon.RWeaponData.Modules;
                    projectile.StartCoroutine(projectile.DisableAfterTime());
                }

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