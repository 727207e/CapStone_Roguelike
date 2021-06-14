using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    ///<summary>
    /// 드래그 바에 DragWindow 스크립트를 추가하고
    /// window 변수에 움직이고 싶은 창을 인스펙터에 드래그 한다.
    ///</summary>

    public RectTransform window; //Drag Move Window
    private Vector2 downPosition;

    public void OnPointerDown(PointerEventData data)
    {
        downPosition = data.position;
    }

    public void OnDrag(PointerEventData data)
    {
        Vector2 offset = data.position - downPosition;
        downPosition = data.position;

        window.anchoredPosition += offset;
    }
}