using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instanace;
    private void Awake()
    {
        
    }
    public Player player;
    public float originalAttack = 100;
    public float originalAmmo = 20;

    void Start()
    {
       
    }

    public List<BaseBuff> onBuff = new List<BaseBuff>(); // 버프가 생성될때마다 추가

    public float BuffChange(string type, float origin)
    {
        if (onBuff.Count > 0)
        {
            float temp = 0;
            for (int i = 0; i < onBuff.Count; i++) // 타입을 비교하고
            {
                if (onBuff[i].type.Equals(type))  // 알맞은 타입이라면
                    temp += origin * onBuff[i].percentage; // 퍼센트 변환식으로 값을 구한뒤
            }
            return origin + temp; // 반환
        }

        else
        {
            return origin;  // 리스트에 아무것도 없다면 origin 반환
        }
    }

    public void ChooseBuff(string type)
    {
        switch(type)
        {
            case "Attack":
                player.Attack = BuffChange(type,originalAttack); // 버프 체인지 메소드  ,  공격은 공격효과끼리 더한다
                break;
            case "Ammo":
                player.Ammo = BuffChange(type,originalAmmo); // 마찬가지
                break;

        }
    }
}
