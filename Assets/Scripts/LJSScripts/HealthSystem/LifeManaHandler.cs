using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManaHandler : MonoBehaviour
{
    public GameObject player;
    public msPlayerControllerNew msPCN;

    public Image lifeBar;
    public Image manaBar;
    public Text lifeText;
    public Text manaText;
    



    public float calculateHealthPoint;

    void Start()
    {

        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Debug.Log("플레이어 캐릭터가 현재 존재합니다.");
        }
        else
        {
            Debug.LogError("플레이어 캐릭터가 존재하지 않습니다.");
        }
        msPCN = player.GetComponent<msPlayerControllerNew>();
    }


    void Update()
    {
        calculateHealthPoint = msPCN.healthPoint / msPCN.initHealthPoint;
        lifeBar.fillAmount = Mathf.MoveTowards(lifeBar.fillAmount, calculateHealthPoint, Time.deltaTime);
        lifeText.text = "" + (int)msPCN.healthPoint;

        if (msPCN.AbilityPoint < msPCN.initAbilityPoint)
        {
            manaBar.fillAmount = Mathf.MoveTowards(manaBar.fillAmount, 1f, Time.deltaTime * 0.01f);
            msPCN.AbilityPoint = Mathf.MoveTowards(msPCN.AbilityPoint / msPCN.AbilityPoint, 1f, Time.deltaTime * 0.01f) * msPCN.initAbilityPoint;
        }

        if (msPCN.AbilityPoint < 0)
        {
            msPCN.AbilityPoint = 0;
        }

        manaText.text = "" + Mathf.FloorToInt(msPCN.AbilityPoint);
    }

    public void Damage(float damage)
    {
        msPCN.healthPoint -= damage;
        Debug.Log("현재 남은 체력 " + msPCN.healthPoint);
    }

    public void ReduceMana(float mana)
    {
        msPCN.AbilityPoint -= mana;
        Debug.Log("현재 남은 마나 " + msPCN.AbilityPoint);
        manaBar.fillAmount -= mana / msPCN.initAbilityPoint;
    }


}

