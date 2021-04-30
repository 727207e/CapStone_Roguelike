using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPItemUse : MonoBehaviour
{
    void Update()
    {
        LifeManaHandler lifeManaHandler = GameObject.Find("HealthSystem").GetComponent<LifeManaHandler>();

        if (Input.inputString == (transform.parent.GetComponent<Slot>().num + 1).ToString()) 
        {
            // 아이템 사용
            lifeManaHandler.currentMana += 10;
            if (lifeManaHandler.currentMana >= 50)
            {
                lifeManaHandler.currentMana = 50;
            }

            Debug.Log(" MP 상승 , slotNumber : " + (transform.parent.GetComponent<Slot>().num + 1));
            Destroy(this.gameObject);
        }
    }
}
