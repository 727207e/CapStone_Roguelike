using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageChange : MonoBehaviour
{
    static StageChange _instance = null;
    public static StageChange instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject startmap;
    public GameObject endmap;
    public GameObject map1;
    public GameObject map2;
    public GameObject map3;

    public GameObject[] maps;
    private int maplength = 5;
    public int mappos = 0;
    private int map_ty1 = 2;
    private int map_ty2 = 2;
    private int map_ty3 = 1;

    void Start()
    {
        _instance = this;
        maps = new GameObject[maplength+2];
        creMap();
    }

    public void creMap()
    {
        maps[0] = Instantiate(startmap, Vector3.zero, Quaternion.identity) as GameObject;
        maps[maplength+1] = Instantiate(endmap, Vector3.zero, Quaternion.identity) as GameObject;
        maps[maplength + 1].SetActive(false);
        for (int i = 1 ; i < maplength+1 ; i++)
        {
            Debug.Log("루프");
            //랜덤
            int cnt = Random.Range(1, 4);
            //1나옴
            if (cnt == 1)
            {
                //생성불가
                if (map_ty1 == 0)
                {
                    //다시굴림
                    int cnta = Random.Range(1, 3);
                    //1나옴 -> 2생성
                    if (cnta == 1)
                    {
                        //2 생성불가 -> 3생성
                        if (map_ty2 == 0)
                        {
                            maps[i] = Instantiate(map3, Vector3.zero, Quaternion.identity) as GameObject;
                            map_ty3--;
                        }
                        //2 생성
                        else
                        {
                            maps[i] = Instantiate(map2, Vector3.zero, Quaternion.identity) as GameObject;
                            map_ty2--;
                        }
                    }
                    //2나옴 -> 3생성
                    else if (cnta == 2)
                    {
                        //3 생성불가 -> 2생성
                        if (map_ty3 == 0)
                        {
                            maps[i] = Instantiate(map2, Vector3.zero, Quaternion.identity) as GameObject;
                            map_ty2--;
                        }
                        //3생성
                        else
                        {
                            maps[i] = Instantiate(map3, Vector3.zero, Quaternion.identity) as GameObject;
                            map_ty3--;
                        }
                    }
                }
                //생성가능
                else
                {
                    maps[i] = Instantiate(map1, Vector3.zero, Quaternion.identity) as GameObject;
                    map_ty1--;
                }
            }
            //2나옴
            else if (cnt == 2)
            {
                //2 생성불가
                if (map_ty2 == 0)
                {
                    //다시굴림
                    int cnta = Random.Range(1, 3);
                    //1나옴 -> 1생성
                    if (cnta == 1)
                    {
                        //1 생성불가 -> 3생성
                        if (map_ty1 == 0)
                        {
                            maps[i] = Instantiate(map3, Vector3.zero, Quaternion.identity) as GameObject;
                            map_ty3--;
                            Debug.Log("2-3-1");
                        }
                        //1생성
                        else
                        {
                            maps[i] = Instantiate(map1, Vector3.zero, Quaternion.identity) as GameObject;
                            map_ty1--;
                        }
                    }
                    //2나옴 -> 3생성
                    else if (cnta == 2)
                    {
                        //3 생성불가 -> 1생성
                        if (map_ty3 == 0)
                        {
                            maps[i] = Instantiate(map1, Vector3.zero, Quaternion.identity) as GameObject;
                            map_ty1--;
                        }
                        //3생성
                        else
                        {
                            maps[i] = Instantiate(map3, Vector3.zero, Quaternion.identity) as GameObject;
                            map_ty3--;
                        }
                    }
                }
                //2 생성
                else
                {
                    maps[i] = Instantiate(map2, Vector3.zero, Quaternion.identity) as GameObject;
                    map_ty2--;
                }
            }
            //3 나옴
            else if (cnt == 3)
            {
                //3 생성불가
                if (map_ty3 == 0)
                {
                    //다시굴림
                    int cnta = Random.Range(1, 3);
                    //1나옴 -> 1생성
                    if (cnta == 1)
                    {
                        //1 생성불가 -> 2생성
                        if (map_ty1 == 0)
                        {
                            maps[i] = Instantiate(map2, Vector3.zero, Quaternion.identity) as GameObject;
                            map_ty2--;
                        }
                        //1생성
                        else
                        {
                            maps[i] = Instantiate(map1, Vector3.zero, Quaternion.identity) as GameObject;
                            map_ty1--;
                        }
                    }
                    //2나옴 -> 2생성
                    else if (cnta == 2)
                    {
                        //2 생성불가 - > 1생성
                        if (map_ty2 == 0)
                        {
                            maps[i] = Instantiate(map1, Vector3.zero, Quaternion.identity) as GameObject;
                            map_ty1--;
                        }
                        //1생성
                        else
                        {
                            maps[i] = Instantiate(map2, Vector3.zero, Quaternion.identity) as GameObject;
                            map_ty2--;
                        }
                    }
                }
                //3 생성
                else
                {
                    maps[i] = Instantiate(map3, Vector3.zero, Quaternion.identity) as GameObject;
                    map_ty3--;
                }
            }
            maps[i].SetActive(false);
        }
        Debug.Log("맵만듬");
    }

    public void disableMap()
    {
        maps[mappos].SetActive(false);
    }

    public void activeMap()
    {
        maps[mappos].SetActive(true);
    }
}
