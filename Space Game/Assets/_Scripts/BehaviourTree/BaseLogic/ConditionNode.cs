﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class ConditionNode : Node
    {
        private Node[] inputNodes;
        private Func<bool> condition;

        private BlackBoard blackBoard;

        public ConditionNode(BlackBoard bb, Func<bool> _condition, params Node[] _inputNodes)
        {
            this.blackBoard = bb;
            this.condition = _condition;
            this.inputNodes = _inputNodes;
        }

        public override NodeStates Evaluate()
        {
            if (!condition())
            {
                return NodeStates.FAILURE;
            }
            foreach (Node node in inputNodes)
            {
                NodeStates result = node.Evaluate();

                switch (result)
                {
                    case NodeStates.FAILURE:
                        return NodeStates.FAILURE;
                    case NodeStates.RUNNING:
                        return NodeStates.RUNNING;
                    case NodeStates.SUCCESS:
                        //only at succes it moves on to the next task
                        break;
                    default:
                        break;
                }
            }

            return NodeStates.FAILURE;
        }
    }
}