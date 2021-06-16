using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterStatus : MonoBehaviour
{
    public int maxHP = 100;
    public int curHP = 100;
    public GameObject coin;
    public int Damage = 50;

    private bool alive = true;
    private int position;

    // 클리어 횟수
    public int clearCount = 0;
    // 배수
    private double multiple = 1;

    private Animator animator;

    public Image hpBar;

    void Start()
    {
        position = mapscript.instance.stage_Position;

        mapscript.instance.monster_count++;

        // 클리어 횟수 로드
        //clearCount = 

        multiple = 1 * (1 + (clearCount * 0.1));
        maxHP = (int)(maxHP * multiple);
        curHP = maxHP;
        Damage = (int)(Damage * multiple);

        animator = GetComponent<Animator>();

        alive = true;

        hpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    void Update()
    {
        if (curHP <= 0)
            curHP = 0;

        hpBar.rectTransform.localScale = new Vector3((float)curHP / (float)maxHP, 1f, 1f);

        if (curHP <= 0 && alive)
        {
            alive = false;

            animator.SetTrigger("Dead");

            // 랜덤값
            int rand = Random.Range(1, 4);
            // 코인을 맵의 자식오브젝트로 생성
            // 랜덤값 만큼 반복
            for (int i = 1; i <= rand; i++)
            {
                GameObject coinn = Instantiate(coin, mapscript.instance.map_List[position].transform) as GameObject;
                // 코인 위치 랜덤하게 조정
                float randomx = Random.Range(-0.5f, 0.5f);
                float randomy = Random.Range(-0.5f, 0.5f);
                Vector3 coin_posi = new Vector3(transform.position.x + randomx, transform.position.y + randomy, 0);
                coinn.transform.position = coin_posi;
            }

            //몬스터 수 감소
            mapscript.instance.monster_count--;

            Rigidbody rigid = GetComponent<Rigidbody>();
            rigid.isKinematic = true;

            // 생성한 뒤 제거
            Destroy(gameObject, 1.5f);
        }

        // 디버그용 몬스터 체력 10 감소
        if (Input.GetKeyDown(KeyCode.Mouse2) == true)
        {
            curHP -= 10;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 총알공격
        if (other.tag == "PlayerAttack")
        {
            msBullet1 msBullet1 = other.GetComponent<msBullet1>();
            if (msBullet1 != null)
            {
                int damage = msBullet1.bulletDamage;
                curHP -= damage;
                Destroy(other.gameObject);
            }

            enemyBullet ebul = other.GetComponent<enemyBullet>();
            curHP -= ebul.damage;
            Destroy(other.gameObject);

            animator.SetTrigger("Damaged");
            // 캐넌 판정 아직 덜만들어짐
            /*
            else
            {
                msBullet_Cannon bullet_Cannon = other.GetComponent<msBullet_Cannon>();
                int damage = bullet_Cannon.bulletDamage;
            }
            */
        }

        // 혹시 트랩으로 떨어지면
        if (other.tag == "Trap")
        {
            curHP = 0;
        }

        if (other.tag == "skill_2")
        {

        }

        if (other.tag == "skill_3")
        {

        }
    }

    public void Damaged(int x)
    {
        curHP -= x;
    }
}
