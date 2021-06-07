﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class msPlayerControllerNew : MonoBehaviour
{
    private bool characterMoveMode = false; //캐릭터가 2D로 움직일 건지 3D로 움직일건지
    private bool isStage = false; //캐릭터가 현재 인게임인지, 아니면 필드인지

    //캐릭터가 가지는 스탯
    public float walkSpeed = 2.5f; //이동속도 기본 10.0f
    public float jumpHeight = 5f; //점프 높이 기본 10.0f
    public float acceleration = 1.2f; //가속력 기본 1.2f
    public float dashAcceleration = 500f;
    public float toughness = 0f; //강인함. 넉백에 관여함. 수치가 높을 수록 넉백이 줄어든다.

    public float initHealthPoint = 1000f; //플레이어가 가진 최대 체력을 의미. 이것을 통해 계산하는 함수가 많아서 만듬
    public float initAbilityPoint = 100f; //플레이어가 가진 최대 기력을 의미. 위와 동일
    public float healthPoint; //캐릭터 체력 기본 1000f. 이 수치는 플레이어의 게임 내에서의 현재 체력을 의미함.
    public float abilityPoint; //위와 동일.

    public float invincibleTime = 1.0f; //무적 유지 시간.

    //IK 조작 관련
    public UnityEngine.Animations.Rigging.Rig pistolIk;
    public UnityEngine.Animations.Rigging.Rig rifleIk;
    public UnityEngine.Animations.Rigging.Rig cannonIk;

    //무기 설정 관련
    public int currentWeapon; //0 : none, 1 : pistol, 2 : rifle, 3 : cannon
    private GameObject currentUsedWeapon;
    public GameObject pistol;
    public GameObject rifle;
    public GameObject cannon;
    public bool isRifleActivate = false;
    public bool isCannonActivate = false;

    //땅과 접촉하는 것을 검사하는 것과 관련된 속성
    public Transform groundCheckTransform;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded;
    public LayerMask groundMask;
    private bool isRolling;
    private bool isDamaged;

    //마우스와 에임 일체화과 관련된 속성
    public Transform targetTransform;
    public LayerMask mouseAimMask;

    //애니메이터, 카메라, 리지드 바디 관련
    private Animator animator;
    private Rigidbody rbody;
    private Camera mainCamera;
    private RigBuilder rb;

    //스킬 UI와의 연동을 위함
    public SkillAbility sklUI;
    public GameObject skillOneEffect;
    public GameObject skillTwoEffect;
    public GameObject skillThreeEffect;
    public GameObject skillFourEffect;

     void Awake()
    {
        //모든 함수들 중 가장 먼저 처리되어야하는 경우 이쪽에 등록.
        //여기에 있는 코드를 Start에 넣을 경우 다른 함수가 먼저 처리되는 경우가 발생할 수 있음.

        /*pistol.SetActive(true);
        rifle.SetActive(false);
        cannon.SetActive(false);
        pistolIk.weight = 1.0f;
        rifleIk.weight = 0.0f;
        cannonIk.weight = 0.0f;

        currentWeapon = 1;
        Debug.Log(currentWeapon);*/
    }

    void Start()
    {
        rb = GetComponent<RigBuilder>();

        pistol.SetActive(true);
        rifle.SetActive(false);
        cannon.SetActive(false);
        pistolIk.weight = 1.0f;
        rifleIk.weight = 0.0f;
        cannonIk.weight = 0.0f;

        currentWeapon = 1;
        Debug.Log(currentWeapon);

        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        healthPoint = initHealthPoint;
        abilityPoint = initAbilityPoint;

        //스킬 UI는 하나밖에 없다고 생각해서 Find를 사용함.
        sklUI = GameObject.Find("SkillUI").GetComponent<SkillAbility>();

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //마우스의 현재 위치를 받고 캐릭터가 바라보는 곳과 일체화시킴.
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask))
        {
            targetTransform.position = hit.point;
        }

        //점프 기능을 수행
        if (Input.GetButtonDown("Jump") && isGrounded && isRolling==false && isDamaged==false)
        {
            rbody.velocity = new Vector3(rbody.velocity.x, 0, 0);
            rbody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight *-1* Physics.gravity.y),ForceMode.VelocityChange);
        }

        PlayerDied(); //사망처리
        DebugPlayer(); //디버깅
        PlayerDash(); //대시
        
        WeaponControl(); //무기관리자
    }

    public void WeaponControl()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentWeapon--;
            Debug.Log("버튼 Q 중간 과정 : " + currentWeapon);
            if (currentWeapon==0)
            {
                currentWeapon = 3;
            }
            if (currentWeapon==2 && isRifleActivate == false && isCannonActivate==false)
            {
                currentWeapon = 1;
            }
            else if (currentWeapon==2 && isRifleActivate==false && isCannonActivate == true)
            {
                currentWeapon = 1;
            }
            if(currentWeapon==3 && isCannonActivate == false && isRifleActivate ==true)
            {
                currentWeapon = 2;
            }
            SwitchingWeapon();
            Debug.Log("current Weapon : " + currentWeapon);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentWeapon++;
            Debug.Log("버튼 E 중간 과정 : " + currentWeapon);
            if (currentWeapon==4)
            {
                currentWeapon = 1;
            }
            if (currentWeapon == 2 && isRifleActivate == false && isCannonActivate == false)
            {
                currentWeapon = 1;
            }
            else if (currentWeapon == 2 && isRifleActivate == false && isCannonActivate == true)
            {
                currentWeapon = 3;
            }
            if (currentWeapon == 3 && isCannonActivate == false)
            {
                currentWeapon = 1;
            }
            SwitchingWeapon();
            Debug.Log("current Weapon : " + currentWeapon);
        }

    }

    public void SwitchingWeapon()
    {
        switch (currentWeapon)
        {
            case 1:
                {
                    pistol.SetActive(true);
                    rifle.SetActive(false);
                    cannon.SetActive(false);

                    pistolIk.weight = 1.0f;
                    rifleIk.weight = 0.0f;
                    cannonIk.weight = 0.0f;

                    /*Debug.Log("pistol ik : " + pistolIk.weight);
                    Debug.Log("rifle ik : " + rifleIk.weight);
                    Debug.Log("cannon ik : " + cannonIk.weight);*/

                    currentUsedWeapon = pistol;

                    Debug.Log("1번 장비 장착");

                }
                break;
            case 2:
                {
                    pistol.SetActive(false);
                    rifle.SetActive(true);
                    cannon.SetActive(false);

                    pistolIk.weight = 0.0f;
                    rifleIk.weight = 1.0f;
                    cannonIk.weight = 0.0f;

                    currentUsedWeapon = rifle;

                    Debug.Log("2번 장비 장착");


                }
                break;

            case 3:
                {
                    pistol.SetActive(false);
                    rifle.SetActive(false);
                    cannon.SetActive(true);

                    pistolIk.weight = 0.0f;
                    rifleIk.weight = 0.0f;
                    cannonIk.weight = 1.0f;

                    currentUsedWeapon = cannon;

                    Debug.Log("3번 장비 장착");
                }
                break;

            default:
                {
                    Debug.Log("예기치 못한 에러");
                }
                break;




        }
    }

    private void initPlayerStat()
    {
        //플레이어 생성시 만들어지는 플레이어 초기화 함수이다.
        //
    }

    //이동함수
    private void PlayerMove()
    {

        if (isRolling == false && isDamaged==false)
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

            transform.Translate(velocity * walkSpeed * Time.deltaTime, Space.World);
            //m_rigidBody.MovePosition(transform.position + velocity * m_moveSpeed * Time.deltaTime);
            animator.SetFloat("Speed", velocity.magnitude);
        }
        else
        {

        }
        
    }

    //움직임, 회전, 그라운드 체크를 수행함.
    private void FixedUpdate()
    {
        //움직이기 - 영상에서 사용한 경우. 이 경우 3d 이동이 불가능하기 때문에 자체적으로 만든 함수를 사용
        PlayerMove();

        //플레이어 회전
        rbody.MoveRotation(Quaternion.Euler(new Vector3(0, 90 * Mathf.Sign(targetTransform.position.x - transform.position.x), 0)));

        //그라운드 체크
        isGrounded = Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, groundMask, QueryTriggerInteraction.Ignore);
        animator.SetBool("isGrounded", isGrounded);

        SkillAbility(); //스킬체크
    }

    private void PlayerDied()
    {
        //플레이어가 체력이 0이되거나, 플레이어의 y축 가속도가 -50을 넘어가면 사망함.
        //이후 
        if (healthPoint <= 0 || rbody.velocity.y < -100)
        {
            healthPoint = 0;
            animator.SetTrigger("isDead");
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

        //P를 누르면 3D 움직임을 수행함.
        if (Input.GetKeyDown(KeyCode.P))
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
        animator.SetTrigger("isDamage");
        StartCoroutine(DamagedAnimationDelay());
        rbody.AddForce(new Vector3(-2.0f, 0.2f, 0) * (1000f-toughness),ForceMode.Acceleration); //넉백의 구현
    }

    public void SkillAbility()
    {
        Vector3 skillOneTransform = transform.position + new Vector3(3 * Mathf.Sign(targetTransform.position.x - transform.position.x), 1,0);
        Vector3 skillTwoTransform = transform.position + new Vector3(0.3f * Mathf.Sign(targetTransform.position.x - transform.position.x), 1, 0);
        Vector3 skillFourTransform = transform.position + new Vector3(0,0.7f,0);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (sklUI.isCooldown == false)
            {
                sklUI.isCooldown = true;
                sklUI.abilityImage1.fillAmount = 1;
                StartCoroutine(SkillAnimationDelay(1, 10f));
                Instantiate(skillOneEffect, skillOneTransform, transform.rotation);
                Debug.Log("1번 스킬 발동");
            }
            else
            {
                Debug.Log("1번 스킬은 쿨타임 대기 중 입니다.");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (sklUI.isCooldown2 == false)
            {
                sklUI.isCooldown2 = true;
                sklUI.abilityImage2.fillAmount = 1;
                StartCoroutine(SkillAnimationDelay(2, 10));
                Instantiate(skillTwoEffect, skillTwoTransform, transform.rotation);
                Debug.Log("2번 스킬 발동");
            }
            else
            {
                Debug.Log("2번 스킬은 쿨타임 대기 중 입니다.");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (sklUI.isCooldown3 == false)
            {
                sklUI.isCooldown3 = true;
                sklUI.abilityImage3.fillAmount = 1;
                StartCoroutine(SkillAnimationDelay(3, 10));
                Instantiate(skillThreeEffect, transform.position, transform.rotation);
                Debug.Log("3번 스킬 발동");
            }
            else
            {
                Debug.Log("3번 스킬은 쿨타임 대기 중 입니다.");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (sklUI.isCooldown4 == false)
            {
                sklUI.isCooldown4 = true;
                sklUI.abilityImage4.fillAmount = 1;
                StartCoroutine(SkillAnimationDelay(4, 10));
                Instantiate(skillFourEffect, skillFourTransform, transform.rotation);
                Debug.Log("4번 스킬 발동");
            }
            else
            {
                Debug.Log("4번 스킬은 쿨타임 대기 중 입니다.");
            }
        }
    }

    public void PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(RollingAnimationDelay());
            rbody.AddForce(new Vector3(1 * Mathf.Sign(targetTransform.position.x - transform.position.x), 0, 0) * dashAcceleration, ForceMode.Acceleration);
        }
    }

    //플레이어와 충돌한 오브젝트와 반응을 수행한다.
    public void OnTriggerEnter(Collider other)
    {
        //이런식으로 작성해주시면 됩니다.
        if (other.gameObject.CompareTag("EnemyAttack"))
        {
            //여기서 적 개체 발사한 오브젝트의 함수를 사용한다.
            Debug.Log("공격받았습니다.");
            PlayerDamaged(100); //플레이어 타격 계산 시행
            Destroy(other.gameObject); //대상 오브젝트 파괴 (경우에 따라 다름)
            StartCoroutine("InvincibleTime"); //무적시간 실행
        }

        if (other.gameObject.CompareTag("BossAtt"))
        {
            //여기서 적 개체 발사한 오브젝트의 함수를 사용한다.
            Debug.Log("보스에게 공격받았습니다.");

            BossAttackDamage boss = other.gameObject.GetComponent<BossAttackDamage>();

            if (boss)
            {
                PlayerDamaged(boss.Damage);
            }

            StartCoroutine("InvincibleTime");
        }

    }

    public  IEnumerator InvincibleTime() {
        //int countTime = 0;

        Debug.Log("무적 시간 시작");
        this.gameObject.layer = 12; //레이어를 PlayerDamaged로 전환하여 Enemy와의 충돌 판정을 무시함.

        /* 깜빡거림 구현용
        while (countTime < 10)
        {
            if (countTime % 2 == 0)
            {

            }
            else
            {

            }

            countTime++;
        }*/

        yield return new WaitForSeconds(invincibleTime);


        Debug.Log("무적 시간 종료");
        this.gameObject.layer = 8;
        //무적시간 종료

        yield return null;
    }

    public IEnumerator SkillAnimationDelay(int skillnum, float duration)
    {
        int countTime = 0;

        animator.SetTrigger("isSkill_" + skillnum + "_Use");
        rb.enabled = false;

        if (currentWeapon == 1)
        {
            pistol.SetActive(false);
        }
        else if (currentWeapon == 2)
        {
            rifle.SetActive(false);
        }
        else
        {
            cannon.SetActive(false);
        }


        while (countTime < duration)
        { 
            yield return new WaitForSeconds(0.1f);

            countTime++;
        }

        rb.enabled = true;
        if (currentWeapon == 1)
        {
            pistol.SetActive(true);
        }
        else if (currentWeapon == 2)
        {
            rifle.SetActive(true);
        }
        else
        {
            cannon.SetActive(true);
        }

        yield return null;
    }

    public IEnumerator RollingAnimationDelay()
    {
        int countTime = 0;

        animator.SetTrigger("isRolling");
        isRolling = true;
        rb.enabled = false;
        //this.gameObject.layer = 12;
        //StartCoroutine("InvincibleTime");


        if (currentWeapon == 1)
        {
            pistol.SetActive(false);
        }
        else if (currentWeapon == 2)
        {
            rifle.SetActive(false);
        }
        else
        {
            cannon.SetActive(false);
        }


        while (countTime < 10)
        {
            yield return new WaitForSeconds(0.1f);

            countTime++;
        }

        isRolling = false;
        rb.enabled = true;
        if (currentWeapon == 1)
        {
            pistol.SetActive(true);
        }
        else if (currentWeapon == 2)
        {
            rifle.SetActive(true);
        }
        else
        {
            cannon.SetActive(true);
        }
        //this.gameObject.layer = 8;


        yield return null;
    }

    public IEnumerator DamagedAnimationDelay()
    {
        int countTime = 0;

        animator.SetTrigger("isDamaged");
        isDamaged = true;
        rb.enabled = false;
        this.gameObject.layer = 12;

        if (currentWeapon == 1)
        {
            pistol.SetActive(false);
        }
        else if (currentWeapon == 2)
        {
            rifle.SetActive(false);
        }
        else
        {
            cannon.SetActive(false);
        }


        while (countTime < 10)
        {
            yield return new WaitForSeconds(0.1f);

            countTime++;
        }

        isDamaged = false;
        rb.enabled = true;
        if (currentWeapon == 1)
        {
            pistol.SetActive(true);
        }
        else if (currentWeapon == 2)
        {
            rifle.SetActive(true);
        }
        else
        {
            cannon.SetActive(true);
        }
        this.gameObject.layer = 8;


        yield return null;
    }

    private float GetAnimLength(string animName)
    {
        float time = 0;
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;

        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == animName)
            {
                time = ac.animationClips[i].length;
            }
        }

        return time;
    }

}
