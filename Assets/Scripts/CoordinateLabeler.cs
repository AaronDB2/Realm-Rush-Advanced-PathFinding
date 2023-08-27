using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Default color of the coordinate label")]
	Color defaultColor = Color.white;

	[SerializeField]
	[Tooltip("Color of the coordinate label when towers cant be build")]
	Color blockedColor = Color.grey;

	[SerializeField]
	[Tooltip("Color of the coordinate label for explored nodes")]
	Color exploredColor = Color.yellow;

	[SerializeField]
	[Tooltip("Color of the coordinate label for paths")]
	Color pathColor = new Color(1f, 0.5f, 0f);

	TextMeshPro label;
	Vector2Int coordinates = new Vector2Int();
	GridManager gridManager;

	void Awake()
	{
		gridManager = FindObjectOfType<GridManager>();

		// Get TextMeshPro label
		label = GetComponent<TextMeshPro>();

		if (!label)
		{
			Debug.LogError("No coordinate label found!");
		} else
		{
			label.enabled = false;
		}

		DisplayCoordinates();
	}

	// Update is called once per frame
	void Update()
	{
		//Check if game is running
		if (!Application.isPlaying)
		{
			DisplayCoordinates();
			UpdateObjectName();
			label.enabled = true;
		}

		SetLabelColor();
		ToggleLabels();
	}

	// Function for setting the color of the coordinate label
	void SetLabelColor()
	{
		// Check if gridmanager exists
		if (gridManager == null) { return; }

		Node node = gridManager.GetNode(coordinates);

		// Check if node is not null;
		if (node == null) { return; }

		if(!node.isWalkable)
		{
			label.color = blockedColor;
		}
		else if (node.isPath)
		{
			label.color = pathColor;
		}
		else if (node.isExplored)
		{
			label.color = exploredColor;
		} else
		{
			label.color = defaultColor;
		}
	}

	// Function for displaying coordinates of the parent in a label
	void DisplayCoordinates()
	{
		if(gridManager == null) { return; }

		// Get parent coordinates
		coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
		coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

		label.text = coordinates.x + "," + coordinates.y;
	}

	// Toggle function for coordinate label
	// Key is C
	void ToggleLabels()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			label.enabled = !label.IsActive();
		}
	}

	// Function that updates the object name in the editor to match their coordinates
	void UpdateObjectName()
	{
		transform.parent.name = coordinates.ToString();
	}
}
