using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static int ChosenMap = 0;
    public GameObject[] maps;

    private void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
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