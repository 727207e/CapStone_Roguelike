using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msShoot : MonoBehaviour
{
    private Transform thisTransform;
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("총구에서 총이 발사됩니다.");
            Instantiate(bullet1, thisTransform.position, thisTransform.rotation);
        }
    }

}
