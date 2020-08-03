using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBurst : Node
{
    private int timesShot;

    private float timer, timeBetween;


    public BulletBurst(BlackBoard board)
    {
        blackBoard = board;
    }

    public override NodeStates Evaluate()
    {
        if(timesShot >= blackBoard.TimesToShootBurst)
        {
            return NodeStates.SUCCESS;
        }
        else
        {
            timer++;
            if(timer >= blackBoard.TimeBetweenShotsBurst * 60)
            {
                Fire();
                timer = 0;
            }
            return NodeStates.RUNNING;
        }
    }

    public void Fire()
    {
        float spread = blackBoard.BulletSpreadBurst;
        float angleIncrease = spread / (blackBoard.BulletAmountBurst - 1);

        for (int i = 0; i < blackBoard.BulletAmountBurst; i++)
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
