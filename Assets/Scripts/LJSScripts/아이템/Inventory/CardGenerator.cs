using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGenerator : MonoBehaviour
{
    /// <summary>
    /// //////////////////////////////////////////////////////////////////////
    /// 
    /// (k를 누를경우)
    /// 1. SumthePercent()함수가 각 카드의 가중치를 더함으로써 전체 확률을 계산한다.
    ///     1-1. ResultSelect()함수가 3개의 카드를 뽑는다.
    ///     1-2. 각 카드들은 RandomCard()함수를 통해 result 리스트에 포함된다.
    ///     
    /// 
    /// 
    /// 
    /// 
    /// 
    /// </summary>


    //////////////////////////////////////////////////////////////카드 오브젝트
    public List<Items> TotalCardList;  // 카드 덱(전체)

    // 랜덤하게 선택된 카드를 담을 리스트
    public List<Items> RandomCardSelectresult = new List<Items>();

    public int total = 0;  // 카드들의 가중치 총 합

    //카드의 모양(실 모양)
    public GameObject Prefabs_Card_Obj;

    //카드를 띄울 캔버스
    public GameObject Canvas_Card;

    public Camera m_camera;

    //////////////////////////////////////////////////////////////카드 결정
    GameObject theCanvasObj;


    //////////////////////////////////////////////////////////////인벤토리 등등
    public InventoryScript inventoryScript;


    void SumthePercent()
    {
        total = 0;

        for (int i = 0; i < TotalCardList.Count; i++)
        {
            // 스크립트가 활성화 되면 카드 덱의 모든 카드의 총 가중치를 구한다
            total += TotalCardList[i].weight;
        }


        // 실행
        ResultSelect();
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {

            SumthePercent();

            //캔버스 생성
            theCanvasObj = Instantiate(Canvas_Card) as GameObject;

            for (int count = 0; count < 3; count++)
            {
                GeneratorCard(count);
            }
        }
    }

    //////////////////////////////////////////////////////////////카드 생성
    public void GeneratorCard(int count)
    {

        GameObject theCardObj;

        //카드 생성
        theCardObj = Instantiate(Prefabs_Card_Obj, theCanvasObj.transform.Find("Panel")) as GameObject;

        //카드 이미지 대입
        theCardObj.transform.Find("Front").transform.Find("CardImage").
            GetComponent<Image>().sprite = RandomCardSelectresult[count].cardImage;

        //카드 이름 대입
        theCardObj.transform.Find("Front").transform.Find("CardName").
            GetComponent<Text>().text = RandomCardSelectresult[count].theName();

        //카드에 번호 부여
        theCardObj.GetComponent<CardUI>().theNumber = count;

    }

    //////////////////////////////////////////////////////////////3개를 RandomCard에 맞게 선택

    public void ResultSelect()
    {


        for (int i = 0; i < 3; i++)
        {
            //가중치 랜덤을 돌리면서 결과 리스트에 넣음
            RandomCardSelectresult.Add(RandomCard());
            //비어 있는 카드를 생성하고
            // CardUI cardUI = Instantiate(cardprefab, parent).GetComponent<CardUI>();
            //생성 된 카드에 결과 리스트의 정보를 넣음
            // cardUI.CardUISet(result[i]);
        }
    }

    //////////////////////////////////////////////////////////////카드 랜덤하게 선택
    public Items RandomCard()
    {
        int weight = 0;
        int selectNum = 0;

        selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));

        print("select " + selectNum);

        for (int i = 0; i < TotalCardList.Count; i++)
        {
            weight += TotalCardList[i].weight;
            if (selectNum <= weight)
            {
                print("num" + i);

                Items temp = TotalCardList[i];
                return temp;
            }
        }
        return null;
    }

    public void NowScreenCardSelectAndDestroy(int number)
    {
        //////////////////////////////////////////Save
        ///
        //인벤토리 창에 포함이 된다.
        inventoryScript.AddItem(RandomCardSelectresult[number]);

        //아이템 효과 발동
        RandomCardSelectresult[number].theItemsEffect();


        //////////////////////////////////////////Destroy
        ///
        //캔버스 삭제
        Destroy(theCanvasObj);

        //리스트 초기화
        RandomCardSelectresult = new List<Items>();



    }

}
