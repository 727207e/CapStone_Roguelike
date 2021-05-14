using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public RectTransform uiGroup;
    public GameObject[] itemObj;  // 아이템프리펩
    public int[] itemPrice;  // 아이템가격
    public Transform[] itemPos;  //위치배열변수
    public string[] talkData;
    public Text talkText;


    Player enterPlayer;

    public void Enter(Player player)
    {
        enterPlayer = player;  // 플레이어 정보 저장하며 UI 위치 이동
        uiGroup.anchoredPosition = Vector3.zero;  // 화면 중앙에 위치
    }

    // Update is called once per frame
    public void Exit()
    {
        uiGroup.anchoredPosition = Vector3.down * 1000;  // 화면아래로 위치시킨다.
    }

    public void Buy(int index) // 어떤 물건인지 판별
    {
        int price = itemPrice[index]; // 선택한 아이템 가격 인덱스
        if(price > enterPlayer.Coin)
        {
            StopCoroutine(Talk());  // 이미 진행되고 있는 코루틴 제거
            StartCoroutine(Talk()); // 코루틴 실행
            return;
            }
                

        enterPlayer.Coin -= price;
        Vector3 ranVec = Vector3.right * Random.Range(0, 0)
                        + Vector3.forward * Random.Range(0, 0);
        Instantiate(itemObj[index], itemPos[index].position + ranVec, itemPos[index].rotation);

    }

    IEnumerator Talk()
    {
        talkText.text = talkData[1];  // 구매할수 없다는 메시지를
        yield return new WaitForSeconds(2f);  // 코루틴으로 2초간 출력후
        talkText.text = talkData[0]; // 원래의 메시지를 출력한다.
    }


}
