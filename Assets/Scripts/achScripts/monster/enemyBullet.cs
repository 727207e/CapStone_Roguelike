using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public int damage = 0;
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 1000.0f);
        Destroy(gameObject, 5.0f);
    }

    
    void Update()
    {
        //GetComponent<Rigidbody>().AddForce(transform.forward * 1000.0f);

    }
}
