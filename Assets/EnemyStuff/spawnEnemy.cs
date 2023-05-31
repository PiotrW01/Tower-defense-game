using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
    public int waveNumber = 0;
    public bool isSpawning = false;
    public GameObject[] enemies;
    public static EnemyWave[] enemyWaves = new EnemyWave[10];

    public class EnemyWave
    {
        // enemyType, enemyCount, enemySpawnTime
        public int[,] enemyWavesinWave;

        public EnemyWave(int enemyWavesinWaveAmount)
        {
            enemyWavesinWave = new int[enemyWavesinWaveAmount,3];
        }

    };

    //make class for creating enemies

    private void Start()
    {
        // CreateWave waveNumber
        // Array 0: enemyId
        // Array 1: enemyAmount
        // Array 2: enemySpawnTime 
        // enemySpawnTime is in miliseconds

        // TODO: przerobiæ listy aby ka¿da zawiera³a enemyid,enemyamount,enemyspawntime
        CreateWave(0, 
            new int[] { 0, 0, 1 }, 
            new int[] { 10, 10, 2 }, 
            new int[] { 1000, 500, 1000 });
        CreateWave(1,
            new int[] { 2, 3 },
            new int[] { 5, 5 },
            new int[] { 500, 250 });
        /*       CreateWave(2,
                   new int[] { 1 },
                   new int[] { 1 },
                   new int[] { 1 });
               CreateWave(3,
                   new int[] { 1 },
                   new int[] { 1 },
                   new int[] { 1 });
               CreateWave(4,
                   new int[] { 1 },
                   new int[] { 1 },
                   new int[] { 1 });*/
    }

    private void CreateWave(int gameWaveNumber, int[] enemiesInWave, int[] amountOfEachEnemy, int[] spawnTimeOfEachEnemy)
    {
        enemyWaves[gameWaveNumber] = new EnemyWave(enemiesInWave.Length);

        for (int i = 0; i < enemiesInWave.Length; i++)
        {
                enemyWaves[gameWaveNumber].enemyWavesinWave[i, 0] = enemiesInWave[i];
                enemyWaves[gameWaveNumber].enemyWavesinWave[i, 1] = amountOfEachEnemy[i];
                enemyWaves[gameWaveNumber].enemyWavesinWave[i, 2] = spawnTimeOfEachEnemy[i];
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (enemyWaves[waveNumber] == null) return;
            SpawnWave(waveNumber++);
        }
    }

    public void SpawnWave(int waveNumber) 
    {
        StartCoroutine(SpawnEnemies(waveNumber));
    }

    IEnumerator SpawnEnemies(int waveNumber)
    {
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

    }
}
