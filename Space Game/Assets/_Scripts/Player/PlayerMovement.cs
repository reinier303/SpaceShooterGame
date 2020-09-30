using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Speed Data
    public float BoostSpeed = 1.5f;
    public float baseSpeed;
    private float currentSpeed;

    private Vector3 target;
    private Camera cam;
    private bool Stay;

    private void Awake()
    {
        cam = Camera.main;
        Stay = false;
    }

    private void Start()
    {
        currentSpeed = baseSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        target = hit.point;

        if (Vector2.Distance(transform.position + transform.forward, target) > 3.5f)
        {
            Move();
            Rotate();
        }

        StayInPlace();
    }

    private void Move()
    {
        float step = currentSpeed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

    private void Rotate()
    {
        var dir = target - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void StayInPlace()
    {
        //Stay In Place
        if (Input.GetMouseButtonDown(1))
        {
            currentSpeed = 0;
        }
        if (Input.GetMouseButtonUp(1))
        {
            currentSpeed = baseSpeed;
        }
    }
}
