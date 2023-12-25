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
    
    public TMP_InputField playerMoney;
    public TMP_InputField mapName;
    public TMP_InputField mapWidth;
    public TMP_InputField mapHeight;
    public TMP_InputField playerName;
    public TMP_InputField playerPassword;
    public TMP_Dropdown terrainDropdown;
    public TMP_Dropdown objectDropdown;

    void Start()
    {
        LoadTerrainOptions();
        LoadObjectOptions();
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

    public void LoadObjectOptions()
    {
        List<TMP_Dropdown.OptionData> options = new();
        options.Add(new TMP_Dropdown.OptionData("None"));
        foreach (var key in EnvDictionary.Objects.Keys)
        {
            options.Add(new TMP_Dropdown.OptionData(key.ToString()));
        }
        objectDropdown.value = 0;
        objectDropdown.AddOptions(options);
    }


    public void ChangeTerrain()
    {
        Terrain terrainType;
        Enum.TryParse(terrainDropdown.options[terrainDropdown.value].text, out terrainType);
        Debug.Log(terrainType);
        TerrainRenderer.sprite = TerrainDictionary.Sprites[terrainType];
    }

    public void SelectObject()
    {
        if(objectDropdown.value == 0)
        {
            Destroy(ObjectPlacing.heldObject);
            return;
        }
        Env objectType;
        Enum.TryParse(terrainDropdown.options[objectDropdown.value].text, out objectType);
        Debug.Log(objectType);
        GameObject obj = Instantiate(EnvDictionary.Objects[objectType], GameObject.FindGameObjectWithTag("map").transform.Find("Environment"));
        obj.AddComponent<ObjectHandler>();

        ObjectPlacing.heldObject = obj;
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