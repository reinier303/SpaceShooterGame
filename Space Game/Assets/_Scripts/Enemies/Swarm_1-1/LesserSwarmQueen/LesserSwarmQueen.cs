using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserSwarmQueen : BaseBoss
{
    [Header("General Variables")]
    public float PostMoveTime;

    [Header("ShootVariables")]
    public float TimeBetweenShots = 0.5f;
    public float ShotSpread;
    public int TimesToShoot;
    public int BulletAmount;

    [Header("SpawnVariables")]
    public float SpawnTime;
    public int SpawnAmount;

    [Header("DashVariables")]
    public float DashHaltTime, DashDuration, ChargeUpTime, DashMovementMultiplier;
    public int DashAmount;

    protected override void Start()
    {
        base.Start();
        moves.Add(new SpawnMinion(gameObject, Player, objectPooler, this, MoveToNextStateRoundRobin, PostMoveTime, SpawnTime, SpawnAmount));
        moves.Add(new BossShoot(gameObject, Player, objectPooler, this, MoveToNextStateRoundRobin, PostMoveTime, TimeBetweenShots, ShotSpread, TimesToShoot, BulletAmount));
        moves.Add(new Dash(gameObject, Player, objectPooler, this, MoveToNextStateRoundRobin, PostMoveTime, DashHaltTime, DashDuration, ChargeUpTime, DashMovementMultiplier, DashAmount));

        MoveToNextStateRoundRobin();

        gameManager.BossAlive = true;
    }

    protected override void Die()
    {
        base.Die();
        gameManager.BossAlive = false;
    }

    protected override void SpawnParticleEffect()
    {
        gameManager.StartCoroutine(SpawnDeathEffect());
    }

    private IEnumerator SpawnDeathEffect()
    {
        for (int i = 0; i < 20; i++)
        {
            string explosion;
            if(Random.Range(0, 3.5f) <= 2.5f)
            {
                explosion = "SwarmBossExplosion";
            }
            else
            {
                explosion = "SwarmExplosionBase";
            }
            GameObject Explosion = objectPooler.SpawnFromPool(explosion,
            (Vector2)transform.position + new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f)),
            Quaternion.identity);
            cameraManager.StartCoroutine(cameraManager.Shake(ShakeDuration, ShakeMagnitude));
            Explosion.transform.localScale *= Random.Range(0.65f, 1.15f);
            yield return new WaitForSeconds(0.1f + Random.Range(-0.05f, 0.1f));
        }
        objectPooler.SpawnFromPool("SwarmBossEndExplosion", (Vector2)transform.position + new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)), Quaternion.identity);
        cameraManager.StartCoroutine(cameraManager.Shake(ShakeDuration * 1.5f, ShakeMagnitude * 1.5f));
        SpawnSegments();
    }
}
