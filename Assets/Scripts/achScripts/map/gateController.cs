using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateController : MonoBehaviour
{
    private mapscript mapscript;
    // Start is called before the first frame update
    void Start()
    {
        mapscript = GetComponent<mapscript>();
    }

    // Update is called once per frame
    void Update()
    {
        mapscript.gates = GameObject.FindGameObjectsWithTag("Gate");
    }
}
