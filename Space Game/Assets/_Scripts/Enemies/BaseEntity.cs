using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceGame
{
    public class BaseEntity : MonoBehaviour
    {
        [Header("Stats")]
        public Stat MaxHealth;
        public float currentHealth;

        [Header("Screen Shake")]
        public float ShakeMagnitude;
        public float ShakeDuration;

        [Header("Audio")]
        public string HitSound;
        public string DeathSound;

        [Header("Particle Effect")]
        public string ParticleEffect;
        public float ParticleEffectScale;

        [Header("Permanence Parts")]
        public List<Sprite> PermanenceSprites;
        public Vector2 PermanencePartMinMaxAmount;
        public float PermanencePartOutwardsPower;
        public float PermanenceScaleFactor;

        public System.Action<float> OnTakeDamage;

        protected GameManager gameManager;
        protected ObjectPooler objectPooler;
        protected CameraManager cameraManager;

        protected Material onHitMaterial;
        protected Material baseMaterial;

        protected SpriteRenderer spriteRenderer;

        [HideInInspector] public bool isDead;
        [HideInInspector] public bool isInitialized;

        public int KilledByIndex;

        protected virtual void Awake()
        {
            OnTakeDamage += TakeDamage;

            onHitMaterial = (Material)Resources.Load("Materials/FlashWhite", typeof(Material));
            spriteRenderer = GetComponent<SpriteRenderer>();
            baseMaterial = spriteRenderer.material;
            gameManager = GameManager.Instance;
            objectPooler = ObjectPooler.Instance;
        }

        protected virtual void Start()
        {
            cameraManager = GameManager.Instance.RCameraManager;
        }

        protected virtual void OnEnable()
        {
            isDead = false;
            currentHealth = MaxHealth.GetValue();
            isInitialized = true;
        }

        public virtual void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                isDead = true;
                currentHealth = 0;
                Die();
            }
            else
            {
                StartCoroutine(FlashWhite());
            }
        }

        protected IEnumerator FlashWhite()
        {
            spriteRenderer.material = onHitMaterial;
            yield return new WaitForSeconds(0.03f);
            spriteRenderer.material = baseMaterial;
        }

        protected virtual void Die()
        {
            SpawnSegments();
            SpawnParticleEffect();
            cameraManager.StartCoroutine(cameraManager.Shake(ShakeDuration, ShakeMagnitude));
            gameObject.SetActive(false);
        }

        protected virtual void SpawnParticleEffect()
        {
            GameObject effect = objectPooler.SpawnFromPool(ParticleEffect, transform.position, Quaternion.identity);
            effect.transform.localScale *= ParticleEffectScale;
        }

        protected virtual void SpawnSegments()
        {
            for (int i = 0; i < Random.Range(PermanencePartMinMaxAmount.x, PermanencePartMinMaxAmount.y); i++)
            {
                GameObject permanencePart = objectPooler.SpawnFromPool("PermanencePart", transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                permanencePart.transform.localScale = transform.localScale;
                permanencePart.GetComponent<PermanencePart>().InitializePart(PermanenceSprites, PermanencePartOutwardsPower, PermanenceScaleFactor);
            }
        }
    }
}