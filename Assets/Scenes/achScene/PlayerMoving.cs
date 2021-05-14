using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoving : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // 이동
        if (Input.GetKey(KeyCode.RightArrow))
            transform.position = new Vector3(transform.position.x + 20f * Time.deltaTime, transform.position.y);
            //transform.Translate(new Vector3(10.0f, 0f, 0f)*Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.position = new Vector3(transform.position.x - 20f * Time.deltaTime, transform.position.y);
        if (Input.GetKey(KeyCode.UpArrow))
            transform.position = new Vector3(transform.position.x, transform.position.y + 20.0f * Time.deltaTime);
    }

    // isTrigger 체크 안된애들
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한게 코인이면 코인 파괴
        if (collision.gameObject.tag == "Coin")
        {
            // 빠르게 위를 지나가면 붕 떠서 istrigger 세팅을 해봤는데 별 효과는 없는듯함
            Collider col = collision.gameObject.GetComponent<Collider>();
            col.isTrigger = true;
            Destroy(collision.gameObject);
        }
    }
    // isTrigger 체크된애들
    private void OnTriggerEnter(Collider other)
    {
        // 왼쪽 게이트면 이전맵으로
        if (other.gameObject == mapscript.instance.gates[0])
            mapscript.instance.previousMap();
        // 오른쪽 게이트면 다음 맵으로
        if (other.gameObject == mapscript.instance.gates[1])
            mapscript.instance.nextMap();
    }
}
