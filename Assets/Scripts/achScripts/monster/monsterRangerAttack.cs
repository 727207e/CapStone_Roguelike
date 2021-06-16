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
    public int clearCount = 0;

    // 애니메이션
    private Animator animator;              // 애니메이터 불러오기

    private MonsterStatus monsterStatus;

    private int stage_position;

    public Vector3 moving_direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        monsterStatus = GetComponent<MonsterStatus>();
        clearCount = monsterStatus.clearCount;

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
        int random_range = 1 + (int)(clearCount/4);
        if (random_range > 4)
            random_range = 4;
        int index = Random.Range(0, random_range);
        // 3방향 발사
        if (index == 1)
        {
            fire_Pos.transform.Rotate(-45, 0, 0);
            Instantiate(bullet, fire_Pos.transform.position, fire_Pos.transform.rotation).transform.SetParent(mapscript.instance.map_List[stage_position].transform);
            fire_Pos.transform.Rotate(90, 0, 0);
            Instantiate(bullet, fire_Pos.transform.position, fire_Pos.transform.rotation).transform.SetParent(mapscript.instance.map_List[stage_position].transform);
            fire_Pos.transform.Rotate(-45, 0, 0);
            Instantiate(bullet, fire_Pos.transform.position, fire_Pos.transform.rotation).transform.SetParent(mapscript.instance.map_List[stage_position].transform);
        }
        // 2방향 발사
        else if (index == 2)
        {
            fire_Pos.transform.Rotate(-15, 0, 0);
            Instantiate(bullet, fire_Pos.transform.position, fire_Pos.transform.rotation).transform.SetParent(mapscript.instance.map_List[stage_position].transform);
            fire_Pos.transform.Rotate(30, 0, 0);
            Instantiate(bullet, fire_Pos.transform.position, fire_Pos.transform.rotation).transform.SetParent(mapscript.instance.map_List[stage_position].transform);
            fire_Pos.transform.Rotate(-15, 0, 0);
        }
        // 5방향 발사
        else if (index == 3)
        {
            fire_Pos.transform.Rotate(-45, 0, 0);
            Instantiate(bullet, fire_Pos.transform.position, fire_Pos.transform.rotation).transform.SetParent(mapscript.instance.map_List[stage_position].transform);
            fire_Pos.transform.Rotate(90, 0, 0);
            Instantiate(bullet, fire_Pos.transform.position, fire_Pos.transform.rotation).transform.SetParent(mapscript.instance.map_List[stage_position].transform);
            fire_Pos.transform.Rotate(-45, 0, 0);
            Instantiate(bullet, fire_Pos.transform.position, fire_Pos.transform.rotation).transform.SetParent(mapscript.instance.map_List[stage_position].transform);
            fire_Pos.transform.Rotate(-90, 0, 0);
            Instantiate(bullet, fire_Pos.transform.position, fire_Pos.transform.rotation).transform.SetParent(mapscript.instance.map_List[stage_position].transform);
            fire_Pos.transform.Rotate(180, 0, 0);
            Instantiate(bullet, fire_Pos.transform.position, fire_Pos.transform.rotation).transform.SetParent(mapscript.instance.map_List[stage_position].transform);
            fire_Pos.transform.Rotate(-90, 0, 0);
        }
        // 그냥 한발쏘기
        else
        {
            GameObject rangeratk = Instantiate(bullet, fire_Pos.transform.position, fire_Pos.transform.rotation);//.transform.SetParent(mapscript.instance.map_List[stage_position].transform);
            rangeratk.transform.SetParent(mapscript.instance.map_List[stage_position].transform);
            enemyBullet enemyBullet = rangeratk.GetComponent<enemyBullet>();
            enemyBullet.damage = monsterStatus.Damage;
        }
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

            if (left_right_idle == 1)               // 상
                moving_direction = Vector3.up;
            else if (left_right_idle == 2)          // 하
                moving_direction = Vector3.down;
            else if (left_right_idle == 3)          // 좌
                moving_direction = Vector3.left;
            else if (left_right_idle == 4)          // 우
                moving_direction = Vector3.right;
            else if (left_right_idle == 5)          // 왼위
                moving_direction = (Vector3.left + Vector3.up).normalized;
            else if (left_right_idle == 6)          // 왼아
                moving_direction = (Vector3.left + Vector3.down).normalized;
            else if (left_right_idle == 7)          // 오위
                moving_direction = (Vector3.right + Vector3.up).normalized;
            else if (left_right_idle == 8)          // 오아
                moving_direction = (Vector3.right + Vector3.down).normalized;
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
                    transform.LookAt(transform.position + moving_direction);
                    transform.Translate(moving_direction * 2.0f * Time.deltaTime, Space.World);
            }
            else
            {
                left_right_idle = Random.Range(0, 9);
                
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
                attack();
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
                    //transform.LookAt(target.transform.position);
                    transform.Translate(direction * 2.0f * Time.deltaTime, Space.World);
                }

                animator.SetBool("Idle", false);
                animator.SetBool("Run", true);

                Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
                //방향설정
                direction = (targetPos - transform.position).normalized;

                transform.LookAt(transform.position + direction);

                if (distance <= 8.0f)
                {

                }
                else
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
