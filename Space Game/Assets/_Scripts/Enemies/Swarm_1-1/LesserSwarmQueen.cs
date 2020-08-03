using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserSwarmQueen : BaseEnemy
{
    private BlackBoard blackBoard;

    protected override void Start()
    {
        base.Start();
        blackBoard = GetComponent<BlackBoard>();
        StartCoroutine(Spawn());
    }

    protected void Update()
    {
        Move();
        Rotate();
    }

    private IEnumerator Spawn()
    {
        blackBoard.objectPooler.SpawnFromPool("SwarmQueenMinion", blackBoard.BossEntity.transform.position + blackBoard.BossEntity.transform.up * 2.5f, blackBoard.BossEntity.transform.rotation);
        yield return new WaitForSeconds(blackBoard.SpawnTime);
        StartCoroutine(Spawn());
    }
}
