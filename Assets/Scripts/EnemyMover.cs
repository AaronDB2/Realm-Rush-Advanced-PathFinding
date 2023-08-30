using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How fast the enemy moves")]
    [Range(0f, 5f)]

	List<Node> path = new List<Node>();
	float speed = 1f;
    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;

    // OnEnable is called when game object is enabled
    void OnEnable()
    {
		ReturnToStart();
		RecalculatePath(true);
    }

	void Awake()
	{
		enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder= FindObjectOfType<Pathfinder>();
	}

	// Function for finding the path
	void RecalculatePath(bool resetPath)
    {
		Vector2Int coordinates = new Vector2Int();

        if(resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        } else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
		StartCoroutine(FollowPath());
	}

    // Set the gameobject to the first waypoint in path
    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    // Function that handles what happens if an enemy gets to the end of the path
	void FinishPath()
	{
		enemy.StealGold();
		gameObject.SetActive(false);
	}

	// Coroutine Function for letting object follow the path
	IEnumerator FollowPath() 
    {
		for(int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
			float travelPercent = 0f;

            transform.LookAt(endPosition);

            // Run while travelPercent is not 100%
            while(travelPercent < 1f) 
            {
                travelPercent += Time.deltaTime * speed;

				transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
			}

        }

        FinishPath();
    }
}
