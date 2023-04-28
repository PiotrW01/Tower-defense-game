using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{

    public int enemyCount = 10;
    public float enemySpawnTime = 1f;
    public bool isSpawning = false;
    public GameObject[] enemies;

    private int enemyTempCount;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !IsInvoking("spawn"))
        {
            enemyTempCount = enemyCount;
            InvokeRepeating("spawn", 0, enemySpawnTime);
        }
        if (Input.GetKeyDown(KeyCode.X) || enemyTempCount < 1)
        {
            CancelInvoke("spawn");
        }
    }




    void spawn()
    {
        if (enemies.Length != 0)
        {
            Instantiate(enemies[Random.Range(0,enemies.Length)], Waypoints.waypoints[0].transform.localPosition, Quaternion.identity);
            enemyTempCount--;
        }

    }

}
