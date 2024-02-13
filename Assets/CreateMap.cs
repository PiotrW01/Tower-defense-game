using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateMap : MonoBehaviour
{
    private Button createMap;
    // Start is called before the first frame update
    void Start()
    {
        createMap = GetComponent<Button>();
        createMap.onClick.AddListener( () =>
        {
            SoundManager.Instance.PlayButtonClick();
            MapPreview.ChosenMapData = null;
            SceneManager.LoadScene("mapEditor");
        });
    }
}
