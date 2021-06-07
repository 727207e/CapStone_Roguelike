using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapflowfield : MonoBehaviour
{
    // 스테 13 전용
    private Vector3 startposi;
    private Vector3 endposi;
    public float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        startposi = new Vector3(-10.0f, 28.0f, 0.0f);
        endposi = new Vector3(-10.0f, -25.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // 발판 이동
        transform.position = Vector3.MoveTowards(transform.position, endposi, speed * Time.deltaTime);
        // 끝지점 도착하면 시작지점으로 이동
        if (transform.position == endposi)
            transform.position = startposi;
    }
}
