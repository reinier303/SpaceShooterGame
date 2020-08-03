using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseEntity
{
    public Stat ContactDamage;

    //TEMP - TESTING ONLY: GET REFERENCE FROM GAMEMANAGER OR ELSE LATER
    public Transform Player;

    public Stat Speed;

    [Tooltip("Distance to player at which the enemy will stop moving")]
    public float StopDistance;

    protected virtual void Start()
    {
        Player = GameManager.Instance.RPlayer.transform;
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
        if (Vector2.Distance(transform.position, Player.transform.position) > StopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, Speed.GetValue() * Time.deltaTime);
        }
    }

    protected virtual void Rotate()
    {
        Vector3 difference = Player.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90f);
    }
}
