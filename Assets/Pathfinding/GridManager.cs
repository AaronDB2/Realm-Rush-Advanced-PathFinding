using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Size of the grid")]
	Vector2 gridSize;

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
}
