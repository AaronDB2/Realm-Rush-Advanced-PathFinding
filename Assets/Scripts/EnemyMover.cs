using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField]
    [Tooltip("List of waypoints to follow")]
    List<Tile> path = new List<Tile>();

    [SerializeField]
    [Tooltip("How fast the enemy moves")]
    [Range(0f, 5f)]
    float speed = 1f;

    Enemy enemy;

    // OnEnable is called when game object is enabled
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

	void Start()
	{
		enemy = GetComponent<Enemy>();
	}

	// Function for finding the path
	void FindPath()
    {
        path.Clear();

        // Find gameobject with path tag
        GameObject parent = GameObject.FindGameObjectWithTag("Path"); 

        foreach (Transform child in parent.transform)
        {
            Tile waypoint = child.GetComponent<Tile>();

            if (waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }

    // Set the gameobject to the first waypoint in path
    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
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
		foreach (Tile waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
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
