using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EenemyAttackArea : MonoBehaviour
{
    //적군
    private Collider playerAttackDetectedCollider;

    //Target을 검출한다면(유저를 찾는다면) enemyMove 스크립트에 Target으로 지정한다.
    private EnemyMove enemyMove;

    //공격 가능형태
    private bool AttackOn = false;

    // Start is called before the first frame update
    void Start()
    {
        //콜라이더를 가져온다.
        playerAttackDetectedCollider = transform.GetComponent<BoxCollider>();

        //부모에게 스크립트를 가져온다.
        enemyMove = transform.parent.GetComponent<EnemyMove>();

    }

    private void Update()
    {
        if (AttackOn&&enemyMove.AIAttack_CanAttack)
        {
            //공격 애니메이션
            enemyMove.Attack();
            enemyMove.AIAttack_CanAttack = false;
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        //유저 타겟 지정
        if (other.transform.tag == "Player")
        {
            AttackOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            AttackOn = false;
        }
    }

}
