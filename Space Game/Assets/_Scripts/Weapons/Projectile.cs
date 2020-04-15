using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //TEMP: GET FROM PLAYER
    public Stat Speed;
    public Stat Damage;
    public Stat AliveTime;

    protected virtual void Awake()
    {
        //Get Player
    }

    protected void OnEnable()
    {
        StartCoroutine(DisableAfterTime());
    }

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.position += transform.up * Time.deltaTime * Speed.GetValue(); 
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        BaseEntity entity = collider.GetComponent<BaseEntity>();
        if(entity != null)
        {
            //TEMP: GET DAMAGE FROM PLAYER
            entity.TakeDamage(Damage.GetValue());
            gameObject.SetActive(false);
        }
    }

    protected virtual IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(AliveTime.GetValue());
        gameObject.SetActive(false);
    }
}
