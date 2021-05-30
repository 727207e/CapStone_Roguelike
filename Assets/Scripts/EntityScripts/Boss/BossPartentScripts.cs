using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPartentScripts : MonoBehaviour, IBoss
{
    /// /////////////////////////////////////////////////////////Interface 정보
    public int BossHp { get; set; }
    public int BossAttackPower { get; set; }
    public List<GameObject> cameraMovingWalk_Camera { get; set; }

    /// /////////////////////////////////////////////////////// 애니메이션

    protected Animator _animator;

    protected bool _animation_Appear = false;

    /////////////////////////////////////////////////////////// 공격 패턴
    ///

    protected float attack_Delay;
    protected float attack_nowTime;

    protected bool attack_CanAttack = false;
    public bool attack_DistanceLimitToPlayer = false; // 보스와 플레이어의 거리 조절


    /////////////////////////////////////////////////////////// 이동 패턴

    protected float move_Delay;
    protected float move_nowTime;

    protected bool move_CanMove = true;
    protected bool move_Countdown = false;
    
    public float m_moveSpeed = 5.0f;

    /// /////////////////////////////////////////////////////// 등등

    public GameObject _camera;

    public List<GameObject> CameraMoveingPos; // 인터페이스 값에 넘겨줄 값
    public List<float> CameraMovingTime; // 카메라 위치 변화 딜레이 시간

    protected GameObject canvas_Boss;
    protected GameObject canvas_BossText;

    protected GameObject target;

    Rigidbody rigid;
    Vector3 velocity = Vector3.zero;

    

    /// ///////////////////////////////////// 추후 변경
    /// 
    public GameObject oldpos;



    protected virtual void Start()
    {

        cameraMovingWalk_Camera = CameraMoveingPos;
        _camera = GameObject.Find("Main Camera");
        _animator = GetComponent<Animator>();

        canvas_Boss = _camera.transform.Find("BossCanvas").gameObject;
        canvas_BossText = _camera.transform.Find("BossCanvas").Find("Image").gameObject;

        target = GameObject.FindGameObjectWithTag("Player");

        gameObject.transform.LookAt(target.transform.position);

        rigid = GetComponent<Rigidbody>();
    }


    /// /////////////////////////////////////////////////////// Interface 함수 정의
    public virtual void ShowTheBoss()
    {
        StartCoroutine(CameraMovingCoroutine());
    }

    public virtual void Attack() { }

    IEnumerator CameraMovingCoroutine()
    {
        float _time = 0;
        float _Time_Lerp = 0;


        // 카메라 이동
        for (int i = 0; i < CameraMoveingPos.Count - 1; i++)
        {
            //초기화
            _time = 0;
            _Time_Lerp = 0;


            //while값으로 함수를 못끝내게 붙잡아 둔다.
            while (_time <= CameraMovingTime[i])
            {
                _time += Time.deltaTime;
                _Time_Lerp = _time / CameraMovingTime[i];

                //위치이동 선형보간
                _camera.transform.position =
                    Vector3.Lerp(CameraMoveingPos[i].transform.position,
                               CameraMoveingPos[i + 1].transform.position,
                               _Time_Lerp);

                //회전이동 구선형보간
                _camera.transform.rotation =
                    Quaternion.Slerp(CameraMoveingPos[i].transform.rotation,
                                    CameraMoveingPos[i + 1].transform.rotation,
                                    _Time_Lerp);

                yield return null;
            }
        }
        //카메라 이동 종료


        //케릭터 웃음
        yield return new WaitForSeconds(1);

        _animator.SetBool("Laugh",true);


        //보스 등장 텍스트 생성
        canvas_BossText.SetActive(true);
        _time = 0;

        while (_time <= 1)
        {
            _time += Time.deltaTime;

            float x = Mathf.Lerp(5000, canvas_Boss.transform.position.x, _time);
            canvas_BossText.transform.position =
                new Vector3(x, canvas_BossText.transform.position.y, canvas_BossText.transform.position.z);


            yield return null;
        }

        yield return new WaitForSeconds(1);

        _time = 0;
        _Time_Lerp = 0;

        while (_time <= 1)
        {
            _time += Time.deltaTime;

            float x = Mathf.Lerp(canvas_Boss.transform.position.x, -5000, _time);
            canvas_BossText.transform.position =
                new Vector3(x, canvas_BossText.transform.position.y, canvas_BossText.transform.position.z);

            yield return null;
        }

        //등장 애니메이션 종료

        canvas_BossText.SetActive(false);
        _animator.SetBool("Laugh", false);
        _animation_Appear = true;

        ////////////////////////////////////////////////////////추후 변경
        ///

        _camera.transform.position = oldpos.transform.position;
        _camera.transform.rotation = oldpos.transform.rotation;





    }

    public void BossMove()
    {
        //플레이어(target이 있는 경우)
        if(target != null)
        {
            Vector3 targetPos = target.transform.position;

            velocity = (targetPos - transform.position).normalized;

            transform.LookAt(transform.position + velocity); //캐릭터가 방향을 바라본다.

            transform.Translate(velocity * m_moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
