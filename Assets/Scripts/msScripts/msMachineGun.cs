using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msMachineGun : MonoBehaviour
{
    public GameObject bulletPrefab; //사용하는 총알 이후 총이랑 따로 분리해야함.
    public Transform muzzleTransform; //총알이 발사되는 곳

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    private void Fire()
    {
        //recoilTimer = Time.time;
        for(int i = 1; i< 3; i++)
        {
            var go = Instantiate(bulletPrefab);
            go.transform.position = muzzleTransform.position;
            var bullet = go.GetComponent<msBulletNew>();
            bullet.Fire(go.transform.position, muzzleTransform.eulerAngles, gameObject.layer);
        }
        
    }
}
