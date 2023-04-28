using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeControl : MonoBehaviour
{

    private int gameSpeed = 0;
    private int[] gameSpeeds = new int[] { 1, 2, 4 };
    private bool gamePaused = false;

    public TextMeshProUGUI[] textMeshes;
    /*    GameObject[] obj;
        GameObject[] obj2;
        GameObject[] obj3;*/


    private void Start()
    {
        Time.timeScale = 1;
        textMeshes[gameSpeed].color = new Color(1f, 1f, 0f);
    }

    private void OnDisable()
    {
        textMeshes[gameSpeed].color = Color.white;
        textMeshes[0].color = new Color(1f, 1f, 0f);
        gameSpeed = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            changeTimeSpeed();
        }

        if(Input.GetKeyDown(KeyCode.P)) 
        {
            /*            if (gamePaused)
                        {
                            onGameResume();
                            gamePaused = false;
                        }
                        else
                        {
                            onGamePause();
                            gamePaused = true;
                        }*/

            if (!gamePaused) 
            {
                Time.timeScale = 0;
                gamePaused = true;
            }
            else
            {
                Time.timeScale = 1;
                gamePaused = false;
            }
            





        }

    }


    private void changeTimeSpeed()
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

/*    private void onGamePause()
    {
        obj = GameObject.FindGameObjectsWithTag("enemy");

        foreach(GameObject enemy in obj)
        {
            enemy.GetComponent<enemyMovement>().enabled = false;
        }

        obj2 = GameObject.FindGameObjectsWithTag("tower");

        foreach (GameObject tower in obj2)
        {
            tower.GetComponent<Turret>().enabled = false;
        }

        obj3 = GameObject.FindGameObjectsWithTag("bullet");

        foreach (GameObject bullet in obj3)
        {
            bullet.GetComponent<bullet>().enabled = false;
        }

        gameObject.GetComponent<spawnEnemy>().CancelInvoke();
    }

    private void onGameResume()
    {
        foreach (GameObject enemy in obj)
        {
            enemy.GetComponent<enemyMovement>().enabled = true;
        }

        foreach (GameObject tower in obj2)
        {
            tower.GetComponent<Turret>().enabled = true;
        }

        foreach (GameObject bullet in obj3)
        {
            bullet.GetComponent<bullet>().enabled = true;
        }

        gameObject.GetComponent<spawnEnemy>().enabled = true;
    }*/
}
