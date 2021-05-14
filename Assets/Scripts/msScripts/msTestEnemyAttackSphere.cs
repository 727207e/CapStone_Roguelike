using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msTestEnemyAttackSphere : MonoBehaviour
{
    Rigidbody rigSelf;

    // Start is called before the first frame update
    void Start()
    {
        rigSelf = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigSelf.AddForce(new Vector3(-1, 0, 0) * 10f);
    }
}
