using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlimePatern : BossPartentScripts
{
    //int 값은 Attack의 넘버
    List<int> AttackPatern_Arrage_1 = new List<int> { 2, 3, 1 };
    List<int> AttackPatern_Arrage_2 = new List<int> { 4,5,2,3,1 };

    List<int> AttackPatern_List;
    //첫번째 공격 패턴
    int AttackPatern = 1;

    int count = 0;

    public bool _theGrab = false;
    public GameObject _theGrabObj;

    //카메라가 패턴 변경으로 인해 보이게 될 카메라 무빙 위치
    public List<GameObject> CameraMovingPaternPos;

    public GameObject crystal;
    public bool live = true;
    public Vector3 position = Vector3.zero;

    protected override void Start()
    {
        position = GameObject.Find("Crystal_Spawn").transform.position;

        BossHp = 10000;
        attack_Delay = 1.5f;
        move_Delay = 1f;
        the_Next_Partern_HP_Limit = 30;

        base.Start();

        AttackPatern_List = AttackPatern_Arrage_1;
        _animator.SetInteger("AttackPatern", AttackPatern);
    }

    private void Update()
    {
        if (_animation_Appear)
        {
            //보스 움직임 가능
            if (move_CanMove)
            {

                /////////////////////////////////////////공격 가능상태로 변경
                if (attack_nowTime >= attack_Delay)
                {
                    attack_CanAttack = true;
                }

                /////////////////////////////////////////공격 사거리에 들어오지 않는다면
                if (attack_DistanceLimitToPlayer == false)
                {
                    //target을 향해 간다
                    BossMove();
                }

                //사거리에 들어오면 움직임 멈춤
                else if(attack_DistanceLimitToPlayer == true)
                {
                    _animator.SetBool("Walk", false);

                    if (attack_CanAttack)
                    {
                        /////////////////////////////////////////공격
                        if (attack_CanAttack && attack_DistanceLimitToPlayer)
                        {
                            Attack();

                            //초기화
                            attack_nowTime = 0;
                            attack_CanAttack = false;
                            move_CanMove = false;
                        }
                    }
                }

                /////////////////////////////////////////딜레이
                if (attack_CanAttack == false)
                {
                    attack_nowTime += Time.deltaTime;
                }
            }




            //보스 움직임 불가능
            if (move_Countdown)
            {
                //이동 가능 상태로 변경
                if (move_nowTime >= move_Delay)
                {
                    move_CanMove = true;
                    move_Countdown = false;
                }

                //딜레이
                if (move_CanMove == false)
                {
                    move_nowTime += Time.deltaTime;
                }
            }



        }

        //공격패턴이 1번일 경우
        if(AttackPatern == 1)
             //패턴 바뀌는지 계속 체크
             PaternHpCheck();

        //죽음 체크
        if (live)
            live = Death(live, crystal, position);
    }

    public override void Attack()
    {
        base.Attack();

        //애니메이션 변경
        _animator.SetInteger("Attack_Num", AttackPatern_List[count]);

        //다음 패턴 추가
        count++;

        if (AttackPatern_List.Count == count)
            count = 0;

    }


    public override void AttackSpecial(int attackNumber)
    {
        switch (attackNumber)
        {
            case 1: // 잡기
                break;
            case 2: // 입브레스
                break;
            case 3: // 입브레스(쏘기)
                break;
            case 4: //때리기 - 한손
                break;
            case 5: // 때리기 - 두손
                break;
            case 9: //잡은후 깨물기

                if (_theGrab)
                {
                    print("false");
                    effectController_.InstantiateParticle(2,4,false);
                    _theGrab = false;
                }

                break;
            default:
                break;
        }

    }

    public override void MyHitpointGot(GameObject obj)
    {
        //손(잡기)
        if (obj.name == "Slime_Attack_pos_1")
        {
            //잡았다고 신호 보내기
            _theGrab = true;
            print("grab");
            StartCoroutine(GetoutGrab());
        }

        //때리기 한손
        else if(obj.name == "Slime_Attack_pos_2")
        {
            //맞으면 효과
            effectController_.InstantiateParticle(3, 3, true);
        }

        //때리기 두손
        else if(obj.name == "Slime_Attack_pos_3")
        {
            //맞으면 효과
            effectController_.InstantiateParticle(2, 7, true);

        }
    }

    IEnumerator GetoutGrab()
    {
        while (_theGrab)
        {
            target.transform.position = _theGrabObj.transform.position;
            yield return null;
        }
    }

    protected override void PaternChange()
    {
        base.PaternChange();

        //카메라 무빙 변경
        cameraMovingWalk_Camera = CameraMovingPaternPos;

        //다음 패턴 공격
        AttackPatern = 2;
        _animator.SetInteger("AttackPatern", AttackPatern);

        //초기화
        count = 0;

        //리스트 갱신
        AttackPatern_List = AttackPatern_Arrage_2;

        //다음 패턴 변화 카메라 무빙
        StartCoroutine(NextParternCoroutin());
    }

    public IEnumerator NextParternCoroutin()
    {
        //카메라 이동
        StartCoroutine(CameraMovingCoroutine());

        //카메라 이동 끝날때 까지 붙잡고 있는 방식
        while (!_cameraCoroutineTrigger)
            yield return null;

        //다시 시작하면 나중을 위해 트리거 락
        _cameraCoroutineTrigger = false;

        //케릭터 웃음
        yield return new WaitForSeconds(1);

        _animator.SetBool("Laugh", true);

        //오라클 생성
        effectController_.InstantiateParticle(7, 6,true);

        yield return new WaitForSeconds(3);

        canvas_BossText.SetActive(false);
        _animator.SetBool("Laugh", false);
        _animation_Appear = true;


        ////////////////////////////////////////////////////////추후 변경
        ///

        _camera.transform.position = oldpos.transform.position;
        _camera.transform.rotation = oldpos.transform.rotation;
    }
}
