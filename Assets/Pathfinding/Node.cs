using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    // 2D Coordinates  of the node
    public Vector2Int coordinates;
    // Is walkable flag to check if node is walkable
    public bool isWalkable;
    // Is explored flag to check if node is explored
    public bool isExplored;
    // Is path flag to check if node is a path
    public bool isPath;
    // Connected node
    public Node connectedTo;

    // Constructor
    public Node(Vector2Int coordinates, bool isWalkable)
	{
		this.coordinates = coordinates;
		this.isWalkable = isWalkable;
	}
}
