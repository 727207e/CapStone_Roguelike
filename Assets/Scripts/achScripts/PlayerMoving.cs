using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoving : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(new Vector3(10.0f, 0f, 0f)*Time.deltaTime);
        else if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(new Vector3(-10.0f, 0f, 0f)*Time.deltaTime);
    }
}
