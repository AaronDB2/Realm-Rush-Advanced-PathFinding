using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField]
    [Tooltip("List of waypoints to follow")]
    List<Waypoint> path = new List<Waypoint>();

    // Start is called before the first frame update
    void Start()
    {
        PrintWaypointName();
    }

    // Function for printing the full path name into the console
    void PrintWaypointName() 
    { 
        foreach(Waypoint waypoint in path)
        {
            Debug.Log(waypoint.name);
        }
    }
}
