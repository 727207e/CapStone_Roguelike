using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterStatus : MonoBehaviour
{
    public int HP;
    public GameObject coin;
    private int position;
    // Start is called before the first frame update
    void Start()
    {
        HP = 1;
        position = mapscript.instance.stage_Position;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            // 랜덤값
            int rand = Random.Range(1, 4);
            // 코인을 맵의 자식오브젝트로 생성
            // 랜덤값 만큼 반복
            for (int i = 1; i <= rand; i++)
            {
                GameObject coinn = Instantiate(coin, mapscript.instance.map_List[position].transform) as GameObject;
                // 코인 위치조정
                coinn.transform.position = transform.position;
            }

            //몬스터 수 감소
            mapscript.instance.monster_count--;

            // 생성한 뒤 제거
            Destroy(gameObject);
        }

        // 디버그용 몬스터 체력 0 만들어서 없애기
        if (Input.GetKeyDown(KeyCode.Mouse2) == true)
        {
            HP = 0;
        }
    }
}
