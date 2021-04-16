using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class GreaterSwarmQueen : BaseBoss
    {
        [Header("General Variables")]
        public float PostMoveTime;

        [Header("ShootEggSacVariables")]
        public float TimeBetweenShots = 0.5f;
        public float ShotSpread = 360;
        public int TimesToShoot = 1;
        public int BulletAmount = 4;
        public float SacAliveTime;
        public float SacSpeed;
        public int SacSpawnAmount;

        protected override void Start()
        {
            base.Start();
            AddMoves();

            MoveToNextStateRoundRobin();

            gameManager.BossAlive = true;
        }

        private void AddMoves()
        {
            moves.Add(new ShootEggSac(gameObject, Player, objectPooler, this, MoveToNextStateRoundRobin, 
            PostMoveTime, TimeBetweenShots, ShotSpread, TimesToShoot, BulletAmount, SacAliveTime, SacSpawnAmount, SacSpeed));
        }
    }
}
