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

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            bossPaternStatus.ShowTheBoss();
        }
    }
}
