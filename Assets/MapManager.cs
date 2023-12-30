using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class MapManager : MonoBehaviour
{
    public static int ChosenMap = 0;
    public static MapData mapData;
    public GameObject[] maps;

    // load the chosen map from the game view using MapManager.mapData
    private void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;

        mapData = FileManager.LoadMapData("");
        Debug.Log(mapData.EnvObjectsType[0]);
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (next.name.Equals("game"))
        {
            try
            {
                Instantiate(maps[ChosenMap], new Vector3(0, 0, 0), Quaternion.identity);
            } catch
            {
                Instantiate(maps[0], new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
    }
}