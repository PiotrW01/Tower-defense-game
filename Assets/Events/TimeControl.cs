using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeControl : MonoBehaviour
{

    public static int gameSpeed = 0;
    public static bool gamePaused = false;
    public static int[] gameSpeeds = new int[] { 1, 2, 4 };

    public TextMeshProUGUI[] textMeshes;


    private void Start()
    {
        gamePaused = false;
        Time.timeScale = 1;
        textMeshes[gameSpeed].color = new Color(1f, 1f, 0f);
    }

    private void OnDisable()
    {
        textMeshes[gameSpeed].color = Color.white;
        textMeshes[0].color = new Color(1f, 1f, 0f);
        gameSpeed = 0;
        Time.timeScale = gameSpeeds[gameSpeed];
    }

    void Update()
    {
        if (!Player.isAlive) return;

        if (Input.GetKeyDown(KeyCode.G))
        {
            changeTimeSpeed();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && !PurchaseManager.isPlacing) 
        {
            if (!gamePaused) 
            {
                GameObject.Find("UserInterface").transform.Find("OptionsInterface").gameObject.SetActive(true);
                Time.timeScale = 0;
                gamePaused = true;
            }
            else
            {
                GameObject.Find("UserInterface").transform.Find("OptionsInterface").gameObject.SetActive(false);
                Time.timeScale = gameSpeeds[gameSpeed];
                gamePaused = false;
            }
        }

    }


    public void changeTimeSpeed()
    {
        textMeshes[gameSpeed].color = Color.white;
        gameSpeed++;
        if (gameSpeed == gameSpeeds.Length)
        {
            gameSpeed = 0;
        }
        Time.timeScale = gameSpeeds[gameSpeed];
        textMeshes[gameSpeed].color = new Color(1f, 1f, 0f);
    }
}
