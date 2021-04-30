using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManaHandler : MonoBehaviour
{
    public Image lifeBar;
    public Image manaBar;
    public Text lifeText;
    public Text manaText;

    public float myLife;
    public float myMana;

    public float currentLife;
    public float currentMana;
    public float calculateLife;

    void Start()
    {
        currentLife = myLife;
        currentMana = myMana;
    }


    void Update()
    {
        calculateLife = currentLife / myLife;
        lifeBar.fillAmount = Mathf.MoveTowards(lifeBar.fillAmount, calculateLife, Time.deltaTime);
        lifeText.text = "" + (int)currentLife;

        if( currentMana < myMana)
        {
            manaBar.fillAmount = Mathf.MoveTowards(manaBar.fillAmount, 1f, Time.deltaTime * 0.01f);
            currentMana = Mathf.MoveTowards(currentMana / myMana, 1f, Time.deltaTime * 0.01f) * myMana;
        }

        if (currentMana<0)
        {
            currentMana = 0;
        }

        manaText.text = "" + Mathf.FloorToInt(currentMana);
    }

    public void Damage(float damage)
    {
        currentLife -= damage;
    }

    public void ReduceMana(float mana)
    {
        currentMana -= mana;
        manaBar.fillAmount -= mana / myMana;
    }   
}
