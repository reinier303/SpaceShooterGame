using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShot : Node
{
    private int timesShot;

    private float timer, timeBetween;


    public MultiShot(BlackBoard board)
    {
        blackBoard = board;
    }

    public override NodeStates Evaluate()
    {
        Debug.Log("MultiShot");
        if(timesShot >= blackBoard.TimesToShoot)
        {
            //timesShot = 0;
            return NodeStates.FAILURE;
        }
        else
        {
            timer++;
            if(timer >= blackBoard.TimeBetweenShots * 60)
            {
                Fire();
                timer = 0;
            }
            return NodeStates.RUNNING;
        }
    }

    public void Fire()
    {
        float spread = blackBoard.BulletSpread;
        float angleIncrease = spread / (blackBoard.BulletAmount - 1);

        for (int i = 0; i < blackBoard.BulletAmount; i++)
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
