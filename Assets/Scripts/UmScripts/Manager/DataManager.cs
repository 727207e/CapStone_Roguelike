using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


	//-> DataManager 정보 정달

	//-> Save

	//-> Load

	//-> Status -> DataManager 정보를 읽어옮.

public class DataManager : MonoBehaviour
{
    private static DataManager instance = null;

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

    public static DataManager Instance
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

    //저장 데이터 형식
    [System.Serializable]
    public class Data
    {
        public int Money; // 유저 돈
        public int Health_Hp_Max;
        public int Health_Mp_Max;
        public int Health_Strong; // 강인함
        public int Health_Armor; // 갑옷
        public int Damage_Critical;
        public int Controller_MoveSpeed;
        public int Controller_JumpPower;
        public int Controller_AttackSpeed;

        public Gun[] OwnGun;
    }

    [System.Serializable]
    public struct Gun
    {
        public int GunDamage;
        public int GunRange;
    }

    public Data data;

    public void Start()
    {
        LoadData();
    }

    public void GameSave()
    {
        string jsonData = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.dataPath, "playerData.json");
        File.WriteAllText(path, jsonData);
    }

    public void LoadData()
    {
        string path = Path.Combine(Application.dataPath, "playerData.json");
        string jsonData = File.ReadAllText(path);
        data = JsonUtility.FromJson<Data>(jsonData);
    }

}
