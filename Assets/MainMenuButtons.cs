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

    public void QuitGame()
    {
        SoundManager.Instance.PlayButtonClick();
        Application.Quit();
    }
}
