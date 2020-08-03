using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Node
{
    private int timesShot;

    private float timer, timeBetween;


    public Dash(BlackBoard board)
    {
        blackBoard = board;
    }

    public override NodeStates Evaluate()
    {
        if(blackBoard.PlayerHit)
        {
            Debug.Log("hit");
            blackBoard.PlayerHit = false;
            return NodeStates.SUCCESS;
        }

        timer++;
        DashForward();
        if (timer >= blackBoard.DashSteps)
        {
            timer = 0;
            //Remove when put into behaviour tree
            blackBoard.DashSpeed = 0;
            return NodeStates.FAILURE;
        }
        else
        {
            return NodeStates.RUNNING;
        }

    }

    public void DashForward()
    {
        blackBoard.BossEntity.transform.position += blackBoard.BossEntity.transform.up * blackBoard.DashSpeed / 60;
    }
}
