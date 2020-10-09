using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class Unit : MonoBehaviour
    {
        //Variables
        public float Value;
        public float DisableTime;

        //Components
        private Transform player;

        //Script References
        private Player playerScript;
        private ObjectPooler objectPooler;

        protected virtual void Awake()
        {
            playerScript = GameManager.Instance.RPlayer;
            player = playerScript.transform;
            objectPooler = ObjectPooler.Instance;
        }

        protected virtual void OnEnable()
        {
            StartCoroutine(DisableAfterTime());
        }

        protected virtual void Update()
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 2f)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 1.5f);
            }
        }

        public void MoveUnit()
        {
            StartCoroutine(lerpPosition(transform.position, (Vector2)transform.position + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), 0.35f));
        }

        private IEnumerator lerpPosition(Vector2 StartPos, Vector2 EndPos, float LerpTime)
        {
            float StartTime = Time.time;
            float EndTime = StartTime + LerpTime;

            while (Time.time < EndTime)
            {
                float timeProgressed = (Time.time - StartTime) / LerpTime;  // this will be 0 at the beginning and 1 at the end.
                transform.position = Vector2.Lerp(StartPos, EndPos, timeProgressed);

                yield return new WaitForFixedUpdate();
            }

        }

        private IEnumerator DisableAfterTime()
        {
            yield return new WaitForSeconds(DisableTime + Random.Range(0, 1f));
            //TODO:Add fade
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                objectPooler.SpawnFromPool("CoinPickupEffect", transform.position, Quaternion.identity);
                playerScript.AddUnits(Value);
                gameObject.SetActive(false);
                //StartCoroutine(MoveToUnits());
            }
        }

        IEnumerator MoveToUnits()
        {
            //TODO:Visually move to Units UI
            yield return new WaitForSeconds(0.01f);
        }
    }
}