using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playergate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject map = GameObject.FindGameObjectWithTag("Map"); ;
        if (other.tag == "Gate")
        {
            FadeController.instance.objFadeout();

            if (other.transform.position.x == 10.0f)
            {
                transform.position = new Vector3(-8.0f, 1.5f, 0.0f);
                StageChange.instance.disableMap();
                StageChange.instance.mappos++;
                StageChange.instance.activeMap();
            }
            else if (other.transform.position.x == -10.0f)
            {
                transform.position = new Vector3(8.0f, 1.5f, 0.0f);
                StageChange.instance.disableMap();
                StageChange.instance.mappos--;
                StageChange.instance.activeMap();
            }

            FadeController.instance.objFadein();
        }
    }


}
