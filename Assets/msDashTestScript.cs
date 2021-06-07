using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msDashTestScript : MonoBehaviour
{
    private Rigidbody rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //StartCoroutine(RollingAnimationDelay());
            rbody.AddForce(new Vector3(1, 0, 0) * 30, ForceMode.VelocityChange);
        }
    }
}
