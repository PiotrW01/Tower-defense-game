using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class MapLoader : MonoBehaviour
{
    public MapData data;

    private void Start()
    {
        data = MapPreview.ChosenMapData;
        if (data == null)
        {
            transform.Find("PathPrefab").GetComponent<PathShapeController>().enabled = true;
            return;
        }

        // Set the shape of the path
        var pathShape = transform.Find("PathPrefab").GetComponent<SpriteShapeController>();
        pathShape.spline.Clear();
        for (int i = 0; i < data.SplinePos.Length; i++)
        {
            pathShape.spline.InsertPointAt(i, data.SplinePos[i]);
            pathShape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            pathShape.spline.SetLeftTangent(i, data.TangentPos[2 * i]);
            pathShape.spline.SetRightTangent(i, data.TangentPos[2 * i + 1]);
        }

        // Set the terrain sprite and size
        var terrainSprite = transform.Find("TerrainSprite").GetComponent<SpriteRenderer>();
        terrainSprite.size = data.size;
        terrainSprite.sprite = TerrainDictionary.Sprites[data.terrainType];


        if(SceneManager.GetActiveScene().name == "game")
        {
            terrainSprite.GetComponent<BoxCollider>().size = new Vector3(terrainSprite.size.x, terrainSprite.size.y, 0.2f);
            // Instantiate Environment objects
            var envContainer = transform.Find("Environment");
            for (int i = 0; i < data.EnvObjectsPos.Length; i++)
            {
                Instantiate(EnvDictionary.Objects[data.EnvObjectsType[i]], new Vector3(data.EnvObjectsPos[i].x,0, data.EnvObjectsPos[i].y) , Quaternion.Euler(90,0,0), envContainer);
            }
            var path = transform.Find("PathPrefab");
            path.GetComponent<PathShapeController>().enabled = false;
            path.GetComponent<EdgeCollider2D>().enabled = true;
            path.GetComponent<Waypoints>().enabled = true;
        }
        else
        {
            // Instantiate Environment objects
            var envContainer = transform.Find("Environment");
            for (int i = 0; i < data.EnvObjectsPos.Length; i++)
            {
                var newObj = Instantiate(EnvDictionary.Objects[data.EnvObjectsType[i]], data.EnvObjectsPos[i], Quaternion.identity, envContainer);
                newObj.AddComponent<ObjectHandler>();
                newObj.GetComponent<ObjectHandler>().objectType = data.EnvObjectsType[i];
                MapEditor.envObjects.Add(newObj);
            }
            transform.Find("PathPrefab").GetComponent<PathShapeController>().enabled = true;
        }
    }
}
