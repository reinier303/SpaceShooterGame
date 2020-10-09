using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceGame
{
    public class EggCarrier : BaseEnemy
    {
        public Stat SpawnAmount;
        public float DeathTime;
        private Collider2D collider;

        protected virtual void Update()
        {
            Move();
            Rotate();
        }

        protected override void Start()
        {
            base.Start();
            collider = GetComponent<Collider2D>();
        }

        protected override void Die()
        {
            StartCoroutine(WaitThenDie(base.Die));
        }

        protected virtual IEnumerator WaitThenDie(System.Action Die)
        {
            collider.enabled = false;
            spriteRenderer.color = new Color(1, 0.7f, 0.7f, 1);
            yield return new WaitForSeconds(DeathTime);
            Spawn();
            Die.Invoke();
        }

        protected virtual void Spawn()
        {
            for (int i = 0; i < (int)SpawnAmount.GetValue(); i++)
            {
                Vector2 RandomPosition = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
                objectPooler.SpawnFromPool("SwarmQueenMinion", transform.position + (Vector3)RandomPosition, Quaternion.identity);
            }
        }
    }
}