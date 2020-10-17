using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class BaseBoss : BaseEnemy
    {
        protected List<BaseState> moves = new List<BaseState>();
        protected List<BaseState> movesPerformed = new List<BaseState>();

        protected int currentMove = 0;
        public float SpawnRateReductionMultiplier;

        public bool dashing;

        public Sprite BossIcon;
        [TextArea(2,3)]
        public string BossEnterText;

        protected override void Start()
        {
            base.Start();
        }

        protected virtual void Update()
        {
            Move();
            Rotate();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ReduceSpawns();
        }

        protected override void UpdateRunData()
        {
            gameManager.BossesKilled++;
            gameManager.ExperienceEarned += ExpGiven.GetValue();
        }

        /// <summary>
        /// Create cool boss specific particle effect in here.
        /// </summary>
        protected override void SpawnParticleEffect()
        {
            //This method is meant to be overriden
        }

        /// <summary>
        /// Create cool boss specific segment spawn here.
        /// </summary>
        protected override void SpawnSegments()
        {
            //This method is meant to be overriden
        }

        protected virtual void ReduceSpawns()
        {
            gameManager.RWaveManager.AdjustToBoss(SpawnRateReductionMultiplier);
        }

        protected override void Move()
        {
            if (Vector2.Distance(transform.position, Player.transform.position) > StopDistance || dashing && gameManager.PlayerAlive)
            {
                //transform.position = Vector2.MoveTowards(transform.position, Player.position, Speed.GetValue() * Time.deltaTime);
                transform.position += transform.up * Speed.GetValue() * Time.deltaTime;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.position, -Speed.GetValue() * Time.deltaTime);
            }
        }


        protected virtual void OnBecameVisible()
        {
            gameManager.RWaveManager.BossArrowScript.gameObject.SetActive(false);
        }

        protected virtual void OnBecameInvisible()
        {
            gameManager.RWaveManager.BossArrowScript.gameObject.SetActive(true);
        }

        #region MoveToNextStateMethods

        public virtual void MoveToNextStateRandom()
        {
            BaseState nextState = moves[Random.Range(0, moves.Count)];
            nextState.PerformMove();
        }

        public virtual void MoveToNextStateRoundRobin()
        {
            Debug.Log(moves.Count + ", p:" + movesPerformed.Count);

            //If all moves have been performed refill the moves list
            if (moves.Count == 0)
            {
                moves.AddRange(movesPerformed);
                movesPerformed.Clear();
            }

            //select random move from the move list
            BaseState nextState = moves[Random.Range(0, moves.Count)];

            //remove the next state from moves and add it to moves performed to make sure all moves will be performed in a random order.
            moves.Remove(nextState);
            movesPerformed.Add(nextState);

            nextState.PerformMove();
        }

        public virtual void MoveToNextStateSequential()
        {
            BaseState nextState = moves[currentMove];

            if (currentMove < moves.Count)
            {
                currentMove++;
            }
            else
            {
                currentMove = 0;
            }

            nextState.PerformMove();
        }
        #endregion
    }
}