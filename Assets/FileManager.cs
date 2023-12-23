using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public static class FileManager
{


/*    public static MapData LoadMapData(string name)
    {
        OpenFileName ofn = new OpenFileName();

        ofn.structSize = Marshal.SizeOf(ofn);

        ofn.filter = "Json\0*.json\0";

        ofn.file = new String(new char[256]);
        ofn.maxFile = ofn.file.Length;

        ofn.fileTitle = new String(new char[64]);
        ofn.maxFileTitle = ofn.fileTitle.Length;

        ofn.initialDir = "C:\\";
        ofn.title = "Select the map to load";
        ofn.defExt = "json";
        LibWrap.GetOpenFileName(ofn);
        string json = File.ReadAllText(ofn.file);
        MapData mapData = JsonUtility.FromJson<MapData>(json); 
        return mapData;
    }*/

    public static void LoadAllMapData()
    {
        string[] names = Directory.GetFiles(Application.persistentDataPath);
    }

    public static void SaveMapData(MapData data)
    {
        string json = JsonUtility.ToJson(data);
        if (!Directory.Exists(Application.dataPath + "/Maps"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Maps");
        }
        string path = Path.Combine(Application.dataPath + "/Maps", data.name + ".json");

        try
        {
            File.WriteAllText(path, json);
            Debug.Log("saved at: " + path);
        } catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}

public class MapData
{
    public int id = 0;
    public string name;
    public int playerStartingMoney = 500;
    public Vector2 size;
    public Terrain terrainType;
    public Vector2[] SplinePos;
    public Vector2[] TangentPos;
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