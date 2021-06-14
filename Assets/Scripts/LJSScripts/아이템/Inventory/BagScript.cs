using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour
{

    [SerializeField]
    private GameObject slotPrefab;

    // 가방의 캔버스 그룹
    private CanvasGroup canvasGroup;

    // 가방 안의 슬롯 리스트
    private List<SlotScript> slots = new List<SlotScript>();


    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }


    // 가방이 열렸는지 확인
    public bool IsOpen
    {
        get
        {
            return canvasGroup.alpha > 0;
        }
    }


    // 가방에 슬롯을 추가한다.
    public void AddSlots(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            SlotScript slot = Instantiate(slotPrefab, transform).GetComponent<SlotScript>();
            slots.Add(slot);
        }
    }


    public void OpenClose()
    {
        // 열려 있으면 닫고
        // 닫혀있으면 연다.

        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;

        // UI 가 터치 이벤트를 무시하냐 안하냐 옵션
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
    }

    public bool AddItem(Items item)
    {
        foreach (SlotScript slot in slots)
        {
            // 빈 슬롯이 있으면
            if (slot.IsEmpty)
            {
                // 해당 슬롯에 아이템을 추가한다.
                slot.AddItem(item);
                return true;
            }
        }

        return false;
    }


}
