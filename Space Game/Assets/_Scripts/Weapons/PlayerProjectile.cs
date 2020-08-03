using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public Dictionary<string, Module> Modules = new Dictionary<string, Module>();

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.position += transform.up * Time.deltaTime * Modules["ProjectileSpeed"].GetStatValue();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        BaseEntity entity = collider.GetComponent<BaseEntity>();
        if(entity != null)
        {
            entity.OnTakeDamage?.Invoke(Modules["Damage"].GetStatValue());
            gameObject.SetActive(false);
        }
    }

    public virtual IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(Modules["AliveTime"].GetStatValue());
        gameObject.SetActive(false);
    }
}
