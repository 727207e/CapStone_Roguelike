using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msOneHandGun : MonoBehaviour
{
    public GameObject bulletPrefab; //사용하는 총알 이후 총이랑 따로 분리해야함.
    public Transform muzzleTransform; //총알이 발사되는 곳
    public int fullAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 3.0f; //재장전 시간
    public bool onReload; //현재 재장전 중인가?

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = fullAmmo;
        onReload = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
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
        int countTime = 0;
        onReload = true;
        Debug.Log("재장전중...");

        while (countTime < 10)
        {
            Debug.Log("재장전중..." + countTime);
            yield return new WaitForSeconds(0.2f);

            countTime++;
        }
        currentAmmo = fullAmmo;
        Debug.Log("재장전 완료");
        onReload = false;
        yield return null;
    }
}
