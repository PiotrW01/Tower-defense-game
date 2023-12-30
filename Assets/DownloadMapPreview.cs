using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DownloadMapPreview : MonoBehaviour
{
    public MapData mapData;
    public TextMeshProUGUI mapName;
    public TextMeshProUGUI authorName;
    public Button saveButton;

    void Start()
    {
        mapName.text = mapData.name;
        authorName.text = mapData.mapAuthor;

        saveButton.onClick.AddListener(() =>
        {
            FileManager.SaveMapData(mapData);
            saveButton.interactable = false;
        });


    }

}
