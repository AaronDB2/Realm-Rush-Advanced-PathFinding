using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
	TextMeshPro label;
	Vector2Int coordinates = new Vector2Int();

	void Awake()
	{
		// Get TextMeshPro label
		label = GetComponent<TextMeshPro>();

		DisplayCoordinates();
	}

	// Update is called once per frame
	void Update()
    {
        //Check if game is running
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
			UpdateObjectName();
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

	// Function that updates the object name in the editor to match their coordinates
	void UpdateObjectName()
	{
		transform.parent.name = coordinates.ToString();
	}
}
