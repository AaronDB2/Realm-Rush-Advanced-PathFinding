using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Tower script")]
	Tower towerPrefab;

	[SerializeField]
	[Tooltip("Set to true if objects can be placed on this object")]
	bool isPlaceable;
	public bool IsPlaceable { get { return isPlaceable; }}

	// Executes when mouse button is down on this object
	void OnMouseDown()
	{
		if(isPlaceable)
		{
			bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
			isPlaceable = !isPlaced;
		}
	}
}
