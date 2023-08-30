using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Tower script")]
	Tower towerPrefab;

	[SerializeField]
	[Tooltip("Set to true if objects can be placed on this object")]
	bool isPlaceable;
	public bool IsPlaceable { get { return isPlaceable; }}

	GridManager gridManager;
	Pathfinder pathfinder;
	Vector2Int coordinates = new Vector2Int();

	void Awake()
	{
		gridManager = FindObjectOfType<GridManager>();
		pathfinder = FindObjectOfType<Pathfinder>();
	}

	void Start()
	{
		if(gridManager != null)
		{
			coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

			if(!isPlaceable)
			{
				gridManager.BlockNode(coordinates);
			}
		}
	}

	// Executes when mouse button is down on this object
	void OnMouseDown()
	{
		if(gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
		{
			bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);
			if(isSuccessful)
			{
				gridManager.BlockNode(coordinates);
				pathfinder.NotifyReceivers();
			}
		}
	}
}
