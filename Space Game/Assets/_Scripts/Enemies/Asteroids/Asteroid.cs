using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class Asteroid : BaseEnemy
    {
        public Vector2 MinMaxTorque;
        public Vector2 MinMaxForce;
        public Vector2 MinMaxSize;

        private Rigidbody2D rigidbody;

        private Vector2 maxXandY;

        protected override void Awake()
        {
            base.Awake();
            rigidbody = GetComponent<Rigidbody2D>();
        }

        protected override void Start()
        {
            base.Start();
            maxXandY = new Vector2(gameManager.RWaveManager.MapRenderer.size.x + 3, gameManager.RWaveManager.MapRenderer.size.y + 3);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            AddForces();
            RandomSize();
            transform.localScale = RandomSize();
        }

        protected virtual void Update()
        {
            if (transform.position.x > maxXandY.x ||
               transform.position.y > maxXandY.y ||
               transform.position.x < -maxXandY.x ||
               transform.position.y < -maxXandY.y)
            {
                gameObject.SetActive(false);
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerEntity player = collision.GetComponent<PlayerEntity>();

            if (player != null)
            {
                player.OnTakeDamage?.Invoke(ContactDamage.GetValue());
                Die();
            }
        }

        protected virtual void AddForces()
        {
            Vector2 force = new Vector2(Random.Range(MinMaxForce.x, MinMaxForce.y), Random.Range(MinMaxForce.x, MinMaxForce.y));
            float torque = Random.Range(MinMaxTorque.x, MinMaxTorque.y);
            rigidbody.AddForce(force);
            rigidbody.AddTorque(torque);
        }

        protected virtual Vector2 RandomSize()
        {
            float x = Random.Range(MinMaxSize.x, MinMaxSize.y);
            float y = Random.Range(MinMaxSize.x, MinMaxSize.y);

            return new Vector2(x, y);
        }

        protected override void SpawnSegments()
        {
            for (int i = 0; i < Random.Range(PermanencePartMinMaxAmount.x, PermanencePartMinMaxAmount.y); i++)
            {
                GameObject permanencePart = objectPooler.SpawnFromPool("PermanencePart", transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                permanencePart.transform.localScale = RandomSize();
                permanencePart.GetComponent<PermanencePart>().InitializePart(PermanenceSprites, PermanencePartOutwardsPower, PermanenceScaleFactor * Random.Range(0.65f, 1.15f));
            }
        }

        protected override void SpawnParticleEffect()
        {
            GameObject effect = objectPooler.SpawnFromPool(ParticleEffect, transform.position, Quaternion.identity);
            float scaleFactor2 = (Mathf.Abs(transform.localScale.x - transform.localScale.y) / 2) + Mathf.Min(transform.localScale.x, transform.localScale.y);
            effect.transform.localScale *= ParticleEffectScale * scaleFactor2;
        }
    }
}