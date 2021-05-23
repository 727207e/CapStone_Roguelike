using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinMagnet : MonoBehaviour
{
    public GameObject player;
    public float dis;
    private float distance = 5.0f;
    private float acceleration = 0.1f;
    private float velocity = 0.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        dis = Vector3.Distance(player.transform.position, transform.position);
        Vector3 dir = (player.transform.position - transform.position).normalized;
        velocity = (velocity + acceleration * Time.deltaTime);

        if (dis <= distance)
        {
            transform.position = new Vector3(transform.position.x + (dir.x * velocity), transform.position.y + (dir.y * velocity));
        }
        else
            velocity = 0.0f;
    }
}
