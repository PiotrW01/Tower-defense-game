using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class MapLoader : MonoBehaviour
{
    public MapData data;

    private void Start()
    {
        if (data == null)
        {
            transform.Find("PathPrefab").GetComponent<PathShapeController>().enabled = true;
            return;
        }
        var terrainSprite = transform.Find("TerrainSprite").GetComponent<SpriteRenderer>();
        terrainSprite.size = data.size;
        var pathShape = transform.Find("PathPrefab").GetComponent<SpriteShapeController>();

        pathShape.spline.Clear();
        for (int i = 0; i < data.SplinePos.Length; i++)
        {
            pathShape.spline.InsertPointAt(i, data.SplinePos[i]);
            pathShape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            pathShape.spline.SetLeftTangent(i, data.TangentPos[2 * i]);
            pathShape.spline.SetRightTangent(i, data.TangentPos[2 * i + 1]);
        }
        transform.Find("PathPrefab").GetComponent<PathShapeController>().enabled = true;

    }
}