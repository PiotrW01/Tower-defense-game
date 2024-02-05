using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject LocalMapsMenu;
    public GameObject DownloadMapsMenu;

    private void Start()
    {
        MainMenu.SetActive(true);
        LocalMapsMenu.SetActive(false);
        DownloadMapsMenu.SetActive(false);
    }

    public void ReturnToMain()
    {
        MainMenu.SetActive(true);
        LocalMapsMenu.SetActive(false);
        DownloadMapsMenu.SetActive(false);
    }
    public void OpenLocalMaps()
    {
        MainMenu.SetActive(false);
        LocalMapsMenu.SetActive(true);
        DownloadMapsMenu.SetActive(false);
    }
    public void OpenDownloadMaps()
    {
        MainMenu.SetActive(false);
        LocalMapsMenu.SetActive(false);
        DownloadMapsMenu.SetActive(true);
    }

}
