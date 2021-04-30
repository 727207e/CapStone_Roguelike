using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
 

    private void OnTriggerEnter(Collider collision)
    {


        if (collision.tag.Equals("Player"))
        {
                    ScoreTextScript.coinAmount += 1;
                    Destroy(this.gameObject);
                  }
            }

        }


