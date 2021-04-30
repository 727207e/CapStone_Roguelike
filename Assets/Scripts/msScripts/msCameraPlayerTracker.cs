using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msCameraPlayerTracker : MonoBehaviour
{
    public float cameraSpeed = 5.0f;

    public GameObject player;

    void Update()
    {
        if (player == true)
        {
            Vector3 dir = player.transform.position - transform.position;
            Vector3 moveVector = new Vector3(dir.x * cameraSpeed * Time.deltaTime, (dir.y + 2.5f) * cameraSpeed * Time.deltaTime, 0.0f);
            transform.Translate(moveVector);
        }
        
    }
}