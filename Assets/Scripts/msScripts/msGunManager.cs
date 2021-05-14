using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msGunManager : MonoBehaviour
{
    private Transform thisTransform;

    public GameObject player;
    public msPlayerControllerNew msPCN;

    public GameObject normalGun;
    public GameObject gun2; //차후 이름 변경
    public GameObject gun3; //차후 이름 변경

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();

        FindPlayerPhase();
        SelectGunPhase();
    }

    // Update is called once per frame

    void Update()
    {
        
    }

    public void Fire()
    {
        
    }

    void SelectGunPhase()
    {
        Debug.Log("Current Weapon : " + msPCN.currentWeapon);

        switch (msPCN.currentWeapon)
        {
            case 0:
                Instantiate(normalGun, thisTransform);
                Debug.Log("OneHandGun Created");
                break;
            case (msPlayerControllerNew.Weapon)1:
                Instantiate(gun2, thisTransform);
                Debug.Log("MachineGun Created");
                break;
            case (msPlayerControllerNew.Weapon)2:
                Instantiate(gun3, thisTransform);
                Debug.Log("Gun3 Created");
                break;
            case (msPlayerControllerNew.Weapon)3:
                Debug.Log("Weapon Disabled");
                break;
            default:
                Debug.Log("잘못된 경우입니다.");
                break;

        }
    }

    void FindPlayerPhase()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Debug.Log("플레이어 캐릭터가 현재 존재합니다.");
        }
        else
        {
            Debug.LogError("플레이어 캐릭터가 존재하지 않습니다.");
        }
        msPCN = player.GetComponent<msPlayerControllerNew>();
    }
}
