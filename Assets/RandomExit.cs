using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomExit : MonoBehaviour
{

    public RectTransform Canvas;


    public void Exit()
    {
        Canvas.anchoredPosition = Vector3.down * 1000;  // 화면아래로 위치시킨다.
    }
}
