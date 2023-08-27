using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Size of the grid")]
	Vector2 gridSize;

	[SerializeField]
	[Tooltip("World grid size. Should match UnityEditor snap settings.")]
	int unityGridSize = 10;
	public int UnityGridSize { get { return unityGridSize; } }

	// Dictionary were key = 2D Coordinates and value = Node object
	Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
	public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

	void Awake()
	{
		CreateGrid();
	}

	// Getter function for getting the node from the grid at given key (coordinates)
	public Node GetNode(Vector2Int coordinates)
	{
		if(grid.ContainsKey(coordinates))
		{
			return grid[coordinates];
		}

		return null;
	}

	// Function for creating a grid of nodes
	void CreateGrid()
	{
		for(int x = 0; x < gridSize.x; x++)
		{
			for(int y = 0; y < gridSize.y; y++)
			{
				// Create node at current coordinates
				Vector2Int coordinates = new Vector2Int(x, y);
				grid.Add(coordinates, new Node(coordinates, true));
			}
		}
	}

	// Function for setting walkable flag for given coordinates
	public void BlockNode(Vector2Int coordinates)
	{
		if(grid.ContainsKey(coordinates))
		{
			grid[coordinates].isWalkable = false;
		}
	}

	// Function for converting position to coords
	public Vector2Int GetCoordinatesFromPosition(Vector3 position)
	{
		Vector2Int coordinates = new Vector2Int(); 

		coordinates.x = Mathf.RoundToInt(position.x / UnityGridSize);
		coordinates.y = Mathf.RoundToInt(position.z / UnityGridSize);

		return coordinates;
	}

	// Function for converting coords to position
	public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
	{
		Vector3 position = new Vector3();

		position.x = coordinates.x * unityGridSize;
		position.z = coordinates.y * unityGridSize;

		return position;
	}

	// Function for reseting nodes
	public void ResetNodes()
	{
		foreach(KeyValuePair<Vector2Int, Node> entry in grid)
		{
			entry.Value.connectedTo = null;
			entry.Value.isExplored = false;
			entry.Value.isPath = false;
		}
	}
}
