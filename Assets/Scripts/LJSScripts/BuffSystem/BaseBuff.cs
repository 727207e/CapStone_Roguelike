using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseBuff : MonoBehaviour
{
    public string type; // 문자열 변수타입
    public float percentage; // 변화 정도
    public float duration; // 타이머
    public float currentTime; 
    public Image icon; // 타이머 이미지

    private void Awake()
    {
        icon = GetComponent<Image>();
    }

    public void Init(string type, float per, float du) // 초기화
    {
        this.type = type;
        percentage = per;
        duration = du;
        currentTime = duration;  // 변수초기화
        icon.fillAmount = 1;

        // Execute(); 

    }
    WaitForSeconds seconds = new WaitForSeconds(0.1f);
    public void Execute() //변수적용, 타이머
    {
        PlayerData.instanace.onBuff.Add(this); // 온버프 리스트에 자기 자신을 추가
        PlayerData.instanace.ChooseBuff(type); // ChooseBuff 호출
        StartCoroutine(Activation());
    }

    IEnumerator Activation()  // 타이머코루틴
    {
        while (currentTime > 0)
        {
            currentTime -= 0.1f; // 0.1초마다 0.1씩 currentTime을 깎는다
            icon.fillAmount = currentTime / duration; 
            yield return seconds;
        }
        icon.fillAmount = 0;
        currentTime = 0;
        DeActivation();
    }

    public void DeActivation() // 지속시간이 끝나면 버프제거
    {
        PlayerData.instanace.onBuff.Remove(this); // 온버프 리스트에 자기 자신을 추가
        PlayerData.instanace.ChooseBuff(type); // ChooseBuff 호출

        Destroy(gameObject);
    }
}
