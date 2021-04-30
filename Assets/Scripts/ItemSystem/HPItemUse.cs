using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItemUse : MonoBehaviour
{
    void Update()
    {
        if (Input.inputString == (transform.parent.GetComponent<Slot>().num + 1).ToString()) 
        {
            // 아이템 사용
            Debug.Log(" HP 상승 , slotNumber : " + (transform.parent.GetComponent<Slot>().num + 1));
            Destroy(this.gameObject);
        }
    }
}
