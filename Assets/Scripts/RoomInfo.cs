using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    public GameObject start_Position;
    public MapMove MapMove;

    private void Start()
    {
        if(GameObject.Find("GameManager") != null)
        {
            MapMove = GameObject.Find("GameManager").GetComponent<MapMove>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //만약 플레이어가 이동할수 있는 상태라면
        if (GameManager.Instance.player.GetComponent<CharMove>().player_Move_Bool)
        {
            //다음 지형으로 이동
            MapMove.NextRoom();
        }
    }
}
