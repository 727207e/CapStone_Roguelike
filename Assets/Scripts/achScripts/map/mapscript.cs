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
    //맵
    private int map_Total_Count = 9;
    public int stage_Position = 0;

    //플레이어
    public GameObject player;

    // 맵 리스트
    public List<GameObject> maps = new List<GameObject>();
    public List<GameObject> map_List = new List<GameObject>();
    public List<GameObject> boss = new List<GameObject>();
    public GameObject beforeBoss;

    // 남은 몬스터 갯수
    public int monster_count = 0;

    // 문
    public GameObject[] gates = new GameObject[3];

    public GameObject datamanager;
    public DataManager data;

    // 트리거
    private bool musictrigger = true;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;

        datamanager = GameObject.Find("SaveManager");
        data = datamanager.GetComponent<DataManager>();

        data.LoadData();

        player = GameObject.FindGameObjectWithTag("Player");

        AudioManager.instance.PlayMusic("BGMMainStage");

        //monsters_List = new GameObject[10];
        randomized_map();

        // 초기맵 자동생성
        map_List[stage_Position].SetActive(true);
        Debug.Log("위치: " + (stage_Position + 1));
        player.transform.position = map_List[stage_Position].transform.Find("Player_Spawn_1").position;
        //monsters_Point = GameObject.FindGameObjectsWithTag("Monster_Spawn_Point");
    }

    // Update is called once per frame
    void Update()
    {
        datamanager = GameObject.Find("SaveManager");
        data = datamanager.GetComponent<DataManager>();

        player = GameObject.FindGameObjectWithTag("Player");

        // 좌클릭시 다음맵 우클릭시 이전맵
        // 범위 벗어나는 값에대해서는 설정안해둬서 오류남
        // 맵이동 테스트용으로 넣은것이므로 추후에 지울것
        if (Input.GetKeyDown(KeyCode.O) == true && stage_Position != 8)
            nextMap();

        //if (Input.GetKeyDown(KeyCode.Mouse1) == true)
        //    previousMap();

        //몬스터 생성
        //monsters_Spawn();

        if (stage_Position == 8 && musictrigger)
        {
            AudioManager.instance.PlayMusic("BGMBossStage");
            musictrigger = false;
        }
    }

    // 다음 맵으로
    public void nextMap()
    {
        stage_Position++;
        map_List[stage_Position - 1].SetActive(false);
        map_List[stage_Position].SetActive(true);
        player.transform.position = map_List[stage_Position].transform.Find("Player_Spawn_1").position;

        data.GameSave();
        // 페이드
        FadeController.instance.objFadein();
    }

    // 이전 맵으로
    public void previousMap()
    {
        stage_Position--;
        map_List[stage_Position + 1].SetActive(false);
        map_List[stage_Position].SetActive(true);
        player.transform.position = map_List[stage_Position].transform.Find("Player_Spawn_2").position;

        data.GameSave();
        //페이드
        FadeController.instance.objFadein();
    }
    // 맵 랜덤섞기
    public void randomized_map()
    {
        // 랜덤값 저장하는 인수 선언
        int random_count;
        for (int i = 0; i < 8; i++)
        {
            // 보스 통로방 생성
            if (i == 7)
            {
                map_List.Add(Instantiate(beforeBoss, Vector3.zero, Quaternion.identity) as GameObject);
                map_List[i].SetActive(false);
            }
            // 그 외 스테이지 랜덤생성
            else
            {
                //랜덤범위 : maps 갯수
                random_count = Random.Range(0, maps.Count);
                map_List.Add(Instantiate(maps[random_count], Vector3.zero, Quaternion.identity) as GameObject);
                map_List[i].SetActive(false);

                maps.RemoveAt(random_count);
            }
        }
        random_count = Random.Range(0, 2);
        map_List.Add(Instantiate(boss[random_count], Vector3.zero, Quaternion.identity) as GameObject);
        map_List[8].SetActive(false);
    }
}