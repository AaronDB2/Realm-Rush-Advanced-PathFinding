using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Default color of the coordinate label")]
	Color defaultColor = Color.white;

	[SerializeField]
	[Tooltip("Color of the coordinate label when towers cant be build")]
	Color blockedColor = Color.grey;

	TextMeshPro label;
	Vector2Int coordinates = new Vector2Int();
	Waypoint waypoint;

	void Awake()
	{
		// Get TextMeshPro label
		label = GetComponent<TextMeshPro>();

		if (!label)
		{
			Debug.LogError("No coordinate label found!");
		} else
		{
			label.enabled = false;
		}

		waypoint = GetComponentInParent<Waypoint>();
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
		}

		ColorCoordinates();
		ToggleLabels();
	}

	// Function for setting the color of the coordinate label
	void ColorCoordinates()
	{
		if (waypoint.IsPlaceable)
		{
			label.color = defaultColor;
		} else
		{
			label.color = blockedColor;
		}
	}

	// Function for displaying coordinates of the parent in a label
	void DisplayCoordinates()
	{
		// Get parent coordinates
		coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
		coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

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
