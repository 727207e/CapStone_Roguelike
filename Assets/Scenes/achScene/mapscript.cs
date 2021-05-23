using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapscript : MonoBehaviour
{
    static mapscript _instance = null;
    public static mapscript instance
    {
        get
        {
            return _instance;
        }
    }
    private int map_Total_Count = 17;
    public int stage_Position = 0;

    public GameObject player;

    public GameObject[] maps = new GameObject[15];
    public GameObject[] map_List;
    public GameObject healing_map;

    public GameObject monster_type_M;
    public GameObject monster_Type_R;
    private GameObject[] monsters_Point;
    private GameObject[] monsters_List;

    public int monster_count = 0;

    public GameObject[] gates = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;

        map_List = new GameObject[map_Total_Count];
        monsters_List = new GameObject[10];
        randomized_map();

        // 초기맵 자동생성
        map_List[stage_Position].SetActive(true);
        Debug.Log("위치: " + (stage_Position + 1));
        player.transform.position = map_List[stage_Position].transform.Find("Player_Spawn_1").position;
        monsters_Point = GameObject.FindGameObjectsWithTag("Monster_Spawn_Point");
    }

    // Update is called once per frame
    void Update()
    {
        // 좌클릭시 다음맵 우클릭시 이전맵
        // 범위 벗어나는 값에대해서는 설정안해둬서 오류남
        // 맵이동 테스트용으로 넣은것이므로 추후에 지울것
        if (Input.GetKeyDown(KeyCode.Mouse0) == true && monster_count <= 0)
            nextMap();

        if (Input.GetKeyDown(KeyCode.Mouse1) == true && monster_count <= 0)
            previousMap();

        monsters_Spawn();
    }

    // 다음 맵으로
    public void nextMap()
    {
        stage_Position++;
        map_List[stage_Position - 1].SetActive(false);
        map_List[stage_Position].SetActive(true);
        Debug.Log("위치: " + (stage_Position + 1));
        player.transform.position = map_List[stage_Position].transform.Find("Player_Spawn_1").position;

        // 페이드
        FadeController.instance.objFadein();
    }

    // 이전 맵으로
    public void previousMap()
    {
        stage_Position--;
        map_List[stage_Position + 1].SetActive(false);
        map_List[stage_Position].SetActive(true);
        Debug.Log("위치: " + (stage_Position + 1));
        player.transform.position = map_List[stage_Position].transform.Find("Player_Spawn_2").position;

        //페이드
        FadeController.instance.objFadein();
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
            maps[j] = maps[j + 1];
    }

    public void monsters_Spawn()
    {
        monsters_Point = GameObject.FindGameObjectsWithTag("Monster_Spawn_Point");

        for (int i = 0; i < monsters_Point.Length; i++)
        {
            if (monsters_Point[i] != null)
            {
                // 0이면 근접 1이면 원거리
                int rand = Random.Range(0, 2);
                // 몬스터 생성 한 뒤 배열에 집어넣기
                if (rand == 0)
                {
                    monsters_List[i] = Instantiate(monster_type_M, map_List[stage_Position].transform) as GameObject;
                    monsters_List[i].transform.position = monsters_Point[i].transform.position;
                }
                else if (rand == 1)
                {
                    monsters_List[i] = Instantiate(monster_Type_R, map_List[stage_Position].transform) as GameObject;
                    monsters_List[i].transform.position = monsters_Point[i].transform.position;
                }
                

                monster_count++;
                // 몬스터 생성지점 제거해서 추가생성 방지
                Destroy(monsters_Point[i]);
                // 몬스터 생성지점 배열 값 제거
                monsters_Point.SetValue(null, i);
            }
        }
    }
}
