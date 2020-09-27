using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserSwarmQueenBehaviourTree : BaseBoss
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

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerEntity player = collision.GetComponent<PlayerEntity>();

        if (player != null)
        {
            player.OnTakeDamage?.Invoke(ContactDamage.GetValue());
            HitPlayer();
        }
    }

    private IEnumerator Spawn()
    {
        blackBoard.objectPooler.SpawnFromPool("SwarmQueenMinion", blackBoard.BossEntity.transform.position + blackBoard.BossEntity.transform.up * 2.5f, blackBoard.BossEntity.transform.rotation);
        yield return new WaitForSeconds(blackBoard.SpawnTime);
        StartCoroutine(Spawn());
    }

    private void HitPlayer()
    {
        blackBoard.PlayerHit = true;
    }
}
