using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuSwitcher : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject LocalMapsMenu;
    public GameObject DownloadMapsMenu;
    public GameObject StatisticsMenu;

    private void Start()
    {
        var buttons = FindObjectsOfType<Button>();
        foreach (var button in buttons)
        {
            button.onClick.AddListener(delegate
            {
                SoundManager.Instance.PlayButtonClick();
            });
        }

        MainMenu.SetActive(true);
        LocalMapsMenu.SetActive(false);
        DownloadMapsMenu.SetActive(false);
        StatisticsMenu.SetActive(false);

    }

    public void ReturnToMain()
    {
        MainMenu.SetActive(true);
        LocalMapsMenu.SetActive(false);
        DownloadMapsMenu.SetActive(false);
        StatisticsMenu.SetActive(false);
    }
    public void OpenLocalMaps()
    {
        MainMenu.SetActive(false);
        LocalMapsMenu.SetActive(true);
    }
    public void OpenDownloadMaps()
    {
        MainMenu.SetActive(false);
        DownloadMapsMenu.SetActive(true);
    }

    public void OpenStatistics()
    {
        MainMenu.SetActive(false);
        StatisticsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        PlayerPrefs.SetFloat("Sound", SoundManager.Instance.GetSoundVolume());
        PlayerPrefs.SetFloat("Music", SoundManager.Instance.GetMusicVolume());
        PlayerPrefs.Save();

        Application.Quit();
    }
}
