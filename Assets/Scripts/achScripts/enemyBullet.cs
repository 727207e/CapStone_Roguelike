using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 1000.0f);
    }

    
    void Update()
    {
        //Destroy(gameObject, 5.0f);
    }
}
