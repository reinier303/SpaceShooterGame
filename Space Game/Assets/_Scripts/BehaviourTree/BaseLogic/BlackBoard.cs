using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard : MonoBehaviour
{
    //General
    [Header("General Variables")]
    public float Speed;
    public float RotationSpeed;


    //MultiShot
    [Header("MultiShot Variables")]
    public float TimesToShoot;
    public float BulletAmount;
    public float BulletSpread;
    public float BulletDamage;
    public float TimeBetweenShots;

    //Dash
    [Header("Dash Variables")]
    public float DashDamage;
    public float DashSpeed;

    //Spawn
    [Header("Spawn Variables")]
    public float SpawnTime;

    public Transform Player;
    [HideInInspector] public PlayerEntity PlayerEntity;
    [HideInInspector] public BaseEnemy BossEntity;
    [HideInInspector] public ObjectPooler objectPooler;

    private void Awake()
    {
        PlayerEntity = Player.GetComponent<PlayerEntity>();
        BossEntity = GetComponent<BaseEnemy>();
        objectPooler = ObjectPooler.Instance;
    }
}
