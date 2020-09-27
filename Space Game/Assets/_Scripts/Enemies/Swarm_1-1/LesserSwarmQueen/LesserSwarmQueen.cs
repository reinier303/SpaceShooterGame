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
    }
}
