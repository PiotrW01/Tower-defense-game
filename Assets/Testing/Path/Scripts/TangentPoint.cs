using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class TangentPoint : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public SplinePoint splineParent; 
    public Tangent TangentSide;
    private bool isSelected = false;
    private Spline spline;
    private float snapDistance = 0.4f;

    private void Awake()
    {
        spline = PathShapeController.controller.spline;
    }

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if(TangentSide == Tangent.LEFT)
        {
            if (splineParent.splineIndex == 0)
            {
                Destroy(gameObject);
                return;
            }
            Vector3 newPos = spline.GetPosition(splineParent.splineIndex)
                    - spline.GetPosition(splineParent.splineIndex - 1);
            spline.SetLeftTangent(splineParent.splineIndex, -newPos / 4);
            transform.position = spline.GetLeftTangent(splineParent.splineIndex);
        }
    }

    void Update()
    {
        Vector3 parentPos = spline.GetPosition(splineParent.splineIndex);
        if (isSelected)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
            Vector3 tangentPos = (mousePos - PathShapeController.controller.transform.position) - parentPos;

            if (Input.GetKey(KeyCode.LeftShift)) SnapToParentSpline(tangentPos, parentPos);
            else
            {
                if (TangentSide == Tangent.LEFT)
                {
                    spline.SetLeftTangent(splineParent.splineIndex, tangentPos);
                } 
                else
                {
                    spline.SetRightTangent(
                        splineParent.splineIndex, tangentPos);
                }              
            }
        } else
        {
            if(TangentSide == Tangent.LEFT)
            {
                transform.position = PathShapeController.controller.transform.position 
                    + (spline.GetLeftTangent(splineParent.splineIndex) + parentPos);
            }
            else
            {
                transform.position = PathShapeController.controller.transform.position
                    + (spline.GetRightTangent(splineParent.splineIndex) + parentPos);
            }
        }
        lineRenderer.SetPositions(
            new Vector3[] {
                transform.position, 
                PathShapeController.controller.transform.position + parentPos
            });

    }
    private void OnMouseDown()
    {
        isSelected = true;
    }
    private void OnMouseUp()
    {
        isSelected = false;
    }

    private void OnEnable()
    {
        if(TangentSide == Tangent.RIGHT)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector3 newPos = mousePos - PathShapeController.controller.transform.position 
                - spline.GetPosition(splineParent.splineIndex);
                
            spline.SetRightTangent(splineParent.splineIndex, newPos / 4);
        }
    }

    private void SnapToParentSpline(Vector3 tangentPos, Vector3 parentPos)
    {
        float horizontalDistance = Mathf.Abs(tangentPos.y);
        float verticalDistance = Mathf.Abs(tangentPos.x);
        //check snaps
        if(horizontalDistance <= snapDistance && horizontalDistance < verticalDistance)
        {
            tangentPos.y = 0;
        }
        if(verticalDistance <= snapDistance && verticalDistance <= horizontalDistance)
        {
            tangentPos.x = 0;
        }

        //assign positions
        if(TangentSide == Tangent.LEFT) spline.SetLeftTangent(splineParent.splineIndex, tangentPos);
        else spline.SetRightTangent(splineParent.splineIndex, tangentPos);
        transform.position = PathShapeController.controller.transform.position
                + (tangentPos + parentPos);
    }
}




public enum Tangent
{
    LEFT = 0, RIGHT = 1,
}
