using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillAbility : MonoBehaviour
{
    
    [Header("Ability 1")]   // 스킬1 
    public Image abilityImage1;
    public float cooldown1 = 5;  // 스킬1 쿨타임은 5초입니다
    bool isCooldown = false;   
    public KeyCode ability1;  // Keycode로 스킬1 은 F1키로 발동되게 설정해뒀습니다

    [Header("Ability 2")]   // 스킬1 
    public Image abilityImage2;
    public float cooldown2 = 5;  // 스킬2 쿨타임은 5초입니다
    bool isCooldown2 = false;
    public KeyCode ability2;   // Keycode로 스킬2 은 F2키로 발동되게 설정해뒀습니다

    [Header("Ability 3")]  // 스킬3 
    public Image abilityImage3;
    public float cooldown3 = 10;   // 스킬3 쿨타임은 10초입니다
    bool isCooldown3 = false;
    public KeyCode ability3;   // Keycode로 스킬3 은 F3키로 발동되게 설정해뒀습니다

    [Header("Ability 4")]  // 스킬1 
    public Image abilityImage4;
    public float cooldown4 = 15;   // 스킬4 쿨타임은 15초입니다
    bool isCooldown4 = false;
    public KeyCode ability4;  // Keycode로 스킬4 은 F4키로 발동되게 설정해뒀습니다



    void Start()
    {
        abilityImage1.fillAmount = 0;   // 
        abilityImage2.fillAmount = 0;  //
        abilityImage3.fillAmount = 0;  //
        abilityImage4.fillAmount = 0;  //  스킬 쿨타임이 초기화 된 상태로 UI창이 구성됩니다. 
    }

    
    void Update()
    {
        Ability1(); // 
        Ability2(); //
        Ability3(); //
        Ability4(); //  스킬 발동
    }

    void Ability1()
    {
        if(Input.GetKey(ability1) && isCooldown == false)  // 스킬이 쿨타임중이 아니면
        {
            isCooldown = true;  
            abilityImage1.fillAmount = 1;  // fillAmount = 1일때 스킬은 사용가능합니다.
        }

        if(isCooldown)   //  스킬이 쿨타임중이면
        {
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;  //  float형의 스킬 쿨타임 만큼의 시간에 비례해 시계방향으로 스킬 쿨타임이 돌아가고 있다는 것을 이미지로 보여줍니다.

            if(abilityImage1.fillAmount <= 0)  // 이미지가 0보다 작아져서 삭제되는것을 막기위해 fillAmount는 0보다 작을수 없게 고정하고
            {
                abilityImage1.fillAmount = 0;
                isCooldown = false;  // 이때는 스킬을 사용할수 없습니다.
            }
        }

    }

    void Ability2()  // Ability1()과 동일합니다. 아래의 Ability3, Ability4도 마찬가지
    {
        if (Input.GetKey(ability2) && isCooldown2 == false)
        {
            isCooldown2 = true;
            abilityImage2.fillAmount = 1;
        }

        if (isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;

            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 0;
                isCooldown2 = false;
            }
        }

    }

    void Ability3()
    {
        if (Input.GetKey(ability3) && isCooldown3 == false)
        {
            isCooldown3 = true;
            abilityImage3.fillAmount = 1;
        }

        if (isCooldown3)
        {
            abilityImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;

            if (abilityImage3.fillAmount <= 0)
            {
                abilityImage3.fillAmount = 0;
                isCooldown3 = false;
            }
        }

    }

    void Ability4()
    {
        if (Input.GetKey(ability4) && isCooldown4 == false)
        {
            isCooldown4 = true;
            abilityImage4.fillAmount = 1;
        }

        if (isCooldown4)
        {
            abilityImage4.fillAmount -= 1 / cooldown4 * Time.deltaTime;

            if (abilityImage4.fillAmount <= 0)
            {
                abilityImage4.fillAmount = 0;
                isCooldown4 = false;
            }
        }

    }
}
