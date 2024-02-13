using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapPreview : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public static MapData ChosenMapData;
    public MapData mapData;
    public TextMeshProUGUI mapName;
    public TextMeshProUGUI authorName;
    public Button editButton;
    public Button playButton;
    public Button deleteButton;
    private bool isDeleteConfirmed = false;

    void Start()
    {
        mapName.text = mapData.name;
        authorName.text = mapData.mapAuthor;

        editButton.onClick.AddListener(() => 
        {
            SoundManager.Instance.PlayButtonClick();
            ChosenMapData = mapData;
            SceneManager.LoadScene("mapEditor");
        });

        playButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClick();
            ChosenMapData = mapData;
            SceneManager.LoadScene("game");
        });

        deleteButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClick();
            if (!isDeleteConfirmed)
            {
                isDeleteConfirmed = true;
                return;
            } 
            FileManager.DeleteMapData(mapData.mapAuthor, mapData.name);
            Destroy(gameObject);
        });
    }

    public void OnSelect(BaseEventData eventData)
    {
        ChosenMapData = mapData;
        GameObject.Find("HighScores").GetComponent<Highscores>().LoadScores(mapData.id);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        isDeleteConfirmed = false;
    }
}
