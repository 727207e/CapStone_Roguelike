using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStagechange : MonoBehaviour
{
    public GameObject dott;
    public float time = 0;

    Vector3 target = Vector3.zero;
    Vector3 Player_position = Vector3.zero;
    //스위치 변수
    int swcnt = 0;

    // Update is called once per frame
    void Update()
    {
        // Player_position = 해당 물체의 위치 저장 (편의성위함)
        Player_position = transform.position;

        moving(swcnt);
        route();

        // 마우스 좌클릭시 스위치 변수 1 증가
        if (Input.GetMouseButtonDown(0))
            swcnt++;
    }

    // 오브젝트 이동 스위치 값 받아서 그거에 따른 목표지점 설정
    public void moving(int switchcnt)
    {
        switch (switchcnt)
        {
            case 0:
                target = new Vector3(8, 2, 0);
                break;

            case 1:
                target = new Vector3(2, 8, 0);
                break;

            case 2:
                target = new Vector3(-5, 4, 0);
                break;
        }
        // target 지점으로 등속이동
        transform.position = Vector3.MoveTowards(Player_position, target, 3.0f * Time.deltaTime);
    }

    // 이동경로에 흔적남기기
    public void route()
    {
        time += Time.deltaTime;

        if (time >= 0.5f)
        {
            time = 0.0f;
            if (transform.position != target)
                Instantiate(dott, Player_position, Quaternion.identity);
        }
    }
}
