using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.U2D;

public class SplinePoint : MonoBehaviour
{
    public int splineIndex;
    public bool isSelected = false;
    public GameObject tangentCircle;
    public TangentPoint leftTangent;
    public TangentPoint rightTangent;
    private float snapDistance = 0.4f;
    private SpriteShapeController controller;
    private LineRenderer snapLineRenderer;

    private void Awake()
    {
        snapLineRenderer = GetComponent<LineRenderer>();
        controller = PathShapeController.controller;
    }

    private void Start()
    {
        leftTangent = Instantiate(tangentCircle, transform).GetComponent<TangentPoint>();
        rightTangent = Instantiate(tangentCircle, transform).GetComponent<TangentPoint>();
        leftTangent.TangentSide = Tangent.LEFT;
        leftTangent.splineParent = GetComponent<SplinePoint>();
        rightTangent.TangentSide = Tangent.RIGHT;
        rightTangent.splineParent = GetComponent<SplinePoint>();
        if(splineIndex == controller.spline.GetPointCount() - 1) rightTangent.gameObject.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        if(isSelected) 
        {
            if (Input.GetKeyDown(KeyCode.Delete) && controller.spline.GetPointCount() > 2) Destroy(gameObject);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
            if (Input.GetKey(KeyCode.LeftShift)) SnapToSpline((mousePos - controller.transform.position));
            else
            {
                controller.spline.SetPosition(splineIndex, 
                    (mousePos - controller.transform.position));
                snapLineRenderer.enabled = false;
            } 
        } else
        {
            transform.position = controller.spline.GetPosition(splineIndex) + controller.transform.position;
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
        controller.BakeMesh();
        controller.BakeCollider();
    }

    private void OnDestroy()
    {
        PathShapeController.RemoveSpline(splineIndex);
    }

    private void SnapToSpline(Vector3 localPos)
    {
        float distance;
        float minDistance = float.MaxValue;
        float snapPosition = 0;
        bool horizontal = false;
        int index = -1;

        for (int i = 0; i < controller.spline.GetPointCount(); i++)
        {
            if (i == splineIndex) continue;
            Vector3 splinePos = controller.spline.GetPosition(i);
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

        controller.spline.SetPosition(splineIndex, localPos);
    }
}
