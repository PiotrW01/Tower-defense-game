using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PathShapeController : MonoBehaviour
{
    public static SpriteShapeController controller;
    public GameObject SplinePrefab;
    public static bool isSplineSelected;
    private static List<SplinePoint> SplinePoints = new List<SplinePoint>();
    //private Vector3 dragOffset;
    private Vector3 prevPos;
    private Vector3 nextPos;

    void Start()
    {
        isSplineSelected = false;
        prevPos = transform.position;
        nextPos = transform.position;
        controller = GetComponent<SpriteShapeController>();
        if (controller.spline.GetPointCount() != 0)
        {
            for (int i = 0; i < controller.spline.GetPointCount(); i++)
            {
                Vector3 pos = (controller.transform.position + controller.spline.GetPosition(i));
                SplinePoint obj = Instantiate(SplinePrefab, pos, Quaternion.identity).GetComponent<SplinePoint>();
                obj.splineIndex = i;
                SplinePoints.Add(obj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update path scale for correct Spline position conversion
        if (Input.GetKeyDown(KeyCode.Z)) { transform.position = prevPos; };
        if (Input.GetKeyDown(KeyCode.Y)) { transform.position = nextPos; };
        if (Input.GetMouseButtonDown(1) && !isSplineSelected)
        {
            CreateSplinePoint();
        }
    }

    public static void RemoveSpline(int splineIndex)
    {
        try {
        if (splineIndex == SplinePoints.Count - 1) SplinePoints[splineIndex - 1].rightTangent.gameObject.SetActive(false);
        } catch { }
        controller.spline.RemovePointAt(splineIndex);
        SplinePoints.RemoveAt(splineIndex);
        int i = 0;
        SplinePoints.ForEach((spline) => { spline.splineIndex = i++; });
    }

    private void CreateSplinePoint()
    {
        int pointCount = controller.spline.GetPointCount();
        Vector3 lastPoint = controller.spline.GetPosition(pointCount - 1) + controller.transform.position;
        Vector3 newPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPoint.z = 0;
        if (Vector3.Distance(lastPoint, newPoint) > 3f)
        {
            controller.spline.InsertPointAt(pointCount, newPoint - controller.transform.position);
            controller.spline.SetTangentMode(pointCount, ShapeTangentMode.Continuous);
            controller.BakeMesh();
            controller.BakeCollider();
            SplinePoint obj = Instantiate(SplinePrefab, newPoint, Quaternion.identity).GetComponent<SplinePoint>();
            obj.splineIndex = pointCount;
            SplinePoints.Add(obj);
            SplinePoints[pointCount - 1].rightTangent.gameObject.SetActive(true);
        }
    }
}
