using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtons : MonoBehaviour
{
    public Button scoreButton;
    public TextMeshProUGUI scoreButtonTextMesh;

    private void Start()
    {
        if (!NetworkManager.isLoggedIn && scoreButton != null) scoreButton.interactable = false;
    }

    public void RestartLevel()
    {
        SoundManager.Instance.PlayButtonClick();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        SoundManager.Instance.PlayButtonClick();
        SceneManager.LoadScene("menu");
    }

    public void ResumeGame()
    {
        SoundManager.Instance.PlayButtonClick();
        Time.timeScale = TimeControl.gameSpeeds[TimeControl.gameSpeed];
        TimeControl.gamePaused = false;
        GameObject.Find("OptionsInterface").gameObject.SetActive(false);
    }

    public void UploadScore()
    {
        scoreButton.interactable = false;
        scoreButtonTextMesh.text = "Uploading...";
        StartCoroutine(UploadScoreAsync());
    }

    IEnumerator UploadScoreAsync()
    {
        NetworkManager.UploadRequest request = new();
        request.mapID = MapPreview.ChosenMapData.id;
        request.mapData = MapPreview.ChosenMapData;
        request.score = Player.score;
        string jsonData = JsonUtility.ToJson(request);

        var www = NetworkManager.CreateJsonRequest("scores/upload", "PUT", jsonData);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
            scoreButtonTextMesh.text = "Failed to upload";
            scoreButton.interactable = true;
        } else
        {
            Debug.Log(www.responseCode);
            if (www.responseCode == 201)
            {
                scoreButtonTextMesh.text = "Uploaded";
            } else
            {
                scoreButtonTextMesh.text = "Failed to upload";
                scoreButton.interactable = true;
            }
        }

        www.Dispose();
        yield break;
    }
}
