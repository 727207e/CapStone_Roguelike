using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterMeleeAttack : MonoBehaviour
{
    public GameObject target;

    public float dis;
    public float attack_Delay = 0.0f;
    //private float attack_Speed = 3.0f;
    private float sight_Range = 10.0f;
    public float timer = 0.0f;

    public bool patrol_State = true;
    private Vector3 moveto_Position;

    void Update()
    {
        timer += Time.deltaTime;
        target = GameObject.FindGameObjectWithTag("Player");
        dis = Vector3.Distance(target.transform.position, transform.position);

        if (dis > sight_Range)
            patrol();
        else if (dis <= sight_Range)
            detect();
    }

    public void patrol()
    {
        int rand = Random.Range(0, 3);
        int x = 0;
        if (rand == 0)
            x = 0;
        else if (rand == 1)
            x = -10;
        else if (rand == 2)
            x = 10;
        if (patrol_State && timer >= 2.0f)
        {
            moveto_Position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
            patrol_State = false;
            timer = 0.0f;
        }
        else if (!patrol_State && timer < 2.0f)
            transform.position = Vector3.MoveTowards(transform.position, moveto_Position, 1.0f * Time.deltaTime);

        else if (!patrol_State && timer >= 2.0f)
        {
            patrol_State = true;
            timer = 0.0f;
        }
    }

    public void detect()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 2.0f * Time.deltaTime);
        timer = 0.0f;
    }
}
