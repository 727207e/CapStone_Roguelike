using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msParticleCollisionWithPlayer : MonoBehaviour
{
    public int damage = 30;
    public bool onHitTrigger = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            onHitTrigger = false;
            Debug.Log("Player Damaged by boss's Particle Effect");
            other.GetComponent<msPlayerControllerNew>().OnPlayerDamaged(damage);
        }
    }
}
