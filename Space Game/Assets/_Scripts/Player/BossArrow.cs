using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArrow : MonoBehaviour
{
    private GameManager gameManager;
    public Transform Target;
    private Transform player;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        player = gameManager.RPlayer.transform;
    }
    void FixedUpdate()
    {
        //float angle = Vector3.Angle(transform.parent.position, gameManager.transform.position);
        //transform.RotateAround(transform.parent.position, transform.forward, angle);
        var dir = Target.position - player.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        transform.position = player.transform.position + transform.up * 2;
    }
}
