using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    //public UnityEngine.Animations.Rigging.Rig handIk;
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon1)
        {
            //handIk.weight = 1.0f;
        }
        if (weapon2)
        {
            //handIk.weight = 1.0f;
        }
        else
        {
            //handIk.weight = 0.0f;
        }
    }

    public void Equip()
    {
        //handIk.weight = 1.0f;
    }
}
