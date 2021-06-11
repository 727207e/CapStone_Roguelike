using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_UI_HpBar : MonoBehaviour
{
    //////////////////////////////UI의 이미지는 화면의 크기를 따라간다.
    int UI_Width;
    int UI_Height;

    public GameObject Hp_BackImage;
    public GameObject Hp_InImage;

    RectTransform Hp_BackImage_Rect;
    RectTransform Hp_InImage_Rect;

    Image Hp_InImage_Filled;

    public BossPartentScripts theBoss;

    int HP_MAX = 100;

    bool Hp_bar_Trigger = false;

    // Start is called before the first frame update
    void Start()
    {
        //화면 크기
        UI_Width = Screen.width;
        UI_Height = Screen.height;

        //사용할 도형
        Hp_BackImage_Rect = Hp_BackImage.GetComponent<RectTransform>();
        Hp_InImage_Rect = Hp_InImage.GetComponent<RectTransform>();

        //이미지 크기를 화면 크기에 맞춘다.
        Hp_BackImage_Rect.sizeDelta = new Vector2(UI_Width, UI_Height / 12);
        Hp_InImage_Rect.sizeDelta = new Vector2(UI_Width, UI_Height / 12);

        //체력바
        Hp_InImage_Filled = Hp_InImage.GetComponent<Image>();

    }

    public void BossHPBarSettint()
    {
        HP_MAX = theBoss.BossHp;
    }

    private void Update()
    {
        if (Hp_bar_Trigger)
        {
            //비율
            Hp_InImage_Filled.fillAmount = (float)theBoss.BossHp / (float)HP_MAX;

        }
    }

    public void FilltheHpBar()
    {
        Start();
        StartCoroutine(theFill());


    }
    IEnumerator theFill()
    {
        //Hp바 채워지는 모션

        float _time = 0;

        while (_time < 1)
        {

            _time += Time.deltaTime;

            Hp_InImage_Filled.fillAmount = Mathf.Lerp(0, 1, _time);
            yield return null;
        }

        Hp_bar_Trigger = true;

        Hp_InImage_Filled.fillAmount = 1;
    }



}
