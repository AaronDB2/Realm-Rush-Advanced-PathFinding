using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The weapon of the tower that will fire and rotate")]
    Transform weapon;

	[SerializeField]
	[Tooltip("The range of the tower")]
	float range = 15f;

	[SerializeField]
	[Tooltip("Particle system that fire the tower bullet particles")]
	ParticleSystem projectileParticles;

	Transform target;

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

	// Function for finding the closest enemy
	void FindClosestTarget()
	{
		// Find all the enemies in the scene
		Enemy[] enemies = FindObjectsOfType<Enemy>();

		Transform closestTarget = null;
		float maxDistance = Mathf.Infinity;

		foreach (Enemy enemy in enemies)
		{
			// Calculate the distance between this game object and the current enemy
			float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

			if(targetDistance < maxDistance)
			{
				closestTarget = enemy.transform;
				maxDistance = targetDistance;
			}
		}

		target = closestTarget;
	}

	// Function that moves the weapon
	void AimWeapon()
	{
		// Calculate targetDistance
		float targetDistance = Vector3.Distance(transform.position, target.position);	

		weapon.LookAt(target);

		if(targetDistance < range)
		{
			Attack(true);
		} else
		{
			Attack(false);
		}
	}

	// Function for handling attack logic
	void Attack(bool isActive)
	{
		var emissionModule = projectileParticles.emission;
		emissionModule.enabled = isActive;
	}
}
