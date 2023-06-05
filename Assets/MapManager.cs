using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static int ChosenMap = 0;
    public GameObject[] maps;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name.Equals("game"))
        {
            Instantiate(maps[ChosenMap], new Vector3(0,0,0), Quaternion.identity);
        }
    }
}
