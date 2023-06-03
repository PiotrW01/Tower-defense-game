using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
    public int waveNumber = 0;
    public bool isSpawning = false;
    public GameObject[] enemies;
    public static EnemyWave[] enemyWaves = new EnemyWave[10];
    public static int[,] enemyWaves2;
    public TextMeshProUGUI waveNumberText;
    private enum Enemy
    {
        Enemy1,
        Enemy2,
        Enemy3,
        Enemy4,
    }
    public class EnemyWave
    {
        // enemyType, enemyAmount, enemySpawnDelay
        public int[,] enemyWavesinWave;

        public EnemyWave(int enemyWavesinWaveAmount)
        {
            enemyWavesinWave = new int[enemyWavesinWaveAmount,3];
        }

    }

    //make class for creating enemies

    private void Start()
    {
        // CreateWave waveNumber
        // array: enemyID, enemyAmount, enemySpawnDelay
        // enemySpawnDelay is in miliseconds

        CreateWave(0,
            new int[,] {{(int)Enemy.Enemy1, 10, 1000 },
                        {(int)Enemy.Enemy1, 10, 500 }, 
                        {(int)Enemy.Enemy2, 2, 1000 }});
        CreateWave(1,
            new int[,] {{(int)Enemy.Enemy3, 5, 500 },
                        {(int)Enemy.Enemy4, 5, 250 } });
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SpawnWave();
        }
    }
    public void SpawnWave() 
    {
        if (enemyWaves[waveNumber] == null || isSpawning) return;
        waveNumberText.text = "Wave " + (waveNumber + 1).ToString();
        StartCoroutine(SpawnEnemies(waveNumber++));
    }
    IEnumerator SpawnEnemies(int waveNumber)
    {
        isSpawning = true;
        int wavesInWave = enemyWaves[waveNumber].enemyWavesinWave.GetLength(0);
        int currentWaveInWave = 0;
        var v = enemyWaves[waveNumber].enemyWavesinWave;

        while (currentWaveInWave < wavesInWave)
        {
            for (int i = 0; i < v[currentWaveInWave, 1]; i++) // 1 is array with enemy amount
            {
                Instantiate(enemies[v[currentWaveInWave, 0]], Waypoints.StartPos, Quaternion.identity); // 0 is array with enemy types

                yield return new WaitForSeconds((float)v[currentWaveInWave, 2] / 1000); // 2 is array with spawn delay
            }

            currentWaveInWave++;
        }
        isSpawning = false;
    }
    private void CreateWave(int gameWaveNumber, int[,] wavesInWave)
    {
        enemyWaves[gameWaveNumber] = new EnemyWave(wavesInWave.GetLength(0));

        for (int i = 0; i < wavesInWave.GetLength(0); i++)
        {
            enemyWaves[gameWaveNumber].enemyWavesinWave[i, 0] = wavesInWave[i,0];
            enemyWaves[gameWaveNumber].enemyWavesinWave[i, 1] = wavesInWave[i, 1];
            enemyWaves[gameWaveNumber].enemyWavesinWave[i, 2] = wavesInWave[i, 2];
        }
    }

}
