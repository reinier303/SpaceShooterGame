﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class DashNode : Node
    {
        private float timer, cooldownTimer;

        private bool onCooldown;


        public DashNode(BlackBoard board)
        {
            blackBoard = board;
        }

        public override NodeStates Evaluate()
        {
            if (onCooldown)
            {
                cooldownTimer++;
                if (cooldownTimer > blackBoard.Cooldown)
                {
                    return NodeStates.RUNNING;
                }
            }

            if (blackBoard.PlayerHit)
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
}