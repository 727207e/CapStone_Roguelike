using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabSceneManager : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject Player;
    public GameObject MapGround;

    private float blockLength; // 맵 밖으로 나가는 기준
    
    public float range = 3; // 맵 막기의 범위

    private Vector3 oldPos;
    private Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        Player = GameObject.FindWithTag("Player");
        if(Player != null)
        {

            //위치 저장
            oldPos = Player.transform.position;
        }


        //바닥의 길이를 가져온다(x = y 이기 때문에 x를 가져옴)
        //루트2 를 곱함(피타고라스 빗변공식)
        blockLength = MapGround.transform.localScale.x/2 * Mathf.Sqrt(2) - range;

        Player.GetComponent<msPlayerControllerNew>().SetPlayerDisabled(0);
    }

    // Update is called once per frame
    void Update()
    {
        //위치 저장
        //newPos = Player.transform.position;

        if (oldPos != newPos)
        {
            //지역 경계 밖으로 못나가게 막음
            Map_Block();
        }

    }

    private void Map_Block()
    {
        float pX = Player.transform.position.x;
        float pY = Player.transform.position.z;

        //맵 구역 안으로 들어오게 설정
        //
        //맵 구역 밖이라면 이전 좌표로 이동
        if (pX > 0)
        {
            if (pY > -pX + blockLength)
            {
                Player.transform.position = oldPos;
                return;
            }
            if (pY < pX - blockLength)
            {
                Player.transform.position = oldPos;
                return;
            }

        }

        else
        {
            if (pY < -pX - blockLength)
            {
                Player.transform.position = oldPos;
                return;
            }
            else if (pY > pX + blockLength)
            {
                Player.transform.position = oldPos;
                return;
            }
        }

        //맵 구역 안이라면 위치 변경없이 갱신
        oldPos = newPos;
    }
}
