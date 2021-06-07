using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabTheTarget : MonoBehaviour
{
    public BossSlimePatern boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {

        }
    }
}
