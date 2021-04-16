using SpaceGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceGame
{
    public class EggSac : EnemyProjectile
    {
        public int spawnAmount;
        ObjectPooler objectPooler;

        protected virtual void Awake()
        {
            objectPooler = ObjectPooler.Instance;
        }

        protected override void OnEnable()
        {
            //This is meant to be empty
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerEntity player = collision.GetComponent<PlayerEntity>();

            if (player != null)
            {
                player.OnTakeDamage?.Invoke(Damage);
                SpawnParticleEffect();
                Spawn();
                gameObject.SetActive(false);
            }
        }

        public override IEnumerator DisableAfterTime()
        {
            yield return new WaitForSeconds(AliveTime);
            SpawnParticleEffect();
            Spawn();
            gameObject.SetActive(false);
        }

        protected virtual void Spawn()
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                Vector2 randomPosition = transform.position + new Vector3(Random.Range(-spawnAmount / 5, spawnAmount / 5), Random.Range(-spawnAmount / 5, spawnAmount / 5));
                objectPooler.SpawnFromPool("SwarmQueenMinion", randomPosition, Quaternion.identity);
            }
        }

        protected virtual void SpawnParticleEffect()
        {
            objectPooler.SpawnFromPool("SwarmExplosionBase", transform.position, Quaternion.identity);
        }
    }
}
