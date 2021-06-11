using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalkSlimePatern : BossPartentScripts
{
    //int 값은 Attack의 넘버
    List<int> AttackPatern_Arrage_1 = new List<int> { 3,1,2 };

    List<int> AttackPatern_List;

    int count = 0;

    protected override void Start()
    {
        BossHp = 100;
        attack_Delay = 1.5f;
        move_Delay = 1f;
        the_Next_Partern_HP_Limit = 30;

        base.Start();

        AttackPatern_List = AttackPatern_Arrage_1;
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
                else if (attack_DistanceLimitToPlayer == true)
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

        //죽음 체크
        Death();
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
            case 1: // 타격 - 왼손
                break;
            case 2: // 타격 - 오른손
                break;
            case 3: // 쏘기 공격
                break;
            default:
                break;
        }

    }

    public override void MyHitpointGot(GameObject obj)
    {
        ////손(잡기)
        //if (obj.name == "Slime_Attack_pos_1")
        //{
        //    //잡았다고 신호 보내기
        //    _theGrab = true;
        //    print("grab");
        //    StartCoroutine(GetoutGrab());
        //}

        //왼손공격
        if (obj.name == "AttackPos_attack1")
        {
            effectController_.InstantiateParticle(1, 1, false);
        }

        //오른손공격
        else if (obj.name == "AttackPos_attack2")
        {
            effectController_.InstantiateParticle(2, 2, false);
        }

    }

}
