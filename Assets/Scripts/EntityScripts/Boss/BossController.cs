using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    IBoss bossPaternStatus;


    // Start is called before the first frame update
    void Start()
    {
        bossPaternStatus = GetComponent<IBoss>();

        mapscript.instance.monster_count++;

        bossPaternStatus.ShowTheBoss();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    bossPaternStatus.ShowTheBoss();
        //}

        if (Input.GetKeyDown(KeyCode.K))
        {
            bossPaternStatus.BossHp = 30;
        }

    }
}
