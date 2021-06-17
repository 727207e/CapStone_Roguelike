using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public GameObject player; // 플레이어
    public float healthPoint = 1000; //체력

    public GameObject main_Camera;

    public Image fadeInAndOutPlane;

    public Action FadeInAndOutAfterFuction;

    public Transform audioListener;

    //이동될 씬 이름
    string theScenename;


    public bool thePlayerController_Block = false;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            //씬 전환이 되어도 파괴되지 않는다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //씬 전환이후 그 씬에도 gamemanager가 있을수 있음.
            //따라서 새로운 씬의 gamemanager 파괴
            Destroy(this.gameObject);

        }
    }

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("MainCharacterSys_Final");
        }

        if(main_Camera == null)
        {
            main_Camera = GameObject.Find("Main Camera");
        }
         
        if(fadeInAndOutPlane == null)
        {
            fadeInAndOutPlane = main_Camera.transform.Find("CameraBlockCanvas").
                transform.Find("FadePlane").GetComponent<Image>() ;
        }

    }

    void Update()
    {
        if (player != null)
        {
            //오디오 리스너를 플레이어에게 붙인다
            audioListener.position = player.transform.position;
        }
    }

    private void LoadDataFromJson()
    {
        //JSON 파일에서 플레이어 데이터를 가져오는 경우 수행하는 함수이다.
    }

    //////////////////////////////////////////////////////////Scene에 관련된 함수들
    /// <summary>
    /// 
    /// </summary>

    public void NextSceneFadein(string theSceneName)
    {
        theScenename = theSceneName;

        FadeInAndOutAfterFuction += MoveScene;

        StartCoroutine(fadeIn());

    }


    public void MoveScene()
    {
        SceneManager.LoadScene(theScenename);
    }

    public IEnumerator fadeIn()
    {
        float _time = 0;
        
        Color color = fadeInAndOutPlane.color;

        while(_time < 1)
        {
            _time += Time.deltaTime;

            color.a =  _time;

            fadeInAndOutPlane.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        //저장된 함수 실행(씬 전환)
        FadeInAndOutAfterFuction();
    }

    public IEnumerator fadeOut()
    {
        float _time = 0;

        Color color = fadeInAndOutPlane.color;

        while (_time < 1)
        {
            _time += Time.deltaTime;

            color.a = (1-_time);

            fadeInAndOutPlane.color = color;

            yield return null;
        }
    }


    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //게임 매니저 정보 읽어오기
        Start();


        //화면 밝아짐
        StartCoroutine(fadeOut());

        //연구소 씬일경우
        if (SceneManager.GetActiveScene().name == "3_LabScene")
        {

            //캐릭터 이동 잠금
            player.transform.Find("MainPlayerCharacter").GetComponent<msPlayerControllerNew>()
                .SetIsStage(false);


            //연구소 이동시 캐릭터 특정위치에 배치
            GameObject charpos = GameObject.Find("CharPos");
            player.transform.Find("MainPlayerCharacter").
                transform.position = charpos.transform.position;

        }


        //주사기 일 경우
        if (SceneManager.GetActiveScene().name == "4_InjectionScene")
        {
            //캐릭터 이동 잠금
            player.transform.Find("MainPlayerCharacter").GetComponent<msPlayerControllerNew>()
                .SetIsStage(false);

            //Ui off
            player.transform.Find("MainPlayerCharacter").GetComponent<msPlayerControllerNew>()
                .OnOffAllUI(false);


        }


        //던전씬 입장일 경우
        if (SceneManager.GetActiveScene().name == "DungeonScene")
        {

            //캐릭터 이동 잠그고 UI 잠금
            player.transform.Find("MainPlayerCharacter").GetComponent<msPlayerControllerNew>()
    .OnOffAllUI(true);

            player.transform.Find("MainPlayerCharacter").GetComponent<msPlayerControllerNew>()
    .SetIsStage(true);

            //정보갱신
            player.transform.Find("MainPlayerCharacter").GetComponent<msPlayerControllerNew>().Start();
        }


    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
