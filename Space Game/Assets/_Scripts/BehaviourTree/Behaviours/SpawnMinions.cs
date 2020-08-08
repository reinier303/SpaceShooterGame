using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMinions : Node
{
    private int timesSpawned;

    private float timer;

    public SpawnMinions(BlackBoard board)
    {
        blackBoard = board;
    }

    public override NodeStates Evaluate()
    {
        if(timesSpawned >= blackBoard.SpawnAmount)
        {
            //Remove when in behaviour tree/testing only
            blackBoard.SpawnAmount = 0;

            return NodeStates.FAILURE;
        }
        else
        {
            timer++;
            if(timer >= blackBoard.TimeBetweenSpawns * 60)
            {
                Spawn();
                timer = 0;
                timesSpawned++;
            }
            return NodeStates.RUNNING;
        }
    }

    private void Spawn()
    {
        blackBoard.objectPooler.SpawnFromPool("SwarmQueenMinion", blackBoard.BossEntity.transform.position + blackBoard.BossEntity.transform.up * 2.5f, blackBoard.BossEntity.transform.rotation);
    }
}
