using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class SpawnMinion : BaseState
    {
        protected float postMoveWaitTime, timeBetweenSpawns = 1;
        protected int spawns = 1;


        public SpawnMinion(GameObject myGameObject, Transform playerTransform, ObjectPooler objectPoolerRef, BaseBoss bossScriptRef, System.Action nextMoveTypeRef, float postMoveTime, float spawnTime, int spawnAmount) : base(myGameObject, playerTransform, objectPoolerRef, bossScriptRef, nextMoveTypeRef)
        {
            postMoveWaitTime = postMoveTime;
            timeBetweenSpawns = spawnTime;
            spawns = spawnAmount;
        }

        public override void PerformMove()
        {
            bossScript.StartCoroutine(PerformMoveIEnumerator());
        }

        private IEnumerator PerformMoveIEnumerator()
        {
            for (int i = 0; i < spawns; i++)
            {
                objectPooler.SpawnFromPool("SwarmQueenMinion", bossGameObject.transform.position + bossGameObject.transform.up * 3, bossGameObject.transform.rotation);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
            yield return new WaitForSeconds(postMoveWaitTime);
            nextMoveType.Invoke();
        }
    }
}