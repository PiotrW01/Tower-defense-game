using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] waypoints;
    public static float maxDistance = 0f;

    void Awake()
    {
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }

        for (int i = 0; i < waypoints.Length-1; i++)
        {
            maxDistance += Vector2.Distance(waypoints[i].transform.localPosition, waypoints[i + 1].transform.localPosition);
        }

    }
}
