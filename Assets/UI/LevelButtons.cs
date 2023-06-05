using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartLevel()
    {
        SoundManager.Instance.PlayButtonClick();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        SoundManager.Instance.PlayButtonClick();
    }

    public void ResumeGame()
    {
        SoundManager.Instance.PlayButtonClick();
        Time.timeScale = TimeControl.gameSpeeds[TimeControl.gameSpeed];
        TimeControl.gamePaused = false;
        GameObject.Find("OptionsInterface").gameObject.SetActive(false);
    }

}
