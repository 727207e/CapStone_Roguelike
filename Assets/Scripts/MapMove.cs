using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    public List<GameObject> mapList; //맵 저장

    int room_count = 0;             //맵 카운팅(맵 리스트에 나올 맵의 순서번째)
    private GameObject old_room;    //현재 사용중인 맵
    private GameObject new_room;    //새로 생성될 맵


    // Start is called before the first frame update
    void Start()
    {
        //방 순서가 섞임
        mapList = new List<GameObject>(Utility.ShuffleArray(mapList.ToArray(), mapList.Count));

        NextRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextRoom()
    {
        //캐릭터 이동 제어를 위해 비활성화
        GameManager.Instance.player.GetComponent<CharMove>().player_Move_Bool = false;

        //과거방 삭제
        if (old_room != null)
            Destroy(old_room);

        //새로운 방 생성
        new_room = Instantiate(mapList[room_count], Vector3.zero, Quaternion.identity) as GameObject;
        room_count++;

        //과거방 할당
        old_room = new_room;

        //FadeOUT
        FadeController.instance.objFadeout();

        //플레이어를 해당 방에 시작점으로 이동
        GameManager.Instance.player.transform.position = new_room.GetComponent<RoomInfo>().start_Position.transform.position;

        //FadeIN
        FadeController.instance.objFadein();

        //플레이어 다음 이동 가능(활성화)
        GameManager.Instance.player.GetComponent<CharMove>().player_Move_Bool = true;

    }
}
