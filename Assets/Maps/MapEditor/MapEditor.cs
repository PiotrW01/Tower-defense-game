using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using static NetworkManager;

public class MapEditor : MonoBehaviour
{
    // zamiast tych to zczytaæ to z obiektu environment na mapie
    public static List<GameObject> envObjects;
    public static bool isMenu = false;
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
        var obj = Instantiate(MapPrefab, Vector3.zero, Quaternion.identity);
        TerrainRenderer = obj.transform.Find("TerrainSprite").GetComponent<SpriteRenderer>();
        if(MapPreview.ChosenMapData != null)
        {
            var mapData = MapPreview.ChosenMapData;
            mapName.text = mapData.name;
            mapWidth.text = mapData.size.x.ToString();
            mapHeight.text = mapData.size.y.ToString();
            playerMoney.text = mapData.playerStartingMoney.ToString();
        } else
        {
            playerMoney.text = "500";
            mapWidth.text = TerrainRenderer.size.x.ToString();
            mapHeight.text = TerrainRenderer.size.y.ToString();
        }
        envObjects = new();
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
        Enum.TryParse(objectDropdown.options[objectDropdown.value].text, out objectType);
        Debug.Log(objectType);
        GameObject obj = Instantiate(EnvDictionary.Objects[objectType], GameObject.FindGameObjectWithTag("mapEnv").transform);
        ObjectPlacing.envObjectType = objectType;
        Destroy(ObjectPlacing.heldObject);
        ObjectPlacing.heldObject = obj;
    }

    public void ToggleOptionsMenu()
    {
        if (optionsMenu.activeInHierarchy)
        {
            isMenu = false;
            optionsMenu.SetActive(false);
        } else
        {
            isMenu = true;
            optionsMenu.SetActive(true);
        }
    }

    public MapData CreateMapData()
    {
        if (mapName.text.Length < 3) return null;
        var shapeController = GameObject.FindGameObjectWithTag("path").GetComponent<SpriteShapeController>().spline;
        var data = new MapData();
        int pointCount = shapeController.GetPointCount();
        data.name = mapName.text;
        data.mapAuthor = NetworkManager.username;
        data.SplinePos = new Vector2[pointCount];
        data.TangentPos = new Vector2[pointCount * 2];
        data.terrainType = (Terrain)Enum.Parse(typeof(Terrain), terrainDropdown.options[terrainDropdown.value].text);
        data.size = TerrainRenderer.size;
        data.EnvObjectsPos = new Vector2[envObjects.Count];
        data.EnvObjectsType = new Env[envObjects.Count];

        //pathPoints
        for (int i = 0; i < pointCount; i++)
        {
            data.SplinePos[i] = shapeController.GetPosition(i);
            data.TangentPos[2 * i] = shapeController.GetLeftTangent(i);
            data.TangentPos[2 * i + 1] = shapeController.GetRightTangent(i);
            Debug.Log(i + " " + shapeController.GetPosition(i));
        }

        //envObjects
        for (int i = 0; i < envObjects.Count; i++)
        {
            data.EnvObjectsPos[i] = envObjects[i].transform.position;
            data.EnvObjectsType[i] = envObjects[i].GetComponent<ObjectHandler>().objectType;
        }

        return data;
    }

    public void SaveMapLocally()
    {
        MapData data = CreateMapData();
        if (data == null) return;
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
        if (!NetworkManager.HasCredentialsSet())
        {
            ShowLoginFields();
            return;
        }
        MapData data = CreateMapData();
        if(data == null || data.mapAuthor == "") return;
        StartCoroutine(UploadMapAsync(data));
    }

    public void ShowLoginFields()
    {
        loginCredentials.SetActive(true);
    }

    IEnumerator UploadMapAsync(MapData mapData)
    {
        UploadRequest request = new UploadRequest();
        request.mapData = mapData;
        request.username = NetworkManager.username;
        request.password = NetworkManager.password;
        string jsonData = JsonUtility.ToJson(request);
        UnityWebRequest www = CreateJsonRequest("http://localhost:5000/maps/upload", "PUT", jsonData);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            if (www.responseCode == 202) Debug.Log("Map updated!");
            else if (www.responseCode == 201)
            {
                mapData.id = int.Parse(www.downloadHandler.text);
                Debug.Log("Map created and set map ID as " + mapData.id);
                FileManager.SaveMapData(mapData);
            }
        }

        www.Dispose();
        yield break;
    }
}