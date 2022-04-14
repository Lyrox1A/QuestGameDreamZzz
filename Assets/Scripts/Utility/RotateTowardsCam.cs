using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsCam : MonoBehaviour
{

    private Camera Cam;

    private void Awake()
    {
        Cam = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(Cam.transform);
    }
}
