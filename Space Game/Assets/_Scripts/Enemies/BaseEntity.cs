using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    public Stat MaxHealth;
    public float currentHealth;

    public GameObject ParticleEffect;

    protected virtual void Awake()
    {
        currentHealth = MaxHealth.GetValue();
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
