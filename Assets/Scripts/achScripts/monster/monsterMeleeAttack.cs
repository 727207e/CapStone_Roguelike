using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterMeleeAttack : MonoBehaviour
{
    public GameObject target;               // 타겟
    public float distance;                  // 타겟과의 거리

    // 이동관련
    public float sight_Range = 10.0f;       // 타겟 인지범위
    public float timer = 0.0f;              // 단순 타이머
    public bool patrol_State = true;        // 순찰 상태
    public bool detected = false;           // 타겟 감지
    public float patrol_min = 8.0f;         // 패트롤 최소 이동거리
    public float patrol_max = 12.0f;        // 패트롤 최대 이동거리
    public int left_right_idle = 4;

    private Vector3 moveto_Position;        // 이동방향 설정용
    public Vector3 direction;              // 이동 방향으로 고개돌리기
    public Vector3 moving_direction;

    // 공격관련
    public float attack_Delay = 0.0f;       // 공격 딜레이
    public float attack_Speed = 3.0f;       // 공격속도(고정값)
    public bool attack_state = false;

    // 애니메이션
    private Animator animator;              // 애니메이터 불러오기


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance > sight_Range)
            patrol();
        else if (distance <= sight_Range)
            detect_target();
    }

    public void patrol()
    {
        if (detected)
            timer = 0;
        else
            timer += Time.deltaTime;

        if (patrol_State)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Run", false);

            if (left_right_idle <= 1)               // 좌
                moving_direction = Vector3.left;
            else if (left_right_idle <= 3)          // 우
                moving_direction = Vector3.right;
            else                                    // 정지
            {
                moving_direction = Vector3.zero;
                animator.SetBool("Idle", true);
            }

            if (timer >= 2.0f)
            {
                patrol_State = false;
                timer = 0.0f;
            }
        }
        else
        {
            if (timer < 2.0f)
            {
                //if ((left_right_idle <= 1 && moveto_Position.x < transform.position.x) || (left_right_idle <= 3 && left_right_idle > 1 && moveto_Position.x > transform.position.x) || left_right_idle == 4)
                //{
                    //direction = (moveto_Position - transform.position).normalized;
                    transform.LookAt(transform.position + moving_direction);
                    transform.Translate(moving_direction * 1.5f * Time.deltaTime, Space.World);
                //}
            }
            else
            {
                left_right_idle = Random.Range(0, 5);

                patrol_State = true;
                timer = 0.0f;
            }

        }
    }

    public void detect_target()
    {
        // 공격 가능할때
        if (attack_state)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
                animator.SetTrigger("Attack_M");
            else
                animator.SetTrigger("Attack_M2");
            attack_state = false;
        }

        //공격 쿨타임
        else
        {
            if (attack_Delay < attack_Speed)
            {
                attack_Delay += Time.deltaTime;
                if (attack_Delay <= 1.0f)
                {
                    animator.SetBool("Idle", true);
                    direction = Vector3.zero;
                    transform.LookAt(target.transform.position);
                    transform.Translate(direction * 2.0f * Time.deltaTime, Space.World);
                }

                animator.SetBool("Idle", false);
                animator.SetBool("Run", true);

                Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
                //방향설정
                direction = (targetPos - transform.position).normalized;

                transform.LookAt(transform.position + direction);

                if (distance <= 5f)
                {

                }
                else
                    transform.Translate(direction * 2.0f * Time.deltaTime, Space.World);
            }

            else
            {
                //시간 초기화 및 공격활성화
                attack_Delay = 0;
                attack_state = true;
            }
        }
    }
}
