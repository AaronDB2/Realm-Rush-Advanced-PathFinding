using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Maxhealth of the object")]
    int maxHealth = 5;

    [SerializeField]
    int currentHitPoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHitPoints = maxHealth;
    }

    // Function that fires when particles collide with the collider of this object
	void OnParticleCollision(GameObject other)
	{
        ProcessHit();
	}

    // Function for subtracting the health of the object
	void ProcessHit()
	{
        currentHitPoints--;

        if(currentHitPoints <= 0)
        {
            Destroy(gameObject);
        }
	}
}
