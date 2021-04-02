using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public GameObject player; // 플레이어

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
        if(GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player");
        }
    }
}
