using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterRangerAttack : MonoBehaviour
{
    public GameObject target;
    public GameObject bullet;
    public GameObject fire_Pos;
    public float dis;
    public float attack_Delay = 0.0f;
    private float attack_Speed = 3.0f;
    private float sight_Range = 20.0f;
    private int stage_position;
    // Start is called before the first frame update
    void Start()
    {
        stage_position = mapscript.instance.stage_Position;
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        dis = Vector3.Distance(target.transform.position, transform.position);
        
        attack();

        fire_Pos.transform.LookAt(target.transform);
    }

    private void attack()
    {
        if (attack_Delay >= attack_Speed && dis <= sight_Range)
        {
            Instantiate(bullet, fire_Pos.transform.position, fire_Pos.transform.rotation).transform.SetParent(mapscript.instance.map_List[stage_position].transform);

            attack_Delay = 0.0f;
        }
        else
            attack_Delay += Time.deltaTime;
    }
}
