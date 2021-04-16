using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class PlayerProjectile : MonoBehaviour
    {
        public Dictionary<string, ModuleData> Modules = new Dictionary<string, ModuleData>();
        public string OnHitEffectName;
        protected ObjectPooler objectPooler;
        protected GameManager gameManager;
        [HideInInspector] public int WeaponIndex;

        protected virtual void Awake()
        {
            objectPooler = ObjectPooler.Instance;
            gameManager = GameManager.Instance;
        }

        protected virtual void Update()
        {
            Move();
        }

        public virtual void Initialize()
        {
            //This method is meant to be overridden.
        }

        protected virtual void Move()
        {
            transform.position += transform.up * Time.deltaTime * Modules["ProjectileSpeed"].GetStatValue();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            BaseEntity entity = collider.GetComponent<BaseEntity>();
            if (entity != null && !entity.isDead)
            {
                //gameManager.StartCoroutine(gameManager.Sleep(0.001f));
                entity.KilledByIndex = WeaponIndex;
                entity.OnTakeDamage?.Invoke(Modules["Damage"].GetStatValue());
                objectPooler.SpawnFromPool(OnHitEffectName, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }

        public virtual IEnumerator DisableAfterTime()
        {
            yield return new WaitForSeconds(Modules["AliveTime"].GetStatValue());
            objectPooler.SpawnFromPool(OnHitEffectName, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}