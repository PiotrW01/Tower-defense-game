using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using System.Text;
using System.Runtime.InteropServices;

public class MapEditor : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject loginCredentials;
    public GameObject MapPrefab;
    
    public SpriteRenderer TerrainRenderer;
    //MapInformation mapInformation;

    public TMP_InputField playerMoney;
    public TMP_InputField mapName;
    public TMP_InputField mapWidth;
    public TMP_InputField mapHeight;
    public TMP_InputField playerName;
    public TMP_InputField playerPassword;
    public TMP_Dropdown terrainDropdown;

    void Start()
    {
        //mapInformation = GameObject.FindGameObjectWithTag("map").GetComponent<MapInformation>();
        LoadTerrainOptions();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) ToggleOptionsMenu();
    }

    public void LoadTerrainOptions()
    {
        List<TMP_Dropdown.OptionData> options = new();
        foreach (var key in TerrainDictionary.Sprites.Keys)
        {
            options.Add(new TMP_Dropdown.OptionData(key.ToString()));
        }
        terrainDropdown.value = 0;
        terrainDropdown.AddOptions(options);
    }

    public void ChangeTerrain()
    {
        Terrain terrainType;
        Enum.TryParse(terrainDropdown.options[terrainDropdown.value].text, out terrainType);
        Debug.Log(terrainType);
        TerrainRenderer.sprite = TerrainDictionary.Sprites[terrainType];
    }

    public void ToggleOptionsMenu()
    {
        if (optionsMenu.activeInHierarchy)
        {
            optionsMenu.SetActive(false);
        } else
        {
            optionsMenu.SetActive(true);
        }
    }

    public void SaveMap()
    {
        if (mapName.text.Length < 3) return;
        var shapeController = GameObject.FindGameObjectWithTag("map")
            .transform.Find("PathPrefab").GetComponent<SpriteShapeController>().spline;
        var data = new MapData();
        int pointCount = shapeController.GetPointCount();
        data.name = mapName.text;
        data.SplinePos = new Vector2[pointCount];
        data.TangentPos = new Vector2[pointCount * 2];
        data.terrainType = (Terrain)Enum.Parse(typeof(Terrain), terrainDropdown.options[terrainDropdown.value].text);
        data.size = TerrainRenderer.size;

        for (int i = 0; i < pointCount; i++)
        {
            data.SplinePos[i] = shapeController.GetPosition(i);
            data.TangentPos[2 * i] = shapeController.GetLeftTangent(i);
            data.TangentPos[2 * i + 1] = shapeController.GetRightTangent(i);
            Debug.Log(i + " " + shapeController.GetPosition(i));
        }

        FileManager.SaveMapData(data);
    }

/*    public void LoadMap()
    {
        
        MapData data = FileManager.LoadMapData("test");
        StartCoroutine(MapLoader(data));
    }

    IEnumerator MapLoader(MapData data)
    {
        Destroy(GameObject.FindGameObjectWithTag("map"));
        yield return new WaitForFixedUpdate();
        var newMap = Instantiate(MapPrefab);
        TerrainRenderer = newMap.transform.Find("TerrainSprite").GetComponent<SpriteRenderer>();
        newMap.GetComponent<MapLoader>().data = data;
        yield break;
    }*/

    public void GoToMenu()
    {
        SoundManager.Instance.PlayButtonClick();
        SceneManager.LoadScene("menu");
    }

    public void SetMapSize()
    {
        int width, height;
        if (!int.TryParse(mapWidth.text, out width)
            || !int.TryParse(mapHeight.text, out height)) return;

        Math.Clamp(width, 100, 1200);
        Math.Clamp(height, 100, 1200);
        mapWidth.text = width.ToString();
        mapHeight.text = height.ToString();
        TerrainRenderer.size = new Vector2(width,height);
    }

    public void UploadMap()
    {
        //NetworkManager.Instance.UploadMap(mapInformation.gameObject);
    }
}