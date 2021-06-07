using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackDamage : MonoBehaviour
{
    public int Damage;

    public GameObject myboss;
     
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if(myboss != null)
                myboss.SendMessage("MyHitpointGot", gameObject);
        }
    }


}
