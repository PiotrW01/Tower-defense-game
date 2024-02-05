using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Waypoints : MonoBehaviour
{
    public GameObject startingPoint;
    public GameObject endPoint;
    private static LayerMask mask;
    private static Transform colliders;
    public static Vector2[] waypoints;
    private static EdgeCollider2D edgeCollider;

    private void Start()
    {
        mask = LayerMask.NameToLayer("Path");
        edgeCollider = GetComponent<EdgeCollider2D>();
        colliders = transform.parent.Find("Colliders3D");
        StartCoroutine(SetWaypointsAndColliders());
    }

    IEnumerator SetWaypointsAndColliders()
    {
        yield return null;
        yield return null;
        yield return null;
        waypoints = edgeCollider.points;
        GameObject inst = new GameObject("coll");
        inst.layer = mask;
        for (int i = 0; i < edgeCollider.pointCount - 1; i++)
        {
            var obj = Instantiate(inst, colliders);
            Vector3 pos = Vector3.Lerp(new Vector3(waypoints[i + 1].x, 0, waypoints[i + 1].y), new Vector3(waypoints[i].x, 0, waypoints[i].y), 0.5f);
            obj.transform.position = pos;
            obj.transform.LookAt(new Vector3(waypoints[i + 1].x, 0, waypoints[i + 1].y));
            var comp = obj.AddComponent<BoxCollider>();
            comp.size = new Vector3(0.7f, 0.5f, 2 * Vector3.Distance(pos, new Vector3(waypoints[i + 1].x, 0, waypoints[i + 1].y)));
            var rb = obj.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        Destroy(inst);

        Vector3 position = new Vector3(waypoints[0].x, 0, waypoints[0].y);
        if (startingPoint != null)
        {
            GameObject obj = Instantiate(startingPoint, transform);
            obj.transform.position = position;
            Vector3 localPos = obj.transform.localPosition;
            obj.transform.LookAt(new Vector3(waypoints[1].x, 0f, waypoints[1].y));
            obj.transform.Rotate(Vector3.left, 90);
            localPos.z = -0.288f;
            obj.transform.localPosition = localPos;
        }

        if (endPoint != null)
        {
            position = new Vector3(waypoints[waypoints.Length - 1].x, 0f, waypoints[waypoints.Length - 1].y);
            GameObject obj = Instantiate(endPoint, transform);
            obj.transform.position = position;
            Vector3 localPos = obj.transform.localPosition;

            obj.transform.LookAt(new Vector3(waypoints[waypoints.Length - 2].x, 0f, waypoints[waypoints.Length - 2].y));
            obj.transform.Rotate(Vector3.left, 90);
            localPos.z = -0.288f;
            obj.transform.localPosition = localPos;
        }
    }
}
