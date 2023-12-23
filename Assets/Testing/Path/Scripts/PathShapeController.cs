using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class PathShapeController : MonoBehaviour
{
    public SpriteShapeController shapeController;
    public GameObject SplinePrefab;
    public static bool isSplineSelected = false;
    private List<SplinePoint> SplinePoints = new List<SplinePoint>();
    private Vector3 prevPos;
    private Vector3 nextPos;

    private void Awake()
    {

        if (SceneManager.GetActiveScene().name == "game")
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            GetComponent<EdgeCollider2D>().enabled = true;
            this.enabled = false;
        }
    }

    void Start()
    {
        prevPos = transform.position;
        nextPos = transform.position;
        shapeController = GetComponent<SpriteShapeController>();
        

        if (shapeController.spline.GetPointCount() != 0)
        {
            for (int i = shapeController.spline.GetPointCount() - 1; i >= 0; i--)
            {
                Vector3 pos = (shapeController.transform.position + shapeController.spline.GetPosition(i));
                SplinePoint obj = Instantiate(SplinePrefab, pos, Quaternion.identity, transform).GetComponent<SplinePoint>();
                obj.controller = this;
                obj.splineIndex = shapeController.spline.GetPointCount() - i - 1;
                obj.loadedFromPrefab = true;
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

    public void RemoveSpline(int splineIndex)
    {
        try {
        if (splineIndex == SplinePoints.Count - 1) SplinePoints[splineIndex - 1].rightTangent.gameObject.SetActive(false);
        } catch { }
        shapeController.spline.RemovePointAt(splineIndex);
        SplinePoints.RemoveAt(splineIndex);
        int i = 0;
        SplinePoints.ForEach((spline) => { spline.splineIndex = i++; });
    }

    private void CreateSplinePoint()
    {
        int pointCount = shapeController.spline.GetPointCount();
        Vector3 lastPoint = shapeController.spline.GetPosition(pointCount - 1) + shapeController.transform.position;
        Vector3 newPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPoint.z = 0;
        if (Vector3.Distance(lastPoint, newPoint) > 1f)
        {
            shapeController.spline.InsertPointAt(pointCount, newPoint - shapeController.transform.position);
            shapeController.spline.SetTangentMode(pointCount, ShapeTangentMode.Continuous);
            shapeController.BakeMesh();
            shapeController.BakeCollider();
            SplinePoint obj = Instantiate(SplinePrefab, newPoint, Quaternion.identity, transform).GetComponent<SplinePoint>();
            obj.splineIndex = pointCount;
            obj.controller = this;
            SplinePoints.Add(obj);
            SplinePoints[pointCount - 1].rightTangent.Enable();
        }
    }
}
