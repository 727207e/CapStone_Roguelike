using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msAimTest : MonoBehaviour
{
    private Transform thisTransform;
    //private GameObject aimPoint;

    private Vector3 MousePosition; //마우스의 위치
    public Vector3 aimDirection;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        MousePosition = Input.mousePosition;
        //Debug.Log("x : " + MousePosition.x);
        //Debug.Log("y : " + MousePosition.y);
        //Debug.Log("z : " + MousePosition.z);
        aimDirection = new Vector3(MousePosition.x/170, MousePosition.y/50, 0);
        Debug.Log(aimDirection);
        thisTransform.LookAt(aimDirection);
    }
}
