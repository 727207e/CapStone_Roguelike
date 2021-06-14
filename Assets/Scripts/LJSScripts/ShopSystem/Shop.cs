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

    /// ////////////////////////////////////////////////////////////////////////
    /// 돈관리 하고 난 이후 필요한 업데이트
    /// 
    /// 지금은 돈만 업데이트 하니까 List에 돈이 사용되는 UI를 집어넣었습니다.
    /// 이후엔 필요한 Ui갱신이 있으면 넣으면 됩니다.
    /// 또는 더 필요한 갱신이 없다면 List<Text> 대신 gameobject만 넣으셔도 될꺼같습니다.
    /// 
    
    /// 서버에서 돈을 빼면 Text를 갱신합니다.
    public List<Text> UiText;




    public void Start()
    {
        //게임을 시작하면 Data를 Load 합니다.
       //  DataManager.Instance.LoadData();

        //로드 이후 UI창을 업데이트합니다.
        UpdateUi();
    }





    public void Enter(Player player)
    {
        enterPlayer = player;  // 플레이어 정보 저장하며 UI 위치 이동
        uiGroup.anchoredPosition = Vector3.zero;  // 화면 중앙에 위치
    }

    // Update is called once per frame
    public void Exit()
    {
        uiGroup.anchoredPosition = Vector3.down * 2000;  // 화면아래로 위치시킨다.
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
        else
            StopCoroutine(Talk2());  // 이미 진행되고 있는 코루틴 제거
            StartCoroutine(Talk2()); // 코루틴 실행




        //enterPlayer.Coin -= price;

        ///////////////////////////////////////////////////////////////////////////////////서버에서 돈 관리
        // DataManager.Instance.data.Money -= price;

        // DataManager.Instance.GameSave(); // 저장

        UpdateUi(); //Ui 갱신

        //로드는 게임 재시작, 또는 다른 특이사항에 진행합니다.
        //(불필요한 로드)
        ////////////////////////////////////////////////////////////////////////////////////





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
    IEnumerator Talk2()
    {
        talkText.text = talkData[2];  // 구매할수 없다는 메시지를
        yield return new WaitForSeconds(2f);  // 코루틴으로 2초간 출력후
        talkText.text = talkData[0]; // 원래의 메시지를 출력한다.
    }

    /// /////////////////////////////////////////////////////////////////////////////////////텍스트 갱신
    public void UpdateUi()
    {
        for(int i = 0; i < UiText.Count; i++)
        {
            //텍스트 갱신
            UiText[i].text = DataManager.Instance.data.Money.ToString();
        }
    }
}
