using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;

public class BaseEnemy : BaseEntity
{
    public Transform Player;

    //Stats
    public Stat ContactDamage;
    public Stat Speed;
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
        foreach(var field in fields)
        {
            if(field.FieldType == typeof(Stat))
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
        }
    }

    protected virtual void Move()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) > StopDistance && gameManager.PlayerAlive)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, Speed.GetValue() * Time.deltaTime);
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
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90f);
        if (!gameManager.PlayerAlive)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 90f);
        }
    }

    protected override void Die()
    {
        Player.GetComponent<Player>().AddExperience(ExpGiven.GetValue());
        DropUnits(DroppedUnits.GetValue());
        base.Die();
    }

    protected virtual void DropUnits(float units)
    {
        for (int i = 0; i * 2 < units; i++)
        {
            GameObject unitObj = objectPooler.SpawnFromPool("Unit0.5", transform.position, Quaternion.identity);
            Unit unit = unitObj.GetComponent<Unit>();
            unit.MoveUnit();
        }
    }

}
