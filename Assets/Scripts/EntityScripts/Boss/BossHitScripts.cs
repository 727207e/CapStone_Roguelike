using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitScripts : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == ("Bullet"))
        {
            int damage = 5;


            //총알 공격력을 가져온다
            /*
            
            damage = other.GetComponent<msBulletNew>().bulletDamage;

             */



            ////////////////////////////////////////////////보스
            ///

            //체력 감소
            transform.parent.parent.GetComponent<IBoss>().BossHp -= damage;






            //총알 사라지는 함수(총알이 무언가랑 부딪힐때 나타날 파티클,
            //              사라질때 효과, 없어지는 함수(Destroy)등등)

            /*
            other.GetComponent<msBulletNew>().BulletDisappear();
            */


            print("hit");
            print(transform.parent.parent.GetComponent<IBoss>().BossHp);



            //////////////////////////////////////지울것
            Destroy(other.gameObject);
            //////////////////////////////////////지울것


        }
    }
}
