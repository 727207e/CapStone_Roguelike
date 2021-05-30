using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject[] weapons; // 무기관련 배열함수 선언1
    public bool[] hasWeapons; //무기관련 배열함수 선언2

    GameObject nearObject; // 트리거된 아이템을 위한 선언
    bool iDown;
    bool isShop;

    public int Coin;  // 재화
    public int Coin2;  // 영구적재화
    public int Ammo;  // 방어력
    public int Health;  // 체력

    public int MaxCoin;  // 최대재화 
    public int MaxCoin2;  // 최대영구적재화
    public int MaxAmmo; // 최대방어력
    public int MaxHealth;  // 최대체력



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Interation(); // 상호작용함수
    }

    void GetInput()
    {
        iDown = Input.GetButton("Interation");  // inputManager에서 Interation 항목을 e키로 설정해야한다.
    }

    void Interation()
    {
        if (iDown && nearObject != null)  // 주변에 nearobject 같은 상호작용할만한 물체가 있을때 
        {  
            if(nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>(); // 아이템 정보를 가져오고
                int weaponIndex = item.value; // value 값으로 아이템 확인
                hasWeapons[weaponIndex] = true; // 해당 무기 입수 체크

                Destroy(nearObject);
            }

            else if (nearObject.tag == "Shop")
            {
                Shop shop = nearObject.GetComponent<Shop>(); 
                shop.Enter(this); // player 정보 자기자신에 접근
                isShop = true; // 플래그변수 true로
            }


        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                case Item.Type.Ammo:  // enum Type에 맞춰 아이템 수치를 player 수치에 적용 
                    Ammo += item.value;
                    if (Ammo > MaxAmmo)
                        Ammo = MaxAmmo;
                    break;

                case Item.Type.Coin:
                    Coin += item.value;
                    if (Coin > MaxCoin)
                        Coin = MaxCoin;
                    break;

                case Item.Type.Coin2:
                    Coin2 += item.value;
                    if (Coin2 > MaxCoin2)
                        Coin2 = MaxCoin2;
                    break;

                case Item.Type.Health:
                    Health += item.value;
                    if (Health > MaxHealth)
                        Health = MaxHealth;
                    break;
            }
            Destroy(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon" || other.tag == "Shop")
            nearObject = other.gameObject;
         }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = null;
        else if (other.tag == "Shop") {
            Shop shop = nearObject.GetComponent<Shop>();
            shop.Exit();
            isShop = false; // 퇴장한순간 공격가능
            nearObject = null; 
        }
    }
}
