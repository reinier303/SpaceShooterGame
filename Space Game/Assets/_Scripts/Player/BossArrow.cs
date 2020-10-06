using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArrow : MonoBehaviour
{
    private GameManager gameManager;
    public Transform Target;
    private Transform player;
    [HideInInspector] public SpriteRenderer bossIcon;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        player = gameManager.RPlayer.transform;
        bossIcon = GetComponentInChildren<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        var dir = Target.position - player.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        transform.position = player.transform.position + transform.up * 2;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnBecameVisible()
    {
        gameObject.SetActive(true);
    }
}
