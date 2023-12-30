using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadGame()
    {
        SoundManager.Instance.PlayButtonClick();
        SceneManager.LoadScene("game");
    }

    public void LoadMapEditor()
    {
        SoundManager.Instance.PlayButtonClick();
        SceneManager.LoadScene("mapEditor");
    }


    public void QuitGame()
    {
        SoundManager.Instance.PlayButtonClick();

        PlayerPrefs.SetFloat("Sound", SoundManager.Instance.GetSoundVolume());
        PlayerPrefs.SetFloat("Music", SoundManager.Instance.GetMusicVolume());
        PlayerPrefs.Save();

        Application.Quit();
    }
}
