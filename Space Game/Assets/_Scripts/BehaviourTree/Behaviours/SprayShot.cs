using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class SprayShot : Node
    {
        private int timesShot;

        private float timer, timeBetween;

        public SprayShot(BlackBoard board)
        {
            blackBoard = board;
        }

        public override NodeStates Evaluate()
        {
            Debug.Log("SprayShot");
            if (timesShot >= blackBoard.TimesToShootSpray)
            {
                timesShot = 0;
                return NodeStates.FAILURE;
            }
            else
            {
                timer++;
                if (timer >= blackBoard.TimeBetweenShotsSpray * 60)
                {
                    Fire();
                    timer = 0;
                }
                return NodeStates.RUNNING;
            }
        }

        public void Fire()
        {
            float spread = blackBoard.BulletSpreadSpray;
            float angleIncrease = spread / (blackBoard.BulletAmountSpray - 1);

            for (int i = 0; i < blackBoard.BulletAmountSpray; i++)
            {
                //Calculate new rotation
                float newRotation = (blackBoard.BossEntity.transform.eulerAngles.z - angleIncrease * i) + (spread / 2);

                //Spawn Projectile with extra rotation based on projectile count
                GameObject projectileObject = blackBoard.objectPooler.SpawnFromPool("SwarmBullet", blackBoard.BossEntity.transform.position + (blackBoard.BossEntity.transform.up / 3),
                Quaternion.Euler(blackBoard.BossEntity.transform.eulerAngles.x, blackBoard.BossEntity.transform.eulerAngles.y, newRotation));
            }
            timesShot++;
        }
    }
}