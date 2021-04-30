using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItemUse : MonoBehaviour
{
     void Start()
    {
       
    }

    void Update()
    {
        LifeManaHandler lifeManaHandler = GameObject.Find("HealthSystem").GetComponent<LifeManaHandler>();

        if (Input.inputString == (transform.parent.GetComponent<Slot>().num + 1).ToString()) 
        {
            // 아이템 사용
            lifeManaHandler.currentLife += 10;
            if ( lifeManaHandler.currentLife >= 100)
            {
                lifeManaHandler.currentLife = 100;
            }
            Debug.Log(" HP 상승 , slotNumber : " + (transform.parent.GetComponent<Slot>().num + 1));
            Destroy(this.gameObject);
        }
    }
}
