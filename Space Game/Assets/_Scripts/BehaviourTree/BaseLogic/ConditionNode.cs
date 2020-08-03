using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNode : Node
{
    private List<Node> inputNodes;
    private Func<bool> condition;

    private BlackBoard blackBoard;

    public ConditionNode(BlackBoard bb, Func<bool> _condition, List<Node> _inputNodes)
    {
        this.blackBoard = bb;
        this.condition = _condition;
        this.inputNodes = _inputNodes;
    }

    public override NodeStates Evaluate()
    {
        if (!condition())
        {
            return NodeStates.SUCCESS;
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
