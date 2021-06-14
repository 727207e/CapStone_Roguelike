using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager MyInstance

    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    [SerializeField]
    private GameObject tooltip;
    private Text tooltipText;

    private void Awake()
    {
        // 아이템 툴팁 참조
        tooltipText = tooltip.GetComponentInChildren<Text>();
    }



    // 튤팁UI 활성화
    public void ShowTooltip(Vector3 position, IDescribable description)
    {
        tooltip.SetActive(true);
        tooltip.transform.position = position;

        // 아이템의 내용을 툴팁게임오브젝트에 전달
        tooltipText.text = description.GetDescription();
    }



    // 튤팁UI 비활성화
    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }

}
