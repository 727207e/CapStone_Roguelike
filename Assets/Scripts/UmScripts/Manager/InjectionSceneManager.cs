using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;


public class InjectionSceneManager : MonoBehaviour
{
    //////////////////////////////////////////////////////////화면 변화
    public PostProcessProfile Postprofile;

    LensDistortion _Lens;

    ////////////////////////////////////////////////////////카메라 무빙
    public GameObject Camera;

    public GameObject startPoint;
    public GameObject endpoint;



    // Start is called before the first frame update
    void Start()
    {
        //Postprofile.TryGetSettings(out _Lens);

        //렌즈 초기화
        //_Lens.intensity.value = 0f;
        //_Lens.scale.value = 1f;

        //카메라 위치 초기화
        Camera.transform.position = startPoint.transform.position;


        //StartCoroutine(MakeSmallerIntensityAndScale());

        StartCoroutine(MakeZoominCamera());

    }


    IEnumerator MakeSmallerIntensityAndScale()
    {
        //3초동안 진행된다.
        float timelimit = 3f;
        float timecount = 0f;

        //주어진 시간
        while (timecount < timelimit)
        {
            timecount += Time.deltaTime;

            //멀어진다
            _Lens.intensity.value = -100f * ( timecount / timelimit);
            _Lens.scale.value = 0.01f + (1 - timecount/timelimit);

            yield return null;

        }

        GameManager.Instance.NextSceneFadein("DungeonScene");
    }

    IEnumerator MakeZoominCamera()
    {
        //3초동안 진행된다.
        float timelimit = 3f;
        float timecount = 0f;

        //주어진 시간
        while (timecount < timelimit)
        {
            timecount += Time.deltaTime;

            Vector3 CameraPos = new Vector3(
                 Mathf.Lerp(startPoint.transform.position.x, endpoint.transform.position.x, timecount / timelimit),
                 Mathf.Lerp(startPoint.transform.position.y, endpoint.transform.position.y, timecount / timelimit),
                 Mathf.Lerp(startPoint.transform.position.z, endpoint.transform.position.z, timecount / timelimit)
                );

            //가까워진다
            Camera.transform.position = CameraPos;


            yield return null;

        }
    }
}
