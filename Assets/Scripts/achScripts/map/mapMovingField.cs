using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapMovingField : MonoBehaviour
{
    public Vector3 from_pos;
    public Vector3 to_pos = Vector3.zero;
    public bool fromto = true;
    public float speed = 5.0f;

    private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        from_pos = transform.position;
        fromto = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fromto)
        {
            transform.position = Vector3.MoveTowards(transform.position, to_pos, speed * Time.deltaTime);
            if (to_pos == transform.position)
            {
                timer += Time.deltaTime;
                if (timer >= 3.0f)
                {
                    fromto = false;
                    timer = 0.0f;
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, from_pos, speed * Time.deltaTime);
            if (from_pos == transform.position)
            {
                timer += Time.deltaTime;
                if (timer >= 3.0f)
                {
                    fromto = true;
                    timer = 0.0f;
                }
            }
        }
    }
}
