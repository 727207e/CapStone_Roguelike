using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Animator m_animator; //자기 자신의 애니메이터를 받아옴
    private Rigidbody m_rigidBody; //자기 자신의 리지드 바디
    private bool m_wasGrounded;
    private bool m_isGrounded;
    private List<Collider> m_collisions = new List<Collider>(); //충돌 검사용 리스트

    //문제점 현재 반드시 벨로시티가 0~1 사이로 정규화되므로, 이동속도 증가등의 상태 변화를 할 수없음.
    public float m_moveSpeed = 5.0f; //이동속도
    public float m_jumpForce = 5.0f; //점프력
    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f; //최소 점프 시간

    private bool characterMoveMode = false; //캐릭터가 2D로 움직일 건지 3D로 움직일건지

    //캐릭터 이동 방향 벡터
    Vector3 velocity;

    //////////////////////////////////////////////////캐릭터 AI - 자동 이동(범위이동)

    //지정된 범위로 움직인다.
    public float AIMoveAround_MaxRange;        //움직임 범위를 조절한다(Range값은 좌우 모두 설정됨)
    public float AIMoveAround_MinRange;        //최소 최대  설정
    int Select_Left_Right;                     //캐릭터가 좌우로 가는지 확인하는 변수

    public float AIMoveAround_routinTime;         //다음 움직임까지의 시간

    float AIMoveAround_Time;         //다음 움직임까지의 시간의 흐름
    bool AIMoveAround_DestinationArive = true;         //지정 위치로 도착
    Vector3 AIMoveAround_MoveDestination = Vector3.zero; //AI가 이동할 위치
    float Select_RandomPoint;       //캐릭터가 좌우로 가는지 체크


    //////////////////////////////////////////////////캐릭터 타겟 락

    //타겟
    public GameObject target = null;

    //공격
    float AIAttack_Time;                         //다음 공격까지 시간의 흐름
    public float AIAttack_routinTime;                   //공격 쿨타임
    public bool AIAttack_CanAttack = true;                                //공격 가능 상태

    public float AIAttack_distance;                     //공격 사거리


    //////////////////////////////////////////////////캐릭터 타겟 언락

    //일정 거리 이후 멀어짐
    public float AIUnLock_Distance;




    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        //타겟을 찾았다면
        if(target != null)
        {
            goToTarget();

            //유저가 일정 거리 떨어지면 타겟 해제
            isPlayerTooFar();
        }

        //타겟이 없다면
        else if(target == null)
        {
            goAround();
        }


        //땅 위
        m_wasGrounded = m_isGrounded;
        m_animator.SetBool("Grounded", m_isGrounded);

        //애니메이션 갱신 이후 초기화
        m_animator.SetFloat("MoveSpeed", velocity.magnitude);
        velocity = Vector3.zero;
    }



    void goToTarget()
    {
        //공격가능 형태
        if (AIAttack_CanAttack)
        {
            Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
            //방향설정
            velocity = (targetPos - transform.position).normalized;

            transform.LookAt(transform.position + velocity); //캐릭터가 방향을 바라본다.

            transform.Translate(velocity * m_moveSpeed * Time.deltaTime, Space.World);
        }

        //공격 쿨타임
        else
        {
            //시간을 채움
            if (AIAttack_Time < AIAttack_routinTime)
            {
                AIAttack_Time += Time.deltaTime;
            }

            else
            {
                //시간 초기화 및 공격활성화
                AIAttack_Time = 0;
                AIAttack_CanAttack = true;
            }
        }
    }

    void isPlayerTooFar()
    {
        //거리가 멀어지면 타겟을 푼다
        if (Vector3.Distance(target.transform.position, transform.position) > AIUnLock_Distance)
        {
            target = null;
        }
    }

    void goAround()
    {

        m_animator.SetBool("Grounded", m_isGrounded);

        //도착을 한 상태일때
        if (AIMoveAround_DestinationArive)
        {
            //이동할 시간이 되었다면 다음 이동지를 설정한다
            if (AIMoveAround_Time >= AIMoveAround_routinTime)
            {
                //캐릭터가 이동할 위치 설정
                Select_Left_Right = Random.Range(0, 2);
                if(Select_Left_Right == 0) //좌
                    Select_RandomPoint = Random.Range(transform.position.x - AIMoveAround_MaxRange,
    transform.position.x - AIMoveAround_MinRange);

                else //우
                    Select_RandomPoint = Random.Range(transform.position.x + AIMoveAround_MinRange,
transform.position.x + AIMoveAround_MaxRange);

                AIMoveAround_MoveDestination = new Vector3(Select_RandomPoint, transform.position.y, transform.position.z);

                //이동가능한 상태로 변경
                AIMoveAround_DestinationArive = false;

                AIMoveAround_Time = 0; // 시간 초기화

            }


            //시간이 다 되지 않았다면 시간을 기다린다.
            else if (AIMoveAround_Time < AIMoveAround_routinTime)
            {
                AIMoveAround_Time += Time.deltaTime;
            }
        }

        //이동
        else
        {
            //이동 가능 상태(이동범위 내부)
            if((Select_Left_Right == 0 && AIMoveAround_MoveDestination.x < transform.position.x)
                || (Select_Left_Right == 1 && AIMoveAround_MoveDestination.x > transform.position.x))
            {
                //방향설정
                velocity = (AIMoveAround_MoveDestination - transform.position).normalized;

                transform.LookAt(transform.position + velocity); //캐릭터가 방향을 바라본다.

                transform.Translate(velocity * m_moveSpeed * Time.deltaTime, Space.World);

            }

            //이동범위를 넘어간 경우 -> 도착상태
            else
            {
                AIMoveAround_DestinationArive = true;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
                m_animator.SetTrigger("Land");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) {
            m_isGrounded = false;
            m_animator.SetTrigger("Jump");
         }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }

    public void Attack()
    {
        //공격함수에서 대미지를 주는 건 무기에게 달린 collider가 준다.
        m_animator.SetTrigger("Attack");
    }

    public void OnHit(int Damage)
    {
        //Damage만큼의 피해를 입는다
        GetComponent<CharacterStats>().maxHealth -= Damage;
        print(Damage + transform.name);
    }
}
