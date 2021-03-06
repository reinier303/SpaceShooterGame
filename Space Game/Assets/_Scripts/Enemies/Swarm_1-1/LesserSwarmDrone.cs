﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class LesserSwarmDrone : BaseEnemy
    {
        public Stat FireRate;
        public Stat ShootDistance;
        private bool canFire;

        protected override void Awake()
        {
            base.Awake();
            canFire = true;
        }

        protected void Update()
        {
            Move();
            Rotate();
            Fire();
        }

        private void Fire()
        {
            if (canFire && gameManager.PlayerAlive && Vector2.Distance(Player.position, transform.position) <= ShootDistance.GetValue())
            {
                objectPooler.SpawnFromPool("SwarmBullet", transform.position + transform.up * 1.6f, transform.rotation);
                canFire = false;
                StartCoroutine(FireCooldownTimer());
            }
        }

        private IEnumerator FireCooldownTimer()
        {
            yield return new WaitForSeconds(1 / FireRate.GetValue());
            canFire = true;
        }
    }
}