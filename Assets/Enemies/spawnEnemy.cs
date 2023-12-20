using System.Collections;
using TMPro;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
    public static int enemiesAlive = 0;

    public int waveNumber = 0;
    public bool isSpawning = false;
    public GameObject[] enemies;
    public static EnemyWave[] enemyWaves = new EnemyWave[20];
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
            new int[,] {
                {(int)Enemy.Enemy1, 20, 1000 }
            });
        CreateWave(1,
            new int[,] {
                { (int)Enemy.Enemy1, 35, 400 }
            });
        CreateWave(2,
            new int[,] {
                {(int)Enemy.Enemy1, 25, 750 },
                {(int)Enemy.Enemy2, 5, 1000 } 
            });
        CreateWave(3,
            new int[,] {
                {(int)Enemy.Enemy1, 35, 500 },
                {(int)Enemy.Enemy2, 18, 500 } 
            });
        CreateWave(4,
            new int[,] {
                {(int)Enemy.Enemy1, 20, 500 },
                {(int)Enemy.Enemy2, 5, 500 },
                {(int)Enemy.Enemy2, 15, 250 }
            });
        CreateWave(5,
            new int[,] {
                {(int)Enemy.Enemy1, 50, 150 },
                {(int)Enemy.Enemy4, 5, 500 } 
            });
        CreateWave(6,
            new int[,] {
                {(int)Enemy.Enemy3, 20, 500 }
            });
        CreateWave(7,
            new int[,] { 
                { (int)Enemy.Enemy2, 30, 100 }, 
                { (int)Enemy.Enemy3, 10, 500 }
            });
        CreateWave(8,
            new int[,] {
                { (int)Enemy.Enemy4, 20, 200 }
            });
        CreateWave(9,
            new int[,] {
                { (int)Enemy.Enemy4, 20, 200 },
                { (int)Enemy.Enemy1, 80, 50 }
            });

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
        if (enemyWaves[waveNumber] == null || isSpawning || enemiesAlive != 0) return;
        SoundManager.Instance.PlayButtonClick();
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
            for (int i = 0; i < v[currentWaveInWave, 1]; i++) // 1 Amount
            {
                Instantiate(enemies[v[currentWaveInWave, 0]], Waypoints.waypoints[0], Quaternion.identity); // 0 Type
                enemiesAlive++;

                yield return new WaitForSeconds((float)v[currentWaveInWave, 2] / 1000); // 2 Delay [ms]
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
