using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SlotScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 슬롯에 등록된 아이템 리스트
    // 중첩개수가 2개 이상인 아이템이 있을 수 있다.

    private ObservableStack<Items> items = new ObservableStack<Items>();
    // 아이템의 아이콘
    [SerializeField]
    private Image icon;

    // 빈 슬롯 여부
    public bool IsEmpty
    {
        get { return items.Count == 0; }
    }

    public ObservableStack<Items> MyItems
    {
        get
        {
            return items;
        }
    }

    public Items MyItem
    {
        get
        {
            if (!IsEmpty)
            {
                return MyItems.Peek();
            }

            return null;
        }
    }


    // 슬롯에 아이템 추가.
    public bool AddItem(Items item)
    {
        MyItems.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        return true;
    }

    // 마우스 커서가 Slot 영역 안으로 들어오면 호출
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsEmpty)
        {
            UIManager.MyInstance.ShowTooltip(transform.position, MyItem);
        }
    }



    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}

