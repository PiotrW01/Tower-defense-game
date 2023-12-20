using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    //public static Transform[] waypoints;
    public GameObject WaypointPrefab;
    private bool showWaypoints = false;
    public static Vector2[] waypoints;
    private static EdgeCollider2D edgeCollider;
    //public static Vector3 StartPos;

    void Awake()
    {
/*        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            // Map the positions of the waypoints gameobjects in PathWayPoints to the waypoints array
            waypoints[i] = transform.GetChild(i);
        }

*//*        for (int i = 0; i < waypoints.Length-1; i++)
        {
            //maxDistance += Vector2.Distance(waypoints[i].transform.localPosition, waypoints[i + 1].transform.localPosition);
        }*//*

        StartPos = waypoints[0].position;*/
    }

    private void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        UpdateWayPoints();
    }

    public void ToggleWaypoints()
    {
        showWaypoints = !showWaypoints;
        if(showWaypoints)
        {
            foreach(var waypoint in waypoints)
            {
                Instantiate(WaypointPrefab, new Vector3(waypoint.x + transform.position.x, 
                    waypoint.y + transform.position.y, 0), Quaternion.identity, transform);
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }



    public void UpdateWayPoints()
    {
        waypoints = edgeCollider.points;
        for(int i =  0; i < waypoints.Length; i++)
        {
            waypoints[i] *= transform.lossyScale;
        }
    }
}
