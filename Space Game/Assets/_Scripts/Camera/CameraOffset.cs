﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraOffset : MonoBehaviour
{
    public Camera cam;
    private CinemachineCameraOffset offset;
    public float OffsetIntensity = 0.1f;
    public float Damping = 0.1f;

    private void Start()
    {
        offset = GetComponent<CinemachineCameraOffset>();
    }
    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector2 target = hit.point;

        Vector2 OffsetTwo = ((hit.point - transform.position) / 2) * OffsetIntensity;
        Vector3 finalOffset = new Vector3(OffsetTwo.x, OffsetTwo.y, 0);

        Vector3 SmoothedOffset = Vector3.Lerp(offset.m_Offset, finalOffset, Damping);

        offset.m_Offset = SmoothedOffset;
    }
}