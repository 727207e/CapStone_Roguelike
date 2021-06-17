using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharBlockDestroy : MonoBehaviour
{
    private static MainCharBlockDestroy instance = null;

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

    public static MainCharBlockDestroy Instance
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
}
