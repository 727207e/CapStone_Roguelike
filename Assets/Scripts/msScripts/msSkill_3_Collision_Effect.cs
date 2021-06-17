using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msSkill_3_Collision_Effect : MonoBehaviour
{
    //public GameObject player;
    //public msPlayerControllerNew msPCN;
    public float effect_3_ReflectForce = 3000f;
    public Vector3 tempPlayerPosition;
    public int skillDamage = 0;
    //public GameObject effectSelf;

    // Start is called before the first frame update
    void Start()
    {
        /*player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Debug.Log("플레이어 캐릭터가 현재 존재합니다. : msSkill_3_Collision_Effect");
        }
        else
        {
            Debug.LogError("플레이어 캐릭터가 존재하지 않습니다. : msSkill_3_Collision_Effect");
        }
        msPCN = player.GetComponent<msPlayerControllerNew>();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
       // Vector3 tempPlayerPos;
        Vector3 tempMonsterPos;

        if (collision.gameObject.tag == "Monster")
        {
            //Debug.Log("Effect 3 Activate Collider with Monster Tag");
            //Debug.Log(tempPlayerPosition);
            tempMonsterPos = collision.gameObject.GetComponent<Transform>().position;
            //Debug.Log(tempMonsterPos);

            Vector3 tempVectorDirection = tempMonsterPos - tempPlayerPosition;
            tempVectorDirection = tempVectorDirection.normalized;
            //Debug.Log(tempVectorDirection);

            collision.gameObject.GetComponent<Rigidbody>().AddForce(tempVectorDirection * effect_3_ReflectForce);
            //Debug.Log("Add Forced");


        }



        if (collision.gameObject.tag == "EnemeyHitBos")
                {
                    collision.gameObject.GetComponent<BossHitScripts>().Damaged(skillDamage);
                }

    }

    public void SetPlayerPosition(Vector3 position)
    {
        tempPlayerPosition = position;
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
