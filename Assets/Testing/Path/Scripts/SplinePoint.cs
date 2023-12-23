using UnityEngine;

public class SplinePoint : MonoBehaviour
{
    public bool loadedFromPrefab = false;
    public int splineIndex;
    public bool isSelected = false;
    public GameObject tangentCircle;
    public TangentPoint leftTangent;
    public TangentPoint rightTangent;
    private float snapDistance = 0.4f;
    public PathShapeController controller;
    private LineRenderer snapLineRenderer;

    private void Awake()
    {
        snapLineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        if(splineIndex != 0)
        {
            leftTangent = Instantiate(tangentCircle, transform).GetComponent<TangentPoint>();
            leftTangent.TangentSide = Tangent.LEFT;
            leftTangent.controller = controller.shapeController;
            leftTangent.splineParent = GetComponent<SplinePoint>();
        }
        rightTangent = Instantiate(tangentCircle, transform).GetComponent<TangentPoint>();
        rightTangent.TangentSide = Tangent.RIGHT;
        rightTangent.splineParent = GetComponent<SplinePoint>();
        rightTangent.controller = controller.shapeController;
        if(splineIndex == controller.shapeController.spline.GetPointCount() - 1) rightTangent.gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = true;
    }



    // Update is called once per frame
    void Update()
    {
        if(isSelected) 
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
            if (Input.GetKey(KeyCode.LeftShift)) SnapToSpline((mousePos - controller.transform.position));
            else
            {
                controller.shapeController.spline.SetPosition(splineIndex, 
                    (mousePos - controller.transform.position));
                snapLineRenderer.enabled = false;
            } 
            if (Input.GetKeyDown(KeyCode.Delete) && controller.shapeController.spline.GetPointCount() > 2)
            {
                Destroy(gameObject);
                controller.RemoveSpline(splineIndex);
            }
        } else
        {
            transform.position = controller.shapeController.spline.GetPosition(splineIndex) + controller.transform.position;
        }
    }

    private void OnMouseDown()
    {
        isSelected = true;
        PathShapeController.isSplineSelected = isSelected;
    }

    private void OnMouseUp()
    {
        isSelected = false;
        PathShapeController.isSplineSelected = isSelected;
        snapLineRenderer.enabled = false;
        controller.shapeController.BakeMesh();
        controller.shapeController.BakeCollider();
    }

    private void SnapToSpline(Vector3 localPos)
    {
        float distance;
        float minDistance = float.MaxValue;
        float snapPosition = 0;
        bool horizontal = false;
        int index = -1;

        for (int i = 0; i < controller.shapeController.spline.GetPointCount(); i++)
        {
            if (i == splineIndex) continue;
            Vector3 splinePos = controller.shapeController.spline.GetPosition(i);
            //horizontal check
            distance = Mathf.Abs(splinePos.y - localPos.y);
            if (distance <= snapDistance)
            {
                if(distance < minDistance)
                {
                    horizontal = true;
                    minDistance = distance;
                    index = i;
                    snapPosition = splinePos.y;
                }
            }
            //vertical check
            distance = Mathf.Abs(splinePos.x - localPos.x);
            if (distance <= snapDistance)
            {
                if (distance < minDistance)
                {
                    horizontal = false;
                    minDistance = distance;
                    index = i;
                    snapPosition = splinePos.x;
                }
            }
        }

        if (index != -1)
        {
            snapLineRenderer.enabled = true;
            if (horizontal)
            {
                localPos.y = snapPosition;
                var pos = transform.position;
                pos.y = snapPosition + controller.transform.position.y;
                transform.position = pos;
                snapLineRenderer.SetPositions(new Vector3[] { new Vector3(-10000, pos.y), new Vector3(10000, pos.y) });
            }
            else
            {
                localPos.x = snapPosition;
                var pos = transform.position;
                pos.x = snapPosition + controller.transform.position.x;
                transform.position = pos;
                snapLineRenderer.SetPositions(new Vector3[] { new Vector3(pos.x, -10000), new Vector3(pos.x, 10000) });
            }
        }
        else snapLineRenderer.enabled = false;

        controller.shapeController.spline.SetPosition(splineIndex, localPos);
    }
}
