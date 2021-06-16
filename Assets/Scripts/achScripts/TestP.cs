using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestP : MonoBehaviour
{
    public GameObject[] monster = new GameObject[20];
    public GameObject bul;
    public GameObject firepos;
    public int index = 0;
    public float timer = 0;

    private enemyBullet eullet;
    // Start is called before the first frame update
    void Start()
    {
        monster = new GameObject[20];

        index = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        eullet = bul.GetComponent<enemyBullet>();
        eullet.damage = 10;
        timer += Time.deltaTime;

        monster = GameObject.FindGameObjectsWithTag("Monster");
        if (monster[index] != null)
            firepos.transform.LookAt(monster[index].transform.position);
        else
            index++;
        if (timer >= 0.5f)
        {
            Instantiate(bul, firepos.transform.position, firepos.transform.rotation);
            timer = 0;
        }

    }
}
