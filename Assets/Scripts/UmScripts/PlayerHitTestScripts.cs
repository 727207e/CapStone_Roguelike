using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitTestScripts : MonoBehaviour
{
    bool isInvincibleTime = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //플레이어와 충돌한 오브젝트와 반응을 수행한다.
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BossAtt"))
        {
            BossAttackDamage boss = other.gameObject.GetComponent<BossAttackDamage>();

            if (boss)
            {
                print(boss.Damage);
            }

            isInvincibleTime = true;
            StartCoroutine("InvincibleTime");
        }

    }

    public void PlayerOnHit(int num)
    {
        print("player hit");
    }

    public IEnumerator InvincibleTime()
    {
        int countTime = 0;

        while (countTime < 10)
        {
            if (countTime % 2 == 0)
            {

            }
            else
            {

            }

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }

        isInvincibleTime = false;
        Debug.Log("무적 시간 종료");
        //무적시간 종료

        yield return null;
    }

}
