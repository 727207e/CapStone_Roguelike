using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerDistanceDetected : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //유저 타겟 지정
        if (other.transform.tag == "Player")
        {
            //공격 범위안으로 들어옴
            transform.parent.transform.parent.GetComponent<BossPartentScripts>()
                .attack_DistanceLimitToPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            //공격 범위밖으로 나감
            transform.parent.transform.parent.GetComponent<BossPartentScripts>()
                .attack_DistanceLimitToPlayer = false;

        }
    }

}
