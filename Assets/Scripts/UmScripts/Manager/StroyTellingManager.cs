using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StroyTellingManager : MonoBehaviour
{
    //스킵 메시지
    public GameObject Skip_message;

    //일정시간이 지나면 사라짐
    float skip_count;
    bool skip_Trigger = false;

    public Material ImageBackGround;
    public List<Texture> images;

    //스토리 설명 텍스트
    public Text Story_Text;



    /// //////////////////////////////////////////////////////////////
    /// Story에 관한 변수들
    /// 
    int Story_nowStory_Num = 0;

    float Story_nextStory_Count = 0;
    float[] Story_nextStory_TimeCountLimit = { 5f, 10f, 10f , 10f , 10f, 10f ,10f };

    //스토리 마지막 번째
    int Story_nowStory_Finish = 5;

    //스토리
    string[] Story_string = { "-21세기 눈부시도록 발전한 과학의 시대-" + "\n"
            + "식량의 부족함을 해소하고, 의학의 발전으로 생명연장까지 이루어진 시대",

        "어느날 갑자기 퍼저버린 <죽음의 바이러스>" + "\n" +
            "어디서 부터 시작인지, 어떻게 피해야하는 지 아무것도 모르던 인류는" + "\n" +
            "갑자기 나타난 <죽음의 바이러스>에 꼼짝없이 당하게 되었다.",

        "많은 사람들이 바이러스에 감염되었고 많은 사람들이 죽어갔다" + "\n" +
            "바이러스의 확산을 막을 방법은 없었고 오직 치료만이 해답이었다" + "\n" +
            "그렇기에 많은 의료진이 치료를 연구하고 또 연구했고 그 과정에서도 많이 죽어갔다",

        "시간이 지나도 바이러스는 줄어들 기미가 보이지않았고" + "\n" +
            "사람들은 바이러스에 지쳐 서로의 이익을 위해 범죄를 저지르기 시작했다",

        "그렇게 황폐해져가는 눈부신 21세기 과학의 시대" + "\n" +
            "이를 막기위해선 치료제를 찾아야 한다",

        "의료진들은 백신개발을 위해 연구했고 연구했지만 결론은 나오지않았고" + "\n" +
            "결국 직접 바이러스를 퇴치하는 <나노로봇>을 개발하기에 이르렀고" + "\n" +
            "이젠 나노로봇을 시험해야 할 차례가 되었다"
    };

    /// //////////////////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        //기본 이미지
        ImageBackGround.SetTexture("_MainTex", images[0]);

        //시작 텍스트
        Story_Text.text = Story_string[Story_nowStory_Num];

        //초기화
        ImageBackGround.SetFloat("_DissolveRange", 0);

    }

    // Update is called once per frame
    void Update()
    {
        StoryTell();

        skipImage();
    }


    void StoryTell()
    {
        //현재 스토리번째 카운트숫자를 넘기면 다음 스토리로 이동
        if(Story_nextStory_Count >= Story_nextStory_TimeCountLimit[Story_nowStory_Num])
        {
            //초기화
            Story_nextStory_Count = 0;
            Story_nowStory_Num++; // 다음 스토리 넘버

            //종료했다면
            if(Story_nowStory_Num > Story_nowStory_Finish)
            {
                //씬 이동
                moveLabScene();
            }

            //이미지 변경
            ImageChange();
        }

        //아직 진행중이라면
        else
        {
            //카운트
            Story_nextStory_Count += Time.deltaTime;

        }
    }

    void skipImage()
    {
        //키 입력시 스킵 버튼 활성화
        if (Input.anyKey)
        {
            skip_Trigger = true;
        }


        if (skip_Trigger)
        {
            //카운트
            skip_count += Time.deltaTime;
            Skip_message.SetActive(true);

            //5초 지남
            if (skip_count > 5f)
            {
                //카운트 초기화 
                skip_count = 0;
                skip_Trigger = false;
                Skip_message.SetActive(false);
            }

            //스킵 후 이동
            if (Input.GetKeyDown(KeyCode.K))
            {
                moveLabScene();
            }
        }   

    }

    void moveLabScene()
    {
        GameManager.Instance.FadeInAndOutAfterFuction += MoveScene_ToLabScene;

        StartCoroutine(GameManager.Instance.fadeIn());
    }


    void MoveScene_ToLabScene()
    {
        GameManager.Instance.MoveScene("3_LabScene");
    }


    void ImageChange()
    {
        //이미지 변경 모션
        StartCoroutine(imageChangeMotion());
    }

    IEnumerator imageChangeMotion()
    {
        float timelimit = 2.5f;
        float timecount = 0f;

        //주어진 시간(timelimit) 보다 작은 카운트 수일 경우
        while(timecount < timelimit)
        {
            timecount += Time.deltaTime;

            //뿌여지는 정도
            ImageBackGround.SetFloat("_DissolveRange", 1.2f * (timecount / timelimit));

            yield return null;
        }

        //이미지변경
        ImageBackGround.SetTexture("_MainTex", images[Story_nowStory_Num]);

        //텍스트 변경
        SetText();

        timecount = 0; // 초기화


        while(timecount < timelimit)
        {
            timecount += Time.deltaTime;

            //뿌여지는 정도
            ImageBackGround.SetFloat("_DissolveRange", 1.2f - 1.2f * (timecount / timelimit));

            yield return null;
        }


    }

    void SetText()
    {
        Story_Text.text = Story_string[Story_nowStory_Num];
    }
}
