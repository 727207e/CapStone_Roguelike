using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msBullet1 : MonoBehaviour
{
    public float bulletSpeed = 1000.0f;
    public int bulletDamage = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FireBullet()
    {
        Debug.Log(gameObject.transform);
        GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * bulletSpeed);
        //Debug.Log("Bullet Fired with" + bulletDamage + " Damage");
    }

    public void SetBulletDamage(int x)
    {
        bulletDamage = x;
        //Debug.Log("Set Bullet Damage " + bulletDamage);
    }

    public int GetBulletDamage()
    {
        return bulletDamage;
    }
}
