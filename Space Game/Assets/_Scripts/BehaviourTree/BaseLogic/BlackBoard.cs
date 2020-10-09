using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
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

        //SprayShot
        [Header("SprayShot Variables")]
        public float TimesToShootSpray;
        public float BulletAmountSpray;
        public float BulletSpreadSpray;
        public float TimeBetweenShotsSpray;

        //BulletBurst
        [Header("BulletBurst Variables")]
        public float TimesToShootBurst;
        public float BulletAmountBurst;
        public float BulletSpreadBurst;
        public float TimeBetweenShotsBurst;

        //Dash
        [Header("Dash Variables")]
        public float DashDamage;
        public float DashSpeed;
        public float DashSteps;
        public float Cooldown;
        public bool PlayerHit;

        //Spawn
        [Header("Spawn Variables")]
        public float SpawnTime;
        public float TimeBetweenSpawns;
        public float SpawnAmount;


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
}