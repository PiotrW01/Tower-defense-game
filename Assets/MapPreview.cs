using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapPreview : MonoBehaviour, ISelectHandler
{
    public static MapData ChosenMapData;
    public MapData mapData;
    public TextMeshProUGUI mapName;
    public TextMeshProUGUI authorName;
    public Button editButton;
    public Button playButton;
    public Button deleteButton;
    private ButtonDeselectHandler deleteHandler;


    public void OnSelect(BaseEventData eventData)
    {
        //get highscores
        throw new System.NotImplementedException();
    }

    void Start()
    {
        mapName.text = mapData.name;
        authorName.text = mapData.mapAuthor;

        editButton.onClick.AddListener(() => 
        {
            ChosenMapData = mapData;
            SceneManager.LoadScene("mapEditor");
        });

        deleteHandler = deleteButton.gameObject.AddComponent<ButtonDeselectHandler>();
        deleteButton.onClick.AddListener(() =>
        {
            if (!deleteHandler.isDeleteConfirmed)
            {
                deleteHandler.isDeleteConfirmed = true;
                return;
            } 
            FileManager.DeleteMapData(mapData.mapAuthor, mapData.name);
            Destroy(gameObject);
        });
        
    }

    private class ButtonDeselectHandler : MonoBehaviour, IDeselectHandler
    {   
        public bool isDeleteConfirmed = false;


        public void OnDeselect(BaseEventData eventData)
        {
            isDeleteConfirmed = false;
        }
    }
}
