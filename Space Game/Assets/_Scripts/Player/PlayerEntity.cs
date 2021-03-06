﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class PlayerEntity : BaseEntity
    {
        [SerializeField] private GameObject PlayerHitEffect;
        private bool canHit = true;
        [SerializeField] private float hitCooldown;
        public Sprite lastEnemyDamagedBy;

        protected override void Die()
        {
            gameManager.PlayerAlive = false;
            gameManager.SetSummaryData();
            gameManager.RPlayer.AddUnitsToTotal();
            gameManager.RCameraManager.DeathCamera();
            gameManager.RUIManager.OnPlayerDeathUI();
            SpawnParticleEffect();
            cameraManager.StartCoroutine(cameraManager.Shake(ShakeDuration, ShakeMagnitude));
            gameManager.RPlayer.SavePlayerData();
            gameObject.SetActive(false);
        }

        public override void TakeDamage(float damage)
        {
            if (!canHit)
            {
                return;
            }
            PlayerHitEffect.SetActive(true);
            gameManager.RUIManager.StartCoroutine(gameManager.RUIManager.TweenAlpha(gameManager.RUIManager.HitVignette.rectTransform,
                gameManager.RUIManager.VignetteDuration, gameManager.RUIManager.AlphaTo, 0));
            if (!gameManager.PlayerAlive)
            {
                return;
            }
            StartCoroutine(HitCooldown());
            base.TakeDamage(damage);
        }

        protected override void SpawnParticleEffect()
        {
            gameManager.StartCoroutine(SpawnDeathEffect());
        }

        private IEnumerator SpawnDeathEffect()
        {
            for (int i = 0; i < 10; i++)
            {
                objectPooler.SpawnFromPool("PlayerExplosion",
                (Vector2)transform.position + new Vector2(Random.Range(-0.15f, 0.15f), Random.Range(-0.15f, 0.15f)),
                Quaternion.identity);
                cameraManager.StartCoroutine(cameraManager.Shake(ShakeDuration, ShakeMagnitude));
                yield return new WaitForSeconds(0.2f + Random.Range(-0.05f, 0.15f));
            }
            objectPooler.SpawnFromPool("PlayerEndExplosion", (Vector2)transform.position + new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)), Quaternion.identity);
            cameraManager.StartCoroutine(cameraManager.Shake(ShakeDuration, ShakeMagnitude * 1.5f));
            SpawnSegments();
        }

        private IEnumerator HitCooldown()
        {
            canHit = false;
            yield return new WaitForSeconds(hitCooldown);
            canHit = true;
        }
    }
}