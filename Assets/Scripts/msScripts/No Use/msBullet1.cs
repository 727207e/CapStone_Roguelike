using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msBullet1 : MonoBehaviour
{
    public float bulletSpeed = 100.0f;
    private Transform thisTransform;

    private Vector3 MousePosition; //마우스의 위치
    public Vector3 aimDirection; //캐릭터가 실제로 바라보는 방향. z는 항상 0이다.

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
        Debug.Log(thisTransform.position);
        GetComponent<Rigidbody>().AddForce(thisTransform.forward * bulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
