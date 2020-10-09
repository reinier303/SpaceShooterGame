using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
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

        [Header("SecondPhaseVariables")]
        public float TimeBetweenShotsMultiplier;
        public int BulletAmountSecondPhase;
        public float ShotSpreadMultiplier;
        public float SpawnTimeMultiplier;
        public int DashAmountSecondPhase;

        private bool secondPhase;

        protected override void Start()
        {
            base.Start();
            AddMoves();

            MoveToNextStateRoundRobin();

            gameManager.BossAlive = true;
        }

        private void AddMoves()
        {
            moves.Add(new SpawnMinion(gameObject, Player, objectPooler, this, MoveToNextStateRoundRobin, PostMoveTime, SpawnTime, SpawnAmount));
            moves.Add(new BossShoot(gameObject, Player, objectPooler, this, MoveToNextStateRoundRobin, PostMoveTime, TimeBetweenShots, ShotSpread, TimesToShoot, BulletAmount));
            moves.Add(new Dash(gameObject, Player, objectPooler, this, MoveToNextStateRoundRobin, PostMoveTime, DashHaltTime, DashDuration, ChargeUpTime, DashMovementMultiplier, DashAmount));
        }

        protected override void Die()
        {
            base.Die();
            gameManager.BossAlive = false;
            gameManager.RWaveManager.NextWave();
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
                if (Random.Range(0, 3.5f) <= 2.5f)
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

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            if (currentHealth <= MaxHealth.GetValue() / 2 && !secondPhase)
            {
                SecondPhase();
                secondPhase = true;
            }
        }

        private void SecondPhase()
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 0.65f, 0.65f);
            TimeBetweenShots *= 0.8f;
            BulletAmount = 7;
            ShotSpread *= 1.2f;
            SpawnTime *= 0.8f;
            DashAmount = 3;
            moves.Clear();
            AddMoves();
        }
    }
}