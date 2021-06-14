﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarController : MonoBehaviour
{

    private Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
    }
}
