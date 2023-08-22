using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The weapon of the tower that will fire and rotate")]
    Transform weapon;

	Transform target;
	// Start is called before the first frame update
	void Start()
    {
        target = FindObjectOfType<EnemyMover>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        AimWeapon();
    }

    // Function that moves the weapon
	void AimWeapon()
	{
		weapon.LookAt(target);
	}
}
