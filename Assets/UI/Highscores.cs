using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscores : MonoBehaviour
{
    public GameObject ScorePrefab;
    public static PlayerScore[] scores;
    public Transform ScoreContainer;

    public static void LoadScores(PlayerScore[] newScores)
    {
        scores = newScores;
        foreach (PlayerScore score in scores)
        {
            //Instantiate(ScorePrefab);
        }
    }
}
