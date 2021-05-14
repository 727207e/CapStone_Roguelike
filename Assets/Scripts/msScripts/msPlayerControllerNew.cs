using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msPlayerControllerNew : MonoBehaviour
{
    private bool characterMoveMode = false; //캐릭터가 2D로 움직일 건지 3D로 움직일건지

    //캐릭터가 가지는 스탯
    public float walkSpeed = 2.5f; //이동속도 기본 10.0f
    public float jumpHeight = 5f; //점프 높이 기본 10.0f
    public float acceleration = 1.2f; //가속력 기본 1.2f
    public float dashAcceleration = 500f;
    public float toughness = 0f; //강인함. 넉백에 관여함. 수치가 높을 수록 넉백이 줄어든다.

    public float initHealthPoint = 1000f; //플레이어가 가진 최대 체력을 의미. 이것을 통해 계산하는 함수가 많아서 만듬
    public float initAbilityPoint = 100f; //플레이어가 가진 최대 기력을 의미. 위와 동일
    public float healthPoint; //캐릭터 체력 기본 1000f. 이 수치는 플레이어의 게임 내에서의 현재 체력을 의미함.
    public float AbilityPoint; //위와 동일.

    private bool isInvincibleTime = false; //무적인지 판정함


    public enum Weapon { OneHandGun, MachineGun, LaserGun, None }; //총기의 종류. 시작시 지정해주고 이것을 gunManager한테 넘겨줌으로써 총기 생성
    public Weapon currentWeapon;

    //땅과 접촉하는 것을 검사하는 것과 관련된 속성
    public Transform groundCheckTransform;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded;

    public Transform targetTransform;
    public LayerMask mouseAimMask;
    public LayerMask groundMask;

    //애니메이터, 카메라, 리지드 바디 관련
    //private float inputMovement;
    private Animator animator;
    private Rigidbody rbody;
    private Camera mainCamera;

    private int FacingSign
    {
        get
        {
            Vector3 perp = Vector3.Cross(transform.forward, Vector3.forward);
            float dir = Vector3.Dot(perp, transform.up);
            return dir > 0f ? -1 : dir < 0f ? 1 : 0;
        }
    }


     void Awake()
    {
        //모든 함수들 중 가장 먼저 처리되어야하는 경우 이쪽에 등록.
        //여기에 있는 코드를 Start에 넣을 경우 다른 함수가 먼저 처리되는 경우가 발생할 수 있음.
        currentWeapon = Weapon.MachineGun;
        Debug.Log(currentWeapon);
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        healthPoint = initHealthPoint;
        AbilityPoint = initAbilityPoint;

    }

    // Update is called once per frame
    void Update()
    {
        //inputMovement = Input.GetAxis("Horizontal");

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask))
        {
            targetTransform.position = hit.point;
        }

        //점프 기능을 수행
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rbody.velocity = new Vector3(rbody.velocity.x, 0, 0);
            rbody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight *-1* Physics.gravity.y),ForceMode.VelocityChange);
        }

        PlayerDied();
        DebugPlayer();
        PlayerDash();
    }

    private void initPlayerStat()
    {
        //플레이어 생성시 만들어지는 플레이어 초기화 함수이다.
        //
    }

    //이동함수
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
            transform.LookAt(transform.position + velocity);
        }
        else
        {
            Vector3 moveHorizontal = Vector3.right * h;
            velocity = (moveHorizontal).normalized;
        }

        if (Input.GetKey(KeyCode.LeftShift)) //캐릭터가 시프트를 누르면 가속한다.
        {
            velocity *= acceleration;
        }
        transform.Translate(velocity * walkSpeed * Time.deltaTime, Space.World);
        //m_rigidBody.MovePosition(transform.position + velocity * m_moveSpeed * Time.deltaTime);

        animator.SetFloat("Speed", velocity.magnitude);
    }

    //움직임, 회전, 그라운드 체크를 수행함.
    private void FixedUpdate()
    {
        //움직이기 - 영상에서 사용한 경우. 이 경우 3d 이동이 불가능하기 때문에 자체적으로 만든 함수를 사용
        //rbody.velocity = new Vector3(inputMovement * walkSpeed, rbody.velocity.y, 0);
        //animator.SetFloat("Speed", FacingSign * rbody.velocity.x / walkSpeed);
        PlayerMove();

        //플레이어 회전
        rbody.MoveRotation(Quaternion.Euler(new Vector3(0, 90 * Mathf.Sign(targetTransform.position.x - transform.position.x), 0)));

        //그라운드 체크
        isGrounded = Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, groundMask, QueryTriggerInteraction.Ignore);
        animator.SetBool("isGrounded", isGrounded);
    }

    //IK 애니메이션 관련 함수 - 이후 삭제 예정
    private void OnAnimatorIK()
    {
        /*
        // Weapon Aim at Target IK
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, targetTransform.position);

        // Look at target IK
        animator.SetLookAtWeight(1);
        animator.SetLookAtPosition(targetTransform.position);
        */
    }

    private void PlayerDied()
    {
        //플레이어가 체력이 0이되거나, 플레이어의 y축 가속도가 -50을 넘어가면 사망함.
        //이후 
        if (healthPoint <= 0 || rbody.velocity.y < -100)
        {
            healthPoint = 0;
            Destroy(gameObject);
            Debug.Log("플레이어가 사망하였습니다.");
        }
    }

    //쉬운 디버깅을 위한 함수
    private void DebugPlayer()
    {
        //T를 누르면 플레이어를 강제로 사망처리함
        if (Input.GetKeyDown(KeyCode.T))
        {
            healthPoint = 0;
            Debug.Log("디버깅 1 : 플레이어 강제 사망");
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {

        }

        //Q를 누르면 3D 움직임을 수행함.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (characterMoveMode == true)
            {
                characterMoveMode = false;
                Debug.Log("디버깅 3 : 3D모드 OFF");
            }
            else
            {
                characterMoveMode = true;
                Debug.Log("디버깅 3 : 3D모드 ON");
            }
            
        }
    }

    //플레이어가 데미지를 입었을 경우의 함수, 넉백을 주고 라이프를 깎는다.
    public void PlayerDamaged(float damage)
    {
        healthPoint -= damage;
        rbody.AddForce(new Vector3(-1, 0.5f, 0) * (500f-toughness)); //넉백의 구현
    }

    public void PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rbody.AddForce(new Vector3(1 * Mathf.Sign(targetTransform.position.x - transform.position.x), 0, 0) * dashAcceleration);
        }
    }

    //플레이어와 충돌한 오브젝트와 반응을 수행한다.
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAttack"))
        {
            //여기서 적 개체 발사한 오브젝트의 함수를 사용한다.
            Debug.Log("공격받았습니다.");
            PlayerDamaged(100);
            Destroy(other.gameObject);
            isInvincibleTime = true;
            StartCoroutine("InvincibleTime");
        }
        
    }

    public  IEnumerator InvincibleTime() {
        int countTime = 0;

        while(countTime < 10)
        {
            if (countTime % 2 == 0)
            {

            }
            else
            {

            }

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }

        isInvincibleTime = false;
        Debug.Log("무적 시간 종료");
        //무적시간 종료

        yield return null;
    }

}
