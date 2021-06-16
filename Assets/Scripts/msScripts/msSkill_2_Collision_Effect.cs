using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msSkill_2_Collision_Effect : MonoBehaviour
{
    public int skillDamage = 0;
    public float createPassTime=1.0f;
    public float createTime=0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (createPassTime >= createTime)
        {
            //Debug.Log(PhotonNetwork.CountOfPlayers);
            createPassTime = 0.0f;
        }
        else
        {
            createPassTime += Time.deltaTime;
        }
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
    }
}
