using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public static class FileManager
{
    public static MapData LoadMapData(string name)
    {
        string json = File.ReadAllText(Application.dataPath + "/Maps/" + name + ".json");
        MapData mapData = JsonUtility.FromJson<MapData>(json); 
        return mapData;
    }

    public static MapData[] LoadAllMapData()
    {
        string[] paths = Directory.GetFiles(Application.dataPath + "/Maps", "*.json");
        MapData[] mapData = new MapData[paths.Length];
        for (int i = 0; i < paths.Length; i++)
        {
            string json = File.ReadAllText(paths[i]);
            mapData[i] = JsonUtility.FromJson<MapData>(json);
        }
        return mapData;
    }

    public static void SaveMapData(MapData data)
    {
        string json = JsonUtility.ToJson(data);
        if (!Directory.Exists(Application.dataPath + "/Maps"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Maps");
        }
        string path = Application.dataPath + "/Maps/" + data.mapAuthor + data.name + ".json";
        
        try
        {
            File.WriteAllText(path, json);
            Debug.Log("saved at: " + path);
        } catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public static void DeleteMapData(string mapAuthor, string mapName)
    {
        try
        {
            File.Delete(Application.dataPath + "/Maps/" + mapAuthor + mapName + ".json");
        } catch
        {
            Debug.Log("could not delete file");
        }
    }
}

[System.Serializable]
public class MapData
{
    public int id = 0;
    public string name;
    public string mapAuthor;
    public int playerStartingMoney = 500;
    public Vector2 size;
    public Terrain terrainType;
    public Vector2[] SplinePos;
    public Vector2[] TangentPos;
    public Vector2[] EnvObjectsPos;
    public Env[] EnvObjectsType;
}

[System.Serializable]
public class PlayerScore
{
    public string username;
    public int totalScore;
}

public class MapSearchInformation
{
    public int mapID;
    public string authorName;
    public string mapName;
}

/*[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenFileName
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;

    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;

    public String file = null;
    public int maxFile = 0;

    public String fileTitle = null;
    public int maxFileTitle = 0;

    public String initialDir = null;

    public String title = null;

    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;

    public String defExt = null;

    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;

    public String templateName = null;

    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
}

public class LibWrap
{
    //BOOL GetOpenFileName(LPOPENFILENAME lpofn);

    [DllImport("Comdlg32.dll", CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
}
*/