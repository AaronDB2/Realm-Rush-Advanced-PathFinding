using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField]
	[Tooltip("Start coords")]
	Vector2Int startCoordinates;

    [SerializeField]
    [Tooltip("Destination coords")]
    Vector2Int destinationCoordinates;

    Node startNode;
    Node destinationNode;

    // Node that is currently searched
    Node currentSearchNode;

    // Dictionary for storing all the reached nodes for path finding
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    //Queue of nodes to still explore
    Queue<Node> frontier = new Queue<Node>();

    // Direction to search in order
    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

	void Awake()
	{
		gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null)
        {
            grid = gridManager.Grid;
        }
	}

	// Start is called before the first frame update
	void Start()
    {
        startNode = gridManager.Grid[startCoordinates];
        destinationNode = gridManager.Grid[destinationCoordinates];

		BreadthFirstSearch();
        BuildPath();
	}

    // Function for exploring neighbor nodes
    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        // Use directions to find neighbors
        foreach(Vector2Int direction in directions) 
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;

            // Check if neighbor exists at given coords
            if(grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
			}
        }

        // Loop through all the neighbors
        foreach(Node neighbor in neighbors)
        {
            // Check if neighbor has not already been found
            // and neigbor is walkable
            if(!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

	// Function for BreadthFirstSearch algorithm (path finding)
	void BreadthFirstSearch()
    {
        bool isRunning = true;

        frontier.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);

        // Loop through the entire frontier queue as long as path is not found
        while(frontier.Count > 0 && isRunning ) 
        { 
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();

            // If desination is reached stop the while loop
            if(currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    // Function for building the path
    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath= true;

        // From destination node find all the connected nodes and add them to the path
        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;  
        }

        path.Reverse();

        return path;
    }

}
