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

    public RectTransform uiGroup;
    public Image bloodScreen;

    bool activebloodScreen = false;

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


        if (msPCN.abilityPoint < msPCN.initAbilityPoint)
        {
            manaBar.fillAmount = Mathf.MoveTowards(manaBar.fillAmount, 1f, Time.deltaTime * 0.01f);
            msPCN.abilityPoint = Mathf.MoveTowards(msPCN.abilityPoint / msPCN.initAbilityPoint, 1f, Time.deltaTime * 0.01f) * msPCN.initAbilityPoint;
        }

        if (msPCN.abilityPoint < 0)
        {
            msPCN.abilityPoint = 0;
        }

        manaText.text = "" + Mathf.FloorToInt(msPCN.abilityPoint);
    }

    public void Damage(float damage)
    {

        if (msPCN.healthPoint < msPCN.initHealthPoint)
        {
            StartCoroutine(ShowBloodScreen());
        }
        msPCN.healthPoint -= damage;
        Debug.Log("현재 남은 체력 " + msPCN.healthPoint);
    }

    public void ReduceMana(float mana)
    {
        msPCN.abilityPoint -= mana;
        Debug.Log("현재 남은 마나 " + msPCN.abilityPoint);
        manaBar.fillAmount -= mana / msPCN.initAbilityPoint;
    }


    public void HpHeal(float heal)
    {
        msPCN.healthPoint += heal;
        Debug.Log("현재 남은 체력 " + msPCN.healthPoint);
    }

    IEnumerator ShowBloodScreen()
    {
        uiGroup.anchoredPosition = Vector3.zero;
        bloodScreen.color = new Color(1, 0, 0, UnityEngine.Random.Range(0.2f, 0.3f));
        yield return new WaitForSeconds(0.1f);
        bloodScreen.color = Color.clear;
        uiGroup.anchoredPosition = Vector3.down * 10000;
    }
}

