using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer[] waypointSprites = gameObject.transform.GetComponentsInChildren<SpriteRenderer>();
        foreach(var waypoint in waypointSprites)
        {
            waypoint.enabled = false;
        }
    }

}
