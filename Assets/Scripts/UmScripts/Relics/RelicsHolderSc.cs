using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RelicsHolderSc : MonoBehaviour
{
    public List<RelicsParentSc> relicsList;

    private static RelicsHolderSc instance = null;

    public GameObject prefabsTheRelicsItemSample;

    private void Awake()
    {
        if (instance == null)
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

    public static RelicsHolderSc Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void theRelicsReload()
    {
        //나중에
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            //오브젝트 생성
            GameObject obj = Instantiate(prefabsTheRelicsItemSample) as GameObject;

            //오브젝트내에 있는 카드 효과(카드 등장)
            obj.GetComponent<RelicsParentSc>().InstantiateCard();
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
        //씬 이름 체크(던전일 때는 유물 로드)
        if(scene.name == "Dungeon")
        {

        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
