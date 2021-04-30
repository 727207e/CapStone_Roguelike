using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackCollider : MonoBehaviour
{
    public int WeaponAttackDamage;

    private void OnTriggerEnter(Collider other)
    {
        //유저 타겟 지정
        if (other.transform.tag == "Player")
        {

            other.SendMessage("OnHit", WeaponAttackDamage);
        }
    }
}
