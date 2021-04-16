using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class EnemyProjectile : MonoBehaviour
    {
        public float ProjectileSpeed;

        //TODO:Get Damage from enemy that shoots as Stat
        public float Damage;
        public float AliveTime;

        public Sprite FiredBy;

        protected virtual void OnEnable()
        {
            StartCoroutine(DisableAfterTime());
        }

        protected virtual void Update()
        {
            Move();
        }

        protected virtual void Move()
        {
            transform.position += transform.up * Time.deltaTime * ProjectileSpeed;
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerEntity player = collision.GetComponent<PlayerEntity>();

            if (player != null)
            {
                player.OnTakeDamage?.Invoke(Damage);
                gameObject.SetActive(false);
            }
        }

        public virtual IEnumerator DisableAfterTime()
        {
            yield return new WaitForSeconds(AliveTime);
            gameObject.SetActive(false);
        }
    }
}