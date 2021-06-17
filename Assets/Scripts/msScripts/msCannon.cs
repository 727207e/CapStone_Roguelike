using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msCannon : MonoBehaviour
{
    public GameObject bulletPrefab; //사용하는 총알 이후 총이랑 따로 분리해야함.
    public Transform muzzleTransform; //총알이 발사되는 곳
    //public ParticleSystem muzzleFlash;
    //public ParticleSystem muzzleFlash2;

    public int fullAmmo = 5;
    public int currentAmmo;

    public float reloadTime = 6.0f; //재장전 시간
    public bool onReload; //현재 재장전 중인가?

    public int cannonDamage = 60; //총 데미지
    public float fireRate = 2.00f; //연사속도
    public bool isFiring = false;
    public float timer;



    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = fullAmmo;
        onReload = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        if (Input.GetButton("Fire1"))
        {
            isFiring = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            isFiring = false;
        }

        if (timer >= fireRate)
        {
            if (isFiring == true)
            {
                if (onReload == false)
                {
                    if (currentAmmo == 0)
                    {
                        //Debug.Log("재장전이 필요합니다.");
                    }
                    else
                    {
                        AudioManager.instance.PlaySound2D("WeaponCannonShot");
                        var go = Instantiate(bulletPrefab);
                        go.transform.position = muzzleTransform.position;
                        go.transform.rotation = muzzleTransform.rotation;
                        var bullet = go.GetComponent<msBullet_Cannon>();
                        bullet.SetBulletDamage(cannonDamage);
                        bullet.FireBullet();
                        //Instantiate(bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
                        //muzzleFlash.Emit(1);
                        //muzzleFlash2.Emit(1);
                        currentAmmo--;
                        //Debug.Log("남은 장탄량 : " + currentAmmo);
                    }
                }
                else
                {
                    //Debug.Log("재장전 중입니다.");
                }
            }
            timer = 0.0f;
        }
        else
        {
            timer += Time.deltaTime;
        }

    }

    private void Fire()
    {
        if (onReload == false)
        {
            if (currentAmmo == 0)
            {
                Debug.Log("재장전이 필요합니다.");
            }
            else
            {
                var go = Instantiate(bulletPrefab);
                go.transform.position = muzzleTransform.position;
                var bullet = go.GetComponent<msBulletNew>();
                bullet.Fire(go.transform.position, muzzleTransform.eulerAngles, gameObject.layer);
                //muzzleFlash.Emit(1);
                currentAmmo--;
                Debug.Log("남은 장탄량 : " + currentAmmo);
            }
        }
        else
        {
            Debug.Log("재장전 중입니다.");
        }

    }

    public IEnumerator Reload()
    {
        onReload = true;
        Debug.Log("재장전중...");


        yield return new WaitForSeconds(reloadTime);

        currentAmmo = fullAmmo;
        Debug.Log("재장전 완료");
        onReload = false;
        yield return null;
    }
}
