using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField]
    [Tooltip("List of waypoints to follow")]
    List<Waypoint> path = new List<Waypoint>();

    [SerializeField]
    [Tooltip("How fast the enemy moves")]
    [Range(0f, 5f)]
    float speed = 1f;

    // OnEnable is called when game object is enabled
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    // Function for finding the path
    void FindPath()
    {
        path.Clear();

        // Find gameobject with path tag
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path"); 

        // Loop through the waypoints array and add the waypoints to the List
        foreach (GameObject waypoint in waypoints)
        {
            path.Add(waypoint.GetComponent<Waypoint>());
        }
    }

    // Set the gameobject to the first waypoint in path
    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    // Coroutine Function for letting object follow the path
    IEnumerator FollowPath() 
    {
		foreach (Waypoint waypoint in path)
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

        gameObject.SetActive(false);
    }
}
