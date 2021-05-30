using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msPlayerController : MonoBehaviour
{
    private Animator m_animator; //자기 자신의 애니메이터를 받아옴
    private Rigidbody m_rigidBody; //자기 자신의 리지드 바디
    private bool m_wasGrounded;
    private bool m_isGrounded;
    private List<Collider> m_collisions = new List<Collider>(); //충돌 검사용 리스트
    private Transform upperBody; //상체만 컨트롤하기 위함.
    public GameObject shotPoint; //총구가 나가는 곳. 이후 아예 총이 역할을 수행하도록 하는 방법도 고려
    

    //문제점 현재 반드시 벨로시티가 0~1 사이로 정규화되므로, 이동속도 증가등의 상태 변화를 할 수없음.
    public float m_moveSpeed = 5.0f; //이동속도
    public float m_jumpForce = 5.0f; //점프력
    private float m_jumpTimeStamp = 0; 
    private float m_minJumpInterval = 0.25f; //최소 점프 시간

    private bool characterMoveMode = true; //캐릭터가 2D로 움직일 건지 3D로 움직일건지

    private Vector3 MousePosition; //마우스의 위치
    public Vector3 aimDirection; //캐릭터가 실제로 바라보는 방향. z는 항상 0이다.

    bool aimMode = false; //테스트용도
    public Vector3 relativeVec;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        upperBody = m_animator.GetBoneTransform(HumanBodyBones.Spine);
    }

    void Update()
    {
        m_animator.SetBool("Grounded", m_isGrounded);

        PlayerMove();
        JumpingAndLanding();

        m_wasGrounded = m_isGrounded;

        MousePosition = Input.mousePosition;
        aimDirection = new Vector3(MousePosition.x/100, MousePosition.y/50, 0); //기존 마우스 커서의 위치에서 z를 제외하고 넣어줌.

        Attack();
    }

    private void PlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 velocity = new Vector3(0, 0, 0);


        if (characterMoveMode == true)//true면 z축 이동이 가능해진다.
        {
            Vector3 moveHorizontal = Vector3.right * h;
            Vector3 moveVertical = Vector3.forward * v;
            velocity = (moveHorizontal + moveVertical).normalized;
        }
        else
        {
            Vector3 moveHorizontal = Vector3.right * h;
            velocity = (moveHorizontal).normalized;
        }

        transform.LookAt(transform.position + velocity); //캐릭터가 방향을 바라본다.
        //upperBody.LookAt(aimDirection); //캐릭터가 방향을 바라본다.
        //upperBody.rotation = upperBody.rotation * Quaternion.Euler(0, 0, 1);
  

        if (Input.GetKey(KeyCode.LeftShift)) //캐릭터가 시프트를 누르면 가속한다.
        {
            velocity *= 2.0f;
        }
        transform.Translate(velocity * m_moveSpeed * Time.deltaTime, Space.World);
        //m_rigidBody.MovePosition(transform.position + velocity * m_moveSpeed * Time.deltaTime);

        m_animator.SetFloat("MoveSpeed", velocity.magnitude);
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && Input.GetKey(KeyCode.Space))
        {
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
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
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
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
        if (Input.GetMouseButtonDown(0))
        {
            m_animator.SetTrigger("Attack");
            Debug.Log("사용자가 공격을 수행했습니다.");
        }
        
        
    }
   
    public void OnHit(int Damage)
    {
        //Damage만큼의 피해를 입는다
        GetComponent<CharacterStats>().maxHealth -= Damage;
        print(Damage + transform.name);
    }

}
