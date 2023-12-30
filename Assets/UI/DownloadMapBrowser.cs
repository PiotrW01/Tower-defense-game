using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static NetworkManager;

public class DownloadMapBrowser : MonoBehaviour
{
    public GameObject DownloadMapPreviewPrefab;
    public Button previousPage;
    public Button nextPage;
    public Transform container;
    int page = 0;

    void Start()
    {
        previousPage.onClick.AddListener(() =>
        {
            if (page > 0)
            {
                page--;
                DownloadMapBatch();
            }
        });
        nextPage.onClick.AddListener(() =>
        {
            if(container.childCount == 10)
            {
                page++;
                DownloadMapBatch();
            }
        });
    }

    public void DownloadMapBatch()
    {
        StartCoroutine(GetLatestMapsAsync(page));
    }

    IEnumerator GetLatestMapsAsync(int page)
    {
        previousPage.interactable = false;
        nextPage.interactable = false;

        var www = CreateJsonRequest("http://localhost:5000/maps/" + page, "GET", "");
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            MapsRequest req = JsonUtility.FromJson<MapsRequest>(www.downloadHandler.text);
            foreach (var data in req.maps)
            {
                var mapData = JsonUtility.FromJson<MapData>(data.jsonMapData);
                GameObject map = Instantiate(DownloadMapPreviewPrefab, container);
                map.GetComponent<DownloadMapPreview>().mapData = mapData;
            }
        }

        previousPage.interactable = true;
        nextPage.interactable = true;
        www.Dispose();
        yield break;
    }

    private void OnEnable()
    {
        page = 0;
        DownloadMapBatch();
    }

    private void OnDisable()
    {
        for (int i = 0; i < container.childCount; i++)
        {
            Destroy(container.GetChild(i).gameObject);
        }
    }
}
