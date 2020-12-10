using SpaceGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    [CreateAssetMenu(menuName = "WeaponSystem/Weapons/BulletWeapon", order = 997)]
    public class BulletWeapon : Weapon
    {

        public override void Fire(ObjectPooler objectPooler, Transform player)
        {
            float spread = RWeaponData.Modules["ProjectileSpread"].GetStatValue();
            float count = (int)RWeaponData.Modules["ProjectileCount"].GetStatValue();

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
                float newRotation = (player.eulerAngles.z - angleIncrease * i) + (spread / 2);

                //Spawn Projectile with extra rotation based on projectile count
                GameObject projectileObject = objectPooler.SpawnFromPool(ProjectileName, player.position + (player.up / 3),
                Quaternion.Euler(player.eulerAngles.x, player.eulerAngles.y, newRotation));

                //Initialize projectile
                PlayerProjectile projectile = projectileObject.GetComponent<PlayerProjectile>();
                projectile.Modules = RWeaponData.Modules;
                projectile.StartCoroutine(projectile.DisableAfterTime());
            }

        }
    }
}
