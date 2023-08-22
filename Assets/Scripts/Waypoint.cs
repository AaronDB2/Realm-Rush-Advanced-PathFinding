using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Object to instantiate on objects that have isPlaceable flag set to true ")]
	GameObject towerPrefab;

	[SerializeField]
	[Tooltip("Set to true if objects can be placed on this object")]
	bool isPlaceable;

	// Executes when mouse button is down on this object
	void OnMouseDown()
	{
		if(isPlaceable)
		{
			// Instantiate the tower
			Instantiate(towerPrefab, transform.position, Quaternion.identity);
			isPlaceable = false;
		}
	}
}
