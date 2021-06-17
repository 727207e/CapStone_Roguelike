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


    /// <summary>
    /// ///////////플레이어 움직임 저지
    /// </summary>
    /// 

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
            player = GameObject.Find("MainCharacterSys 1");
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
    
    public void MoveScene(string theSceneName)
    {
        SceneManager.LoadScene(theSceneName);
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
        Start();

        StartCoroutine(fadeOut());

        if (SceneManager.GetActiveScene().name == "3_LabScene")
        {
            print("block");
            thePlayerController_Block = true; // 막음

            //연구소 이동시 캐릭터 특정위치에 배치
            GameObject charpos = GameObject.Find("CharPos");
            player.transform.Find("MainPlayerCharacter").
                transform.position = charpos.transform.position;

        }

        if (SceneManager.GetActiveScene().name == "DungeonScene")
        {
            thePlayerController_Block = false; // 움직임 가능
            player.transform.Find("MainPlayerCharacter").GetComponent<msPlayerControllerNew>().Start();
        }


    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
