using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterRangerAttack : MonoBehaviour
{
    public GameObject bullet;               // 발사체
    public GameObject fire_Pos;             // 발사지점
    public GameObject target;               // 타겟
    public float distance;                  // 타겟과의 거리

    // 이동관련
    public float sight_Range = 20.0f;       // 타겟 인지범위
    public float timer = 0.0f;              // 단순 타이머
    public bool patrol_State = true;        // 순찰 상태
    public bool detected = false;           // 타겟 감지
    public float patrol_min = 5.0f;         // 패트롤 최소 이동거리
    public float patrol_max = 10.0f;        // 패트롤 최대 이동거리
    public int left_right_idle = 4;

    private Vector3 moveto_Position;        // 이동방향 설정용
    public Vector3 direction;              // 이동 방향으로 고개돌리기

    // 공격관련
    public float attack_Delay = 0.0f;       // 공격 딜레이
    public float attack_Speed = 3.0f;       // 공격속도(고정값)
    public int attack_state = 0;            // 공격상태 0: 공격 불가 1: 공격 가능 2: 공격 차징
    public float atk_timer = 0.0f;

    // 애니메이션
    private Animator animator;              // 애니메이터 불러오기

    private int stage_position;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        stage_position = mapscript.instance.stage_Position;

        target = GameObject.FindGameObjectWithTag("Player");
        distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance > sight_Range)
            patrol();
        else if (distance <= sight_Range)
            detect_target();

        //attack();

        fire_Pos.transform.LookAt(target.transform);
    }

    private void attack()
    {
        if (attack_Delay >= attack_Speed && distance <= sight_Range)
        {
            animator.SetTrigger("Attack_R");

            Instantiate(bullet, fire_Pos.transform.position, fire_Pos.transform.rotation).transform.SetParent(mapscript.instance.map_List[stage_position].transform);

            attack_Delay = 0.0f;
        }
        else
            attack_Delay += Time.deltaTime;
    }
    public void patrol()
    {
        if (detected)
            timer = 0;
        else
            timer += Time.deltaTime;

        float moving_direction = 0;

        if (patrol_State)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Run", false);

            if (left_right_idle <= 1)               // 좌
                moving_direction = Random.Range(transform.position.x - patrol_max, transform.position.x - patrol_min);
            else if (left_right_idle <= 3)          // 우
                moving_direction = Random.Range(transform.position.x + patrol_min, transform.position.x + patrol_max);
            else                                    // 정지
            {
                moving_direction = transform.position.x;
                animator.SetBool("Idle", true);
            }

            if (timer >= 2.0f)
            {
                moveto_Position = new Vector3(moving_direction, transform.position.y, transform.position.z);
                patrol_State = false;
                timer = 0.0f;
            }
        }
        else
        {
            if (timer < 2.0f)
            {
                if ((left_right_idle <= 1 && moveto_Position.x < transform.position.x) || (left_right_idle <= 3 && left_right_idle > 1 && moveto_Position.x > transform.position.x) || left_right_idle == 4)
                {
                    direction = (moveto_Position - transform.position).normalized;
                    transform.LookAt(transform.position + direction);
                    transform.Translate(direction * 1.0f * Time.deltaTime, Space.World);
                }
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
        if (attack_state == 1)
        {
            animator.SetTrigger("Attack_R");
            attack_state = 2;

        }

        else if (attack_state == 2)
        {
            atk_timer += Time.deltaTime;
            if (atk_timer >= 1.5f)
            {
                Instantiate(bullet, fire_Pos.transform.position, fire_Pos.transform.rotation).transform.SetParent(mapscript.instance.map_List[stage_position].transform);
                atk_timer = 0.0f;
                attack_state = 0;
            }
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

                transform.Translate(direction * 2.0f * Time.deltaTime, Space.World);
            }

            else
            {
                //시간 초기화 및 공격활성화
                attack_Delay = 0;
                attack_state = 1;
            }
        }
    }
}
