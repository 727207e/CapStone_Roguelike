using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MRangerStatus : MonoBehaviour
{
    public int maxHP = 80;
    public int curHP = 80;
    public GameObject coin;
    public int Damage = 30;

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
                float randomx = Random.Range(-5, 6);
                Vector3 coin_posi = new Vector3(transform.position.x + randomx, transform.position.y, 0);
                coinn.transform.position = coin_posi;
            }

            //몬스터 수 감소
            mapscript.instance.monster_count--;

            // 생성한 뒤 제거
            Destroy(gameObject, 1.5f);
        }

        // 디버그용 몬스터 체력 10씩 감소
        if (Input.GetKeyDown(KeyCode.Mouse2) == true)
        {
            curHP -= 10;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            // 체력 감소
        }

        if (other.tag == "skill_1")
        {

        }

        if (other.tag == "skill_2")
        {

        }

        if (other.tag == "skill_3")
        {

        }
    }
}
