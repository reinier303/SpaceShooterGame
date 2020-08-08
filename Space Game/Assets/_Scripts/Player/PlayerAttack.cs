using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //TEMP: Gamemanager delegates 
    public ObjectPooler RObjectPooler;

    private bool canFire;
    public float fireCooldown;

    public List<Weapon> Weapons;

    private Weapon currentWeapon;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        canFire = true;
        currentWeapon = Weapons[0];
        InitializeWeapons();
    }

    //TEMP???: Other way to do this?
    private void InitializeWeapons()
    {
        foreach(Weapon weapon in Weapons)
        {
            weapon.AddBaseModules();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //TEMP: Get input from inputManager
        if(Input.GetMouseButton(0))
        {
            Fire();
        }
    }

    private void Fire()
    {
        if(canFire)
        {
            float spread = currentWeapon.Modules["ProjectileSpread"].GetStatValue();
            float count = (int)currentWeapon.Modules["ProjectileCount"].GetStatValue();

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
                projectile.Modules = currentWeapon.Modules;
                projectile.StartCoroutine(projectile.DisableAfterTime());
            }

            canFire = false;
            StartCoroutine(FireCooldownTimer());
        }
    }

    private IEnumerator FireCooldownTimer()
    {
        yield return new WaitForSeconds(1 / currentWeapon.Modules["FireRate"].GetStatValue());
        canFire = true;
    }
}
