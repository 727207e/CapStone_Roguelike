using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msGunManager : MonoBehaviour
{
    private Transform thisTransform;
    public GameObject normalGun;
    public GameObject gun2;
    public GameObject gun3;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();

        Instantiate(normalGun, thisTransform);
    }

    // Update is called once per frame

    void Update()
    {
        
    }

    void Shoot()
    {
        
    }
}
