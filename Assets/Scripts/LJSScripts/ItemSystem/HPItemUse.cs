using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItemUse : MonoBehaviour
{
    public GameObject player;
    public msPlayerControllerNew msPCN;

    void Start()
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

    void Update()
    {
        if (Input.inputString == (transform.parent.GetComponent<Slot>().num + 1).ToString()) 
        {
            // 아이템 사용
            msPCN.healthPoint += 10;
            if (msPCN.healthPoint >= 100)
            {
                msPCN.healthPoint = 100;
            }
            Debug.Log(" HP 상승 , slotNumber : " + (transform.parent.GetComponent<Slot>().num + 1));
            Destroy(gameObject);
        }
    }
}
