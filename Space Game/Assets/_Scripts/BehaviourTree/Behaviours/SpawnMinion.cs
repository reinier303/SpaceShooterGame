using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMinion : Node
{
    private float timer;

    public SpawnMinion(BlackBoard board)
    {
        blackBoard = board;
    }

    public override NodeStates Evaluate()
    {
        timer++;
        if(timer > blackBoard.TimeBetweenShots * 60)
        {
            Spawn();
        }
        return NodeStates.RUNNING;
    }

    public void Spawn()
    {
        blackBoard.objectPooler.SpawnFromPool("SwarmQueenMinion", blackBoard.BossEntity.transform.position + blackBoard.BossEntity.transform.up * 2.5f, blackBoard.BossEntity.transform.rotation);
    }
}
