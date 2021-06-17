using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msCannonSplashEffect : MonoBehaviour
{
    public int skillDamage=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            collision.gameObject.GetComponent<MonsterStatus>().Damaged(skillDamage);
        }
        if (collision.gameObject.tag == "EnemeyHitBos")
        {
            collision.gameObject.GetComponent<BossHitScripts>().Damaged(skillDamage);
        }
    }

    public void SetSkillDamage(int x)
    {
        skillDamage = x;
    }

    public void ActiveEffect()
    {
        gameObject.SetActive(true);
        AudioManager.instance.PlaySound2D("WeaponCannonSplash");
    }


}
