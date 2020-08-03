using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
    private Node node;
    private BlackBoard blackBoard;

    // Start is called before the first frame update
    private void Start()
    {
        //Get BlackBoard from Transform
        blackBoard = GetComponent<BlackBoard>();

        List<Node> nodeList = new List<Node>();

        List<Node> nodeListSequence = new List<Node>();

        //nodeList.Add(new ConditionNode(blackBoard, new Func<bool>(()=> CheckBossHealth(50)), nodeList));

        /*
        nodeListSequence.Add(new Dash(blackBoard));
        nodeListSequence.Add(new BulletBurst(blackBoard));

        nodeList.Add(new Sequence(nodeListSequence));
        */

        nodeList.Add(new SpawnMinions(blackBoard));


        //Initialize behaviour tree with nodes
        node = new Selector(nodeList);
    }

    // Update is called once per frame
    private void Update()
    {
        node.Evaluate();
    }

    private bool CheckPlayerDistance(float distance)
    {
        if(Vector3.Distance(blackBoard.Player.position, transform.position) <= distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckBossHealth(float percentage)
    {
        if (blackBoard.BossEntity.currentHealth < percentage)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
