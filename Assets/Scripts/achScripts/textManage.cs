using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textManage : MonoBehaviour
{
    // Start is called before the first frame update
    //텍스트필드 할당
    public Text chatText;

    //출력할 문구 기본선언
    public string writer = "";
    void Start()
    {
        //실행
        StartCoroutine(Textprint());
    }

    //문구 타이핑 효과
    IEnumerator NormalChat(string narration)
    {
        int a = 0;
        writer = "";

        for (a = 0; a < narration.Length; a++)
        {
            writer += narration[a];
            chatText.text = writer;
            yield return null;
        }

        //키 입력 할때까지 대기
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                break;
            }
            yield return null;
        }
    }

    //실행할 문구
    IEnumerator Textprint()
    {
        yield return StartCoroutine(NormalChat("가나다라마바사아자차카타파하"));
        yield return StartCoroutine(NormalChat("ㅏㅑㅓㅕㅗㅛㅜㅠㅡㅣ"));
    }
}
