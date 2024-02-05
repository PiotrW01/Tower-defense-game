using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using static NetworkManager;

public class Highscores : MonoBehaviour
{
    public GameObject ScorePrefab;
    public Transform ScoreContainer;

    public void LoadScores(int id)
    {
        if (ScoreContainer == null) return;
        if (ScoreContainer.childCount > 0)
        {
            for (int i = 0; i < ScoreContainer.childCount; i++)
            {
                Destroy(ScoreContainer.GetChild(i).gameObject);
            }
        }

        StartCoroutine(GetMapScoresAsync(id));
    }

    IEnumerator GetMapScoresAsync(int id)
    {
        var www = NetworkManager.CreateJsonRequest("http://localhost:5000/scores/" + id, "GET", "");
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            ScoresRequest req = JsonUtility.FromJson<ScoresRequest>(www.downloadHandler.text);
            foreach (var score in req.scores)
            {
                var scoreInfo = Instantiate(ScorePrefab, ScoreContainer).GetComponent<Score>();
                scoreInfo.playerName.text = score.username;
                scoreInfo.score.text = score.totalScore.ToString();
            }
        }

        yield return null;
    }
}
