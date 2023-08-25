using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Maxhealth of the object")]
    int maxHealth = 5;

    [SerializeField]
    [Tooltip("Everytime an enemie dies there health increases with this value")]
    int difficultyRamp = 1;

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
			maxHealth += difficultyRamp;
            enemy.RewardGold();
        }
	}
}
