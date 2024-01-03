using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Waypoints : MonoBehaviour
{
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
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Debug.Log(edgeCollider.points.Length);
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
    }
}
