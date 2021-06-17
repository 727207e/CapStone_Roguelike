using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPartentScripts : MonoBehaviour, IBoss
{
    //////////////////////////////////////////////
    ///오브젝트 안에 TargetDistance 오브젝트(Sphere Collider_trigger / BossPlayerDistnaceDetected)
    ///오브젝트 안에 Effect 오브젝트(Box collider_trigger)




    /// /////////////////////////////////////////////////////////Interface 정보
    public int BossHp { get; set; }
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

    float _time = 0;
    float _Time_Lerp = 0;

    public GameObject _camera;

    public bool _cameraCoroutineTrigger = false;


    public List<GameObject> CameraMoveingPos; // 인터페이스 값에 넘겨줄 값
    public List<float> CameraMovingTime; // 카메라 위치 변화 딜레이 시간

    protected GameObject canvas_Boss;
    protected GameObject canvas_BossText;
    protected GameObject canvas_Boss_Hp_Bar;

    protected GameObject target;

    Rigidbody rigid;
    Vector3 velocity = Vector3.zero;

    public EffectController effectController_;

    public GameObject hitCollider;


    //다음 패턴 변경시 체력
    public int the_Next_Partern_HP_Limit;
    

    /// ///////////////////////////////////// 추후 변경
    /// 
    public GameObject oldpos;



    protected virtual void Start()
    {
        //공격을 받지않음
        hitCollider.SetActive(false);

        cameraMovingWalk_Camera = CameraMoveingPos;
        _camera = GameObject.Find("Main Camera");

        if (_camera == null) 
        {
            _camera = GameObject.Find("MainCameraWithPostProcessing");
        }


        _animator = transform.Find("Body").GetComponent<Animator>();

        canvas_Boss = _camera.transform.Find("BossCanvas").gameObject;
        canvas_BossText = _camera.transform.Find("BossCanvas").Find("Boss_Text").gameObject;
        canvas_Boss_Hp_Bar = _camera.transform.Find("BossCanvas").Find("Boss_HpBar").gameObject;

        //hp바 와 boss 연결
        canvas_Boss_Hp_Bar.GetComponent<Boss_UI_HpBar>().theBoss = this;
        canvas_Boss_Hp_Bar.GetComponent<Boss_UI_HpBar>().BossHPBarSettint();

        target = GameObject.FindGameObjectWithTag("Player");

        //캐릭터 위치 지정
        Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y,
             target.transform.position.z);

        //바라볼 위치 방향 지정
        velocity = (targetPos - transform.position).normalized;

        //바라보기(타겟 바라보기)
        transform.LookAt(transform.position + velocity); //캐릭터가 방향을 바라본다.

        //effect 스크립트 가져오기
        effectController_ = GetComponent<EffectController>();

        rigid = GetComponent<Rigidbody>();
    }


    /// /////////////////////////////////////////////////////// Interface 함수 정의
    public virtual void ShowTheBoss()
    {
        Vector3 position = Camera.main.transform.position;
        Quaternion rotation = Camera.main.transform.rotation;
        StartCoroutine(BossStageShowCoroutine(position, rotation));
    }

    public virtual void Attack() { }

    public IEnumerator BossStageShowCoroutine(Vector3 position, Quaternion rotation)
    {
        //카메라 이동
        StartCoroutine(CameraMovingCoroutine());

        //카메라 이동 끝날때 까지 붙잡고 있는 방식
        while (!_cameraCoroutineTrigger)
            yield return null;

        //다시 시작하면 나중을 위해 트리거 락
        _cameraCoroutineTrigger = false;

        //케릭터 웃음
        _animator.SetBool("Laugh", true);

        //Hp바 등장씬
        canvas_Boss_Hp_Bar.SetActive(true);
        canvas_Boss_Hp_Bar.GetComponent<Boss_UI_HpBar>().FilltheHpBar();

        //텍스트 등장
        StartCoroutine(TextShowCoroutine());

        //텍스트 끝날때 까지 붙잡고 있는 방식
        while (!_cameraCoroutineTrigger)
            yield return null;

        //다시 시작하면 나중을 위해 트리거 락
        //_cameraCoroutineTrigger = false;

        //등장 애니메이션 종료

        canvas_BossText.SetActive(false);
        _animator.SetBool("Laugh", false);
        _animation_Appear = true;
        hitCollider.SetActive(true);
        ////////////////////////////////////////////////////////추후 변경
        ///
        
        _camera.transform.position = position;
        _camera.transform.rotation = rotation;
        
        /*
        _camera.transform.position = oldpos.transform.position;
        _camera.transform.rotation = oldpos.transform.rotation;
        */
    }

    public IEnumerator TextShowCoroutine()
    {
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

        yield return new WaitForSeconds(CameraMovingTime[CameraMovingTime.Count - 1]);


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

        _cameraCoroutineTrigger = true;
    }

    public IEnumerator CameraMovingCoroutine()
    {
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
        _cameraCoroutineTrigger = true;
    }

    public void BossMove()
    {
        //플레이어(target이 있는 경우)
        if(target != null)
        {
            Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y,
                target.transform.position.z);

            velocity = (targetPos - transform.position).normalized;

            transform.LookAt(transform.position + velocity); //캐릭터가 방향을 바라본다.

            transform.Translate(velocity * m_moveSpeed * Time.deltaTime, Space.World);

            _animator.SetBool("Walk", true);
        }
    }

    public virtual void MyHitpointGot(GameObject obj)
    {

    }


    public void AnimationMoveEnd()
    {
        //애니메이션 종료시 초기화
        _animator.SetInteger("Attack_Num", 0);
        move_Countdown = true;
    }

    public virtual void AttackSpecial(int attackNumber) { }
    /// /////////////////////////////////////////////////////////보스 패턴 변경 함수
    protected void PaternHpCheck()
    {
        //채력이 채력제한보다 아래이면 다음 패턴
        if(BossHp <= the_Next_Partern_HP_Limit)
        {
            PaternChange();
        }
    }

    protected virtual void PaternChange()
    {
        //이동 멈춤
        _animation_Appear = false;
    }

    protected virtual bool Death(bool live, GameObject crystal, Vector3 position)
    {
        if(BossHp <= 0 && live)
        {
            _animator.SetTrigger("Death");
            Debug.Log("보스킬");
            mapscript.instance.monster_count--;
            live = false;

            Instantiate(crystal, position, Quaternion.identity);
        }

        return live;
    }

}
