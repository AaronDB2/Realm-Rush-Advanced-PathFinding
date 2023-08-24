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

    Enemy enemy;

	// OnEnable is called when game object is enabled
	void OnEnable()
    {
        currentHitPoints = maxHealth;
    }

	void Start()
	{
		enemy = GetComponent<Enemy>();
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
            gameObject.SetActive(false);
            enemy.RewardGold();
        }
	}
}
