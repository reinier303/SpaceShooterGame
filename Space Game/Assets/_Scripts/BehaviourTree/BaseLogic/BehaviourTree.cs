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

        node = new Sequence(
            
                new ConditionNode(blackBoard, new Func<bool>(() => !CheckBossHealth(50)),new Selector(
                    new ConditionNode(blackBoard, new Func<bool>(() => !CheckPlayerDistance(12)), new Selector(
                        new MultiShot(blackBoard),
                        new SprayShot(blackBoard))),
                    new ConditionNode(blackBoard, new Func<bool>(() => CheckPlayerDistance(12)), new Sequence(
                        new DashNode(blackBoard),
                        new BulletBurst(blackBoard))))),
                new ConditionNode(blackBoard, new Func<bool>(() => CheckBossHealth(50)), new Selector(
                    new ConditionNode(blackBoard, new Func<bool>(() => !CheckPlayerDistance(12)), new Selector(
                        new SprayShot(blackBoard),
                        new SpawnMinions(blackBoard))),
                    new ConditionNode(blackBoard, new Func<bool>(() => CheckPlayerDistance(12)), new Sequence(
                        new DashNode(blackBoard),
                        new DashNode(blackBoard),
                        new DashNode(blackBoard),
                        new BulletBurst(blackBoard))))));
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
