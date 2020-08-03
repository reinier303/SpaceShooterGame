using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float ProjectileSpeed;

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.position += transform.up * Time.deltaTime * ProjectileSpeed;
    }
}
