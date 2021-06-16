using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerDownHandler
{
    public RectTransform Canvas;
    public Image chr;
    Animator animator;

    CardGenerator cardGenerator;
    InventoryScript inventoryScript;

    //할당받을 넘버
    public int theNumber;


    private void Start()
    {
        SelectCard();
        cardGenerator = GameObject.Find("CardGenerator").GetComponent<CardGenerator>();
        inventoryScript = GameManager.Instance.player.transform.Find("CharacterCanvas").
            transform.Find("체력바,하단스킬창,아템창").transform.Find("Inventory")
            .GetComponent<InventoryScript>();
    }

    public void SelectCard()
    {
        animator = GetComponent<Animator>();
    }


    // 카드의 정보를 초기화
    


    public void CardUISet(Card card)
    {
        chr.sprite = card.cardImage;
    }
    // 카드가 클릭되면 뒤집는 애니메이션 재생
    public void OnPointerDown(PointerEventData eventData)
    {
        if(animator.GetBool("Flip") == true)
        {
            //선택된 카드 저장 및 카드 전체 삭제
            cardGenerator.NowScreenCardSelectAndDestroy(theNumber);
        }

        animator.SetBool("Flip", true);


    }
}









/*
//////////////////////////////////////////////////////////////////////////

0. Scene은 Scene / JsonTestScene / CardItemTestScene 입니다.
    
            Scene 실행후 K를 누르면 카드 3장 생성되고, (지금 가중치가 전부 0이라서 0번째만 등장)
                선택하는 방식.
            이때, I를 눌러스 Bag(인벤토리)를 생성해 주어야 Bag에 포함됩니다.
            지금은 UIManager가 없어서 toolTip은 안뜹니다 <- 이부분은 제가 안건드렸습니다.

//////////////////////////////////////////////////////////////////////////

1. 하이어라키에 필요한 오브젝트

CardGenerator 오브젝트
    -> CardGenerator 스크립트 포함 ( Total Card List에 Item리스트 넣을것)
    -> Prefabs_Card_Obj 에 Card_Prefabs 넣을것(프리팹)
    -> Canvas_Card 에 Canvas_Card 넣을것(프리팹)
    -> Camera에 MainCamera 넣을 것
    -> InventoryScript에 하이어라키에 있는 Inventory 넣을것.

Canvas Inventory 오브젝트
    -> 변경사항 없음

//////////////////////////////////////////////////////////////////////////

2. 클래스 설명

 CardGenerator 클래스 - sumthepercent() <- 확률 계산(전체 Weight계산 및 랜덤 추출)
                        - ResultSelect() <- 3장 추출 및 RandomCardSelectResult(List) 에 포함시킴.
                        - GeneratorCard(int count) <- 카드 생성(프리팹), 넘버 부여(몇번째 카드인지)
                        - NowScreenCardSelectAndDestroy(int number) 
                                            <- 선택된 카드 인벤토리 창에 포함 및 효과 발동
                                            <- 캔버스와 리스트 삭제


 앞으로 사용될 유물 클라스(ex. APTan. ArmorReinForce 등등)
                        - theItemsEffect() <- override를 통해 효과 부여
                                            <- 사용할 효과(공격력 증가 및 체력증가 등)은 items가 보유.(부모스크립트)


//////////////////////////////////////////////////////////////////////////
 
 3.현재 문제점


Scene이동시 Inventory에 넣은 Asset들은 삭제되지 않는가?
            - 불확실합니다. 써 본적이 없는 스크립트 형태라서 잘 모르겠습니다.
                    테스트도 아직 씬 연결이 되어야 진행할수 있을것 같습니다.

Card에 이미지는 어떻게 넣는가?
            - 그것도 제가 만든 UI가 아니라서 잘 모르겠습니다.
                그냥 Asset에 Icon 넣는 부분있어서 넣었을 뿐이라서 이건 잘 모르겠습니다.
 
Scene이동시에 보유한 유물들을 다시 활성화 되는가?
            - 이것도 위에 1번문제처럼 OnSceneLoaded 로 체인 걸어놓은 정보들을 써서
                확인을 나중에 해봐야 할것 같습니다. 씬 연결이 되어야 진행될것 같습니다.
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 */