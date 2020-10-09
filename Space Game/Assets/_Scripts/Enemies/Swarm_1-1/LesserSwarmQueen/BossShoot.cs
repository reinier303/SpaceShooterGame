using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class BossShoot : BaseState
    {
        protected float postMoveWaitTime, timeBetweenShots, spread = 1;
        protected int shots, bullets = 1;

        public BossShoot(GameObject myGameObject, Transform playerTransform, ObjectPooler objectPoolerRef, BaseBoss bossScriptRef, System.Action nextMoveTypeRef, float postMoveTime, float timeBetweenShotsGiven, float bulletSpread, int shotsAmount, int bulletAmount) : base(myGameObject, playerTransform, objectPoolerRef, bossScriptRef, nextMoveTypeRef)
        {
            postMoveWaitTime = postMoveTime;
            timeBetweenShots = timeBetweenShotsGiven;
            shots = shotsAmount;
            bullets = bulletAmount;
            spread = bulletSpread;
        }

        public override void PerformMove()
        {
            bossScript.StartCoroutine(PerformMoveIEnumerator());
        }

        private IEnumerator PerformMoveIEnumerator()
        {
            float angleIncrease;

            //Make sure there are no divisions by 0 in case of single projectile
            if (bullets == 1)
            {
                angleIncrease = 0;
                spread = 0;
            }
            else
            {
                angleIncrease = spread / (bullets - 1);
            }


            for (int i = 0; i < shots; i++)
            {
                for (int j = 0; j < bullets; j++)
                {
                    //Calculate new rotation
                    float newRotation = (bossGameObject.transform.eulerAngles.z - angleIncrease * j) + (spread / 2);

                    //Spawn Projectile with extra rotation based on projectile count
                    GameObject projectileObject = objectPooler.SpawnFromPool("SwarmBullet", bossGameObject.transform.position + (bossGameObject.transform.up * 3),
                    Quaternion.Euler(bossGameObject.transform.eulerAngles.x, bossGameObject.transform.eulerAngles.y, newRotation));
                }
                //objectPooler.SpawnFromPool("SwarmBullet", bossGameObject.transform.position + bossGameObject.transform.up * 3, bossGameObject.transform.rotation);
                yield return new WaitForSeconds(timeBetweenShots);
            }
            yield return new WaitForSeconds(postMoveWaitTime);
            nextMoveType.Invoke();
        }
    }
}