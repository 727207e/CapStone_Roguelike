using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateOpenControl : MonoBehaviour
{
    public GameObject open_Gate;
    public GameObject lab_Gate;
    public GameObject[] gates;
    private GameObject map;
    private int map_Position;

    // Start is called before the first frame update
    void Start()
    {
        gates = mapscript.instance.gates;
        map_Position = mapscript.instance.stage_Position;
        map = mapscript.instance.map_List[map_Position];
    }

    // Update is called once per frame
    void Update()
    {
        // 몬스터가 없으면
        if (mapscript.instance.monster_count <= 0)
        {
            // 왼쪽 게이트일 경우 배열 0번에 오른쪽 게이트일 경우 배열 1번에
            if (gameObject.name == "LGate")
            {
                if (map_Position != 0)
                {
                    gates[0] = Instantiate(open_Gate, map.transform) as GameObject;
                    gates[0].transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
                    gates[0].transform.Rotate(0, -90, 0);
                    Destroy(gameObject);
                }
            }
            if (gameObject.name == "RGate")
            {
                gates[1] = Instantiate(open_Gate, map.transform) as GameObject;
                gates[1].transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
                gates[1].transform.Rotate(0, 90, 0);
                Destroy(gameObject);
            }
            if (gameObject.name == "LabGate")
            {
                gates[2] = Instantiate(lab_Gate, map.transform) as GameObject;
                gates[2].transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
                gates[2].transform.Rotate(0, -90, 0);
                Destroy(gameObject);
            }
        }
    }
}
