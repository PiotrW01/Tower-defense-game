using System;
using UnityEngine;

public class LocalMapBrowser : MonoBehaviour
{
    public GameObject MapPreviewPrefab;
    public GameObject CreateMapPrefab;
    public Transform ContentContainer;
    private MapData[] allMapData;

    private void OnEnable()
    {
        allMapData = FileManager.LoadAllMapData();

        for (int i = 0; i < allMapData.Length; i++)
        {
            var preview = Instantiate(MapPreviewPrefab, ContentContainer);
            preview.GetComponent<MapPreview>().mapData = allMapData[i];
        }
        Array.Clear(allMapData, 0, allMapData.Length);
    }

    private void OnDisable()
    {
        for (int i = 1; i < ContentContainer.childCount; i++)
        {
            Destroy(ContentContainer.GetChild(i).gameObject);
        }
    }
}
