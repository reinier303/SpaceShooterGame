using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceGame
{
    public class ShootEggSac : BaseState
    {
        protected float postMoveWaitTime, timeBetweenShots, spread = 1;
        protected float aliveTime, speed;
        protected int sacs, shots, spawnAmount = 1;

        public ShootEggSac(GameObject myGameObject, Transform playerTransform, ObjectPooler objectPoolerRef, BaseBoss bossScriptRef, System.Action nextMoveTypeRef, 
            float postMoveTime, float timeBetweenShotsGiven, float bulletSpread, int shotsAmount, int sacAmount, float sacAliveTime, int sacSpawnAmount, float sacSpeed) : 
            base(myGameObject, playerTransform, objectPoolerRef, bossScriptRef, nextMoveTypeRef)
        {
            postMoveWaitTime = postMoveTime;
            timeBetweenShots = timeBetweenShotsGiven;
            shots = shotsAmount;
            sacs = sacAmount;
            spread = bulletSpread;
            aliveTime = sacAliveTime;
            speed = sacSpeed;
            spawnAmount = sacSpawnAmount;
        }

        public override void PerformMove()
        {
            bossScript.StartCoroutine(PerformMoveIEnumerator());
        }

        private IEnumerator PerformMoveIEnumerator()
        {
            float angleIncrease;

            //Make sure there are no divisions by 0 in case of single projectile
            if (sacs == 1)
            {
                angleIncrease = 0;
                spread = 0;
            }
            else
            {
                angleIncrease = spread / (sacs - 1);
            }


            for (int i = 0; i < shots; i++)
            {
                for (int j = 0; j < sacs; j++)
                {
                    //Calculate new rotation
                    float newRotation = (bossGameObject.transform.eulerAngles.z - angleIncrease * j) + (spread / 2);

                    //Spawn Projectile with extra rotation based on projectile count
                    GameObject projectileObject = objectPooler.SpawnFromPool("EggSac", bossGameObject.transform.position + (bossGameObject.transform.up * 3),
                    Quaternion.Euler(bossGameObject.transform.eulerAngles.x, bossGameObject.transform.eulerAngles.y, newRotation));
                    EggSac eggSac = projectileObject.GetComponent<EggSac>();
                    eggSac.AliveTime = aliveTime;
                    eggSac.StartCoroutine(eggSac.DisableAfterTime());
                    eggSac.ProjectileSpeed = speed;
                    eggSac.spawnAmount = spawnAmount;
                }
                //objectPooler.SpawnFromPool("SwarmBullet", bossGameObject.transform.position + bossGameObject.transform.up * 3, bossGameObject.transform.rotation);
                yield return new WaitForSeconds(timeBetweenShots);
            }
            yield return new WaitForSeconds(postMoveWaitTime);
            nextMoveType.Invoke();
        }

    }
}
