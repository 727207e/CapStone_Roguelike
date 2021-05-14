using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Ammo, Coin, Weapon, Health , Coin2}; // 타입선언
    public Type type; // 종류
    public int value; // 값



    void Update()
    {
        transform.Rotate(Vector3.down * 50 * Time.deltaTime); // 아이템이 일정하게 회전
    }
}
