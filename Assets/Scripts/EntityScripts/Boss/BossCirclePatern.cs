using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCirclePatern : BossPartentScripts
{
    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////
    /// 
    /// 
    /// 데미지를 주는 부분을 포함해야 합니다
    /// 
    /// </summary>

    //int 값은 Attack의 넘버
    List<int> AttackPatern_Arrage = new List<int>{ 1, 2, 2 , 1, 3, 4 };

    int count = 0;

    protected override void Start()
    {
        BossHp = 100;
        BossAttackPower = 50;
        attack_Delay = 1.5f;
        move_Delay = 1f;

        base.Start();

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

                /////////////////////////////////////////공격
                if (attack_CanAttack && attack_DistanceLimitToPlayer)
                {
                    Attack();

                    //초기화
                    attack_nowTime = 0;
                    attack_CanAttack = false;
                    move_CanMove = false;
                }

                /////////////////////////////////////////공격 사거리에 들어오지 않는다면
                if (attack_DistanceLimitToPlayer == false)
                {
                    //target을 향해 간다
                    BossMove();
                }

                /////////////////////////////////////////딜레이
                if (attack_CanAttack == false)
                {
                    attack_nowTime += Time.deltaTime;
                }
            }




            //보스 움직임 불가능
            if(move_Countdown)
            {
                //이동 가능 상태로 변경
                if(move_nowTime >= move_Delay)
                {
                    move_CanMove = true;
                    move_Countdown = false;
                }

                //딜레이
                if(move_CanMove == false)
                {
                    move_nowTime += Time.deltaTime;
                }
            }



        }

    }

    public override void Attack()
    {

        //애니메이션 변경
        _animator.SetInteger("Attack_Num", AttackPatern_Arrage[count]);

        //다음 패턴 추가
        count++;

        if (AttackPatern_Arrage.Count == count)
            count = 0;

    }

    public void AnimationMoveEnd()
    {
        //애니메이션 종료시 초기화
        _animator.SetInteger("Attack_Num", 0);
        move_Countdown = true;
    }
}
