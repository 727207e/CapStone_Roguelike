using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitScripts : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == ("PlayerAttack"))
        {
            int damage = 0;

            //캐논일 경우
            if (other.GetComponent<msBullet_Cannon>() != null)
            {
                damage = other.GetComponent<msBullet_Cannon>().bulletDamage;
            }


            //총알일 경우
            else if(other.GetComponent<msBullet1>() != null)
            {
                damage = other.GetComponent<msBullet1>().bulletDamage;
            }


            //체력 감소
            transform.parent.parent.GetComponent<IBoss>().BossHp -= damage;



            //총알 사라지는 함수(총알이 무언가랑 부딪힐때 나타날 파티클,
            //              사라질때 효과, 없어지는 함수(Destroy)등등)

            /*
            other.GetComponent<msBulletNew>().BulletDisappear();
            */

            //////////////////////////////////////지울것
            Destroy(other.gameObject);
            //////////////////////////////////////지울것


        }
    }
}
