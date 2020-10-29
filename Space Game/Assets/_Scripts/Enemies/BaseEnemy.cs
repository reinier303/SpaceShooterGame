using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;
namespace SpaceGame
{
    public class BaseEnemy : BaseEntity
    {
        public Transform Player;

        //Stats
        public Stat ContactDamage;
        public Stat Speed;
        public Stat RotationSpeed;
        public Stat ExpGiven;
        public Stat DroppedUnits;

        [Tooltip("Distance to player at which the enemy will stop moving")]
        public float StopDistance;

        protected override void Start()
        {
            base.Start();
            Player = GameManager.Instance.RPlayer.transform;
            InitializeStats();
            //InitializeStatsWithReflection();
        }

        protected void InitializeStats()
        {
            ContactDamage.statName = nameof(ContactDamage);
            Speed.statName = nameof(Speed);
            DroppedUnits.statName = nameof(DroppedUnits);
            ExpGiven.statName = nameof(ExpGiven);
        }

        protected void InitializeStatsWithReflection()
        {
            var fields = GetType().GetFields();
            foreach (var field in fields)
            {
                if (field.FieldType == typeof(Stat))
                {
                    Stat newStat = new Stat("newStat", (float)field.GetValue("baseValue"), (float)field.GetValue("multiplier"));
                    Debug.Log(newStat.GetValue());
                    field.SetValue(field, newStat);
                }
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerEntity player = collision.GetComponent<PlayerEntity>();

            if (player != null)
            {
                player.OnTakeDamage?.Invoke(ContactDamage.GetValue());
                player.lastEnemyDamagedBy = spriteRenderer.sprite;
            }
        }

        protected virtual void Move()
        {
            if (Vector2.Distance(transform.position, Player.transform.position) > StopDistance && gameManager.PlayerAlive)
            {
                //transform.position = Vector2.MoveTowards(transform.position, Player.position, Speed.GetValue() * Time.deltaTime);
                transform.position += transform.up * Speed.GetValue() * Time.deltaTime;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.position, -Speed.GetValue() * Time.deltaTime);
            }
        }

        protected virtual void Rotate()
        {
            Vector3 difference = Player.position - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            Quaternion desiredRotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90f);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, RotationSpeed.GetValue() * Time.deltaTime);
            if (!gameManager.PlayerAlive)
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 90f);
            }
        }

        protected override void Die()
        {
            Player.GetComponent<Player>().AddExperience(ExpGiven.GetValue());
            UpdateRunData();
            DropUnits(DroppedUnits.GetValue());
            base.Die();
        }
        protected virtual void UpdateRunData()
        {
            gameManager.AddEnemyKilled();
            gameManager.ExperienceEarned += ExpGiven.GetValue();
        }

        protected virtual void DropUnits(float units)
        {
            int unitAmount50 = (int)((units - units % 50) / 50);
            int unitAmount5 = (int)(((units - units % 5) / 5) - (unitAmount50 * 10));
            int unitAmount05 = (int)((units / 0.5f) - (unitAmount50 * 100) - (unitAmount5 * 10));

            for (int i = 0; i < unitAmount50; i++)
            {
                GameObject unitObj = objectPooler.SpawnFromPool("Unit50", transform.position, Quaternion.identity);
                Unit unit = unitObj.GetComponent<Unit>();
                unit.MoveUnit();
            }
            for (int i = 0; i < unitAmount5; i++)
            {
                GameObject unitObj = objectPooler.SpawnFromPool("Unit5", transform.position, Quaternion.identity);
                Unit unit = unitObj.GetComponent<Unit>();
                unit.MoveUnit();
            }
            for (int i = 0; i < unitAmount05; i++)
            {
                GameObject unitObj = objectPooler.SpawnFromPool("Unit0.5", transform.position, Quaternion.identity);
                Unit unit = unitObj.GetComponent<Unit>();
                unit.MoveUnit();
            }
        }
    }
}