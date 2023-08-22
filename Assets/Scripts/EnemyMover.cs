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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPath());
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
    }
}
