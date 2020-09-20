using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float ProjectileSpeed;

    //TODO:Get Damage from enemy that shoots as Stat
    public float Damage;

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
        }
    }
}
