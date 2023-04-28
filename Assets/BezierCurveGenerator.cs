using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class BezierCurveGenerator : MonoBehaviour
{
    [SerializeField] private GameObject circlePrefab;
    [SerializeField][Range(0.01f, 0.4f)] private float curveResolution = 0.1f;

    private List<GameObject> circles = new List<GameObject>();
    private Transform[] objects;

    public Transform objectsParent;
    public bool isRunning = false;

    public void ClearPath()
    {
        if(circles.Count == 0) return;
        foreach (GameObject circle in circles)
        {
            DestroyImmediate(circle);
        }
        circles.Clear();
    }
    public void Generate()
    {
        objects = new Transform[objectsParent.childCount];

        for (int i = 0; i< objectsParent.childCount; i++)
        {
            objects[i] = objectsParent.GetChild(i).transform;
        }

        GenerateCurve(objects);
    }
    public void GenerateCurve(params Transform[] objects)
    {
        if (objects.Length < 3)
        {
            Debug.LogWarning("Bezier curve generation requires at least 3 objects.");
            return;
        }

        // Clear existing circles
        foreach (GameObject circle in circles)
        {
            DestroyImmediate(circle);
        }
        circles.Clear();

        // Create curve
        List<Vector2> points = new List<Vector2>();
        foreach (Transform obj in objects)
        {
            points.Add(obj.position);
        }

        for (float t = 0f; t <= 1f; t += curveResolution)
        {
            Vector2 point = CalculateBezierPoint(t, points);
            GameObject circle = Instantiate(circlePrefab, point, Quaternion.identity);
            var sv = SceneVisibilityManager.instance;
            sv.DisablePicking(circle, false);
            circles.Add(circle);
        }
    }

    private Vector2 CalculateBezierPoint(float t, List<Vector2> points)
    {
        int degree = points.Count - 1;

        Vector2 point = Vector2.zero;
        for (int i = 0; i <= degree; i++)
        {
            point += BinomialCoefficient(degree, i) * Mathf.Pow(1 - t, degree - i) * Mathf.Pow(t, i) * points[i];
        }

        return point;
    }

    private int BinomialCoefficient(int n, int k)
    {
        if (k < 0 || k > n) return 0;
        if (k == 0 || k == n) return 1;

        int coefficient = 1;
        for (int i = 1; i <= k; i++)
        {
            coefficient *= n - (k - i);
            coefficient /= i;
        }

        return coefficient;
    }

}