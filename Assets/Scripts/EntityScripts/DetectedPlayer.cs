using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedPlayer : MonoBehaviour
{    
    //적군을 찾아내는 콜라이더(콜라이더 내부로 유저가 들어가면 target으로 지정된다)
    private Collider playerDetectedCollider;

    //Target을 검출한다면(유저를 찾는다면) enemyMove 스크립트에 Target으로 지정한다.
    private EnemyMove enemyMove;

    // Start is called before the first frame update
    void Start()
    {
        //콜라이더를 가져온다.
        playerDetectedCollider = transform.GetComponent<BoxCollider>();

        //부모에게 스크립트를 가져온다.
        enemyMove = transform.parent.GetComponent<EnemyMove>();

    }

    private void OnTriggerEnter(Collider other)
    {
        //유저 타겟 지정
        if (other.transform.tag == "Player")
        {
            enemyMove.target = other.gameObject;
        }
    }

}
