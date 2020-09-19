using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    [Header("Stats")]
    public Stat MaxHealth;
    public float currentHealth;
    public Stat DroppedUnits;

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
    public float PermanencePartOutwardsPower;
    public float PermanenceScaleFactor;

    public System.Action<float> OnTakeDamage;

    protected ObjectPooler objectPooler;
    protected CameraManager cameraManager;

    protected Material onHitMaterial;
    protected Material baseMaterial;

    protected SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        OnTakeDamage += TakeDamage;

        onHitMaterial = (Material)Resources.Load("Materials/FlashWhite", typeof(Material));
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseMaterial = spriteRenderer.material;
    }

    protected virtual void Start()
    {
        objectPooler = ObjectPooler.Instance;
        cameraManager = GameManager.Instance.RCameraManager;
    }

    protected void OnEnable()
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
        DropUnits(DroppedUnits.GetValue());
        cameraManager.StartCoroutine(cameraManager.Shake(ShakeDuration, ShakeMagnitude));
        gameObject.SetActive(false);
    }

    protected void SpawnParticleEffect()
    {
        GameObject effect = objectPooler.SpawnFromPool(ParticleEffect, transform.position, Quaternion.identity);
        effect.transform.localScale *= ParticleEffectScale;
    }

    protected void SpawnSegments()
    {
        for (int i = 0; i < Random.Range(3, 6); i++)
        {
            GameObject permanencePart = objectPooler.SpawnFromPool("PermanencePart", transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            permanencePart.transform.localScale = transform.localScale;
            permanencePart.GetComponent<PermanencePart>().InitializePart(PermanenceSprites, PermanencePartOutwardsPower, PermanenceScaleFactor);
        }
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
