using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Node
{
    private int timesShot;

    private float timer, cooldownTimer;

    private bool onCooldown;


    public Dash(BlackBoard board)
    {
        blackBoard = board;
    }

    public override NodeStates Evaluate()
    {
        if(onCooldown)
        {
            cooldownTimer++;
            if(cooldownTimer > blackBoard.Cooldown)
            {
                return NodeStates.RUNNING;
            }
        }

        if(blackBoard.PlayerHit)
        {
            blackBoard.PlayerHit = false;
            return NodeStates.SUCCESS;
        }

        timer++;
        DashForward();
        if (timer >= blackBoard.DashSteps)
        {
            timer = 0;
            onCooldown = true;
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
