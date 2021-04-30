using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapscript : MonoBehaviour
{
    private int map_Total_Count = 17;
    public int stage_Position = 0;

    public GameObject[] maps = new GameObject[15];
    public GameObject healing_map;
    public GameObject[] map_List;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        map_List = new GameObject[map_Total_Count];

        randomized_map();

        // 초기맵 자동생성
        map_List[stage_Position].SetActive(true);
        Debug.Log("위치: " + (stage_Position + 1));
        player.transform.position = map_List[stage_Position].transform.FindChild("Player_Spawn_1").position;
    }

    // Update is called once per frame
    void Update()
    {
        // 좌클릭시 다음맵 우클릭시 이전맵
        // 범위 벗어나는 값에대해서는 설정안해둬서 오류날수있음
        if (Input.GetKeyDown(KeyCode.Mouse0) == true)
        {
            stage_Position++;
            map_List[stage_Position - 1].SetActive(false);
            map_List[stage_Position].SetActive(true);
            Debug.Log("위치: " + (stage_Position + 1));
            player.transform.position = map_List[stage_Position].transform.FindChild("Player_Spawn_1").position;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) == true)
        {
            stage_Position--;
            map_List[stage_Position + 1].SetActive(false);
            map_List[stage_Position].SetActive(true);
            Debug.Log("위치: " + (stage_Position + 1));
            player.transform.position = map_List[stage_Position].transform.FindChild("Player_Spawn_2").position;
        }

    }
    // 맵 랜덤섞기
    public void randomized_map()
    {
        // 랜덤값 저장하는 인수 선언
        int random_count;
        for (int i = 0; i < map_Total_Count; i++)
        {
            // 힐링스테이지 강제생성
            if (i == 5 || i == 11)
            {
                map_List[i] = Instantiate(healing_map, Vector3.zero, Quaternion.identity) as GameObject;
                map_List[i].SetActive(false);
            }
            // 그 외 스테이지 랜덤생성
            else
            {
                //랜덤범위 : 0 <= 범위 < 전체맵-i-힐링스테이지 갯수
                random_count = Random.Range(0, map_Total_Count - i - 2);
                map_List[i] = Instantiate(maps[random_count], Vector3.zero, Quaternion.identity) as GameObject;
                map_List[i].SetActive(false);
                map_Reroll(random_count);
            }
        }
    }

    // 맵 랜덤섞기 중복제외하게 만드는중
    public void map_Reroll(int i)
    {
        for (int j = i; j < map_Total_Count-1-2; j++)
        {
            maps[j] = maps[j + 1];
        }
    }
}
