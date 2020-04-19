using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarmling : BaseEnemy
{
    //TEMP - TESTING ONLY: GET REFERENCE FROM GAMEMANAGER OR ELSE LATER
    public Transform Player;

    public Stat Speed;

    [Tooltip("Distance to player at which the enemy will stop moving")]
    public float StopDistance;

    private void Start()
    {
        Player = GameManager.Instance.RPlayer.transform;
    }

    protected void Update()
    {
        Move();
        Rotate();
    }

    protected void Move()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) > StopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, Speed.GetValue() * Time.deltaTime);
        }
    }

    protected void Rotate()
    {
        Vector3 difference = Player.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90f);
    }
}
