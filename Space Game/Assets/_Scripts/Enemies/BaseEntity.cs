using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    public Stat MaxHealth;
    public float currentHealth;

    public string HitSound, DeathSound;

    public GameObject ParticleEffect;

    public System.Action<float> OnTakeDamage;

    protected virtual void Awake()
    {
        OnTakeDamage += TakeDamage;
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
