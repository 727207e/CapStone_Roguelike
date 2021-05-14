using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class msPlayerControllerNew : MonoBehaviour
{
    private bool characterMoveMode = false; //캐릭터가 2D로 움직일 건지 3D로 움직일건지

    //캐릭터가 가지는 스탯
    public float walkSpeed = 2.5f; //이동속도 기본 10.0f
    public float jumpHeight = 5f; //점프 높이 기본 10.0f
    public float acceleration = 1.2f; //가속력 기본 1.2f
    public float initHealthPoint = 1000f;
    public float initAbilityPoint = 100f;
    public float healthPoint; //캐릭터 체력 기본 1000f. 
    public float AbilityPoint;
    public Image BloodScreen; 

    public enum Weapon { OneHandGun, MachineGun, LaserGun, None }; //총기의 종류. 시작시 지정해주고 이것을 gunManager한테 넘겨줌으로써 총기 생성
    public Weapon currentWeapon;

    //땅과 접촉하는 것을 검사하는 것과 관련된 속성
    public Transform groundCheckTransform;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded;

    public Transform targetTransform;
    public LayerMask mouseAimMask;
    public LayerMask groundMask;

    //아래 속성들은 모두 반동제어에 관련되어 사용됨. 아마도 사용하지 않을 것으로 생각됨.
    public AnimationCurve recoilCurve; //반동 커브. 사용하지 않을 예정.
    public float recoilDuration = 0.25f; //반동 시간
    public float recoilMaxRotation = 45f; //반동 회전각
    private float recoilTimer;
    public Transform rightLowerArm; 
    public Transform rightHand;

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

    /*private void LateUpdate()
    {
        // Recoil Animation
        if (recoilTimer < 0)
        {
            return;
        }

        float curveTime = (Time.time - recoilTimer) / recoilDuration;
        if (curveTime > 1f)
        {
            recoilTimer = -1;
        }
        else
        {
            rightLowerArm.Rotate(Vector3.forward, recoilCurve.Evaluate(curveTime) * recoilMaxRotation, Space.Self);
        }


    }*/

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

    //IK 애니메이션 관련 함수
    private void OnAnimatorIK()
    {
        
        // Weapon Aim at Target IK
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, targetTransform.position);

        // Look at target IK
        animator.SetLookAtWeight(1);
        animator.SetLookAtPosition(targetTransform.position);
        
    }

    private void PlayerDied()
    {
        //플레이어가 체력이 0이되거나, -50이하의 낙사를 하게되면 사망한다.
        //이후 
        if (healthPoint <= 0 || transform.position.y < -50)
        {
            StartCoroutine(ShowBloodScreen()); // 피격효과 출력

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

    IEnumerator ShowBloodScreen()  // 피해를 입을 때 화면을 붉게 변하게 해주는 함수
    {
        BloodScreen.color = new Color(1, 0, 0, UnityEngine.Random.Range(0.2f, 0.3f)); // 이미지 색은 Red,  투명도는 0.2~0.3사이 랜덤 출력
        yield return new WaitForSeconds(0.2f); // 0.2초동안 출력
        BloodScreen.color = Color.clear; // 이미지를 다시 투명하게
        Debug.Log("피격 효과 화면 출력");
    }
}
