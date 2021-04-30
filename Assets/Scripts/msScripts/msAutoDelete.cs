using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msAutoDelete : MonoBehaviour
{
    public int lifeTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
