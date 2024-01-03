using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{ //change class name to enemyspawner
    public static int enemiesAlive = 0;

    public int waveNumber = 0;
    public bool isSpawning = false;
    private Waves waveManager;
    public TextMeshProUGUI waveNumberText;

    private class EnemyWave
    {
        // enemyType, enemyAmount, enemySpawnDelay
        public int[,] enemyWavesinWave;

        public EnemyWave(int enemyWavesinWaveAmount)
        {
            enemyWavesinWave = new int[enemyWavesinWaveAmount,3];
        }
    }
    private class Waves
    {
        public Wave[] waves;

        public Waves(int maxWaves)
        {
            waves = new Wave[maxWaves];
        }

        public void Init()
        {
            for (int i = 0; i < waves.Length; i++)
            {
                waves[i] = new Wave();
            }
        }

        public void AddInWave(int waveNumber, InWave inWave)
        {
            waves[waveNumber].inWaves.Add(inWave);
        }
        public int GetInWaveAmount(int waveNumber)
        {
            return waves[waveNumber].inWaves.Count;
        }

        public InWave GetInWave(int waveNumber, int inWaveNumber)
        {
            return waves[waveNumber].inWaves[inWaveNumber];
        }
    }
    private class Wave
    {
        public List<InWave> inWaves;

        public Wave()
        {
            inWaves = new();
        }
    }
    private class InWave
    {
        public Enemy enemyType;
        public int enemyAmount;
        public float spawnDelay;
        public InWave(Enemy type, int amount, float delay)
        {
            enemyType = type;
            enemyAmount = amount;
            spawnDelay = delay;
        }
    }



    private void Start()
    {
        waveManager = new Waves(20);
        waveManager.Init();
        waveManager.AddInWave(0, new InWave(Enemy.APC_B1, 20, 2));
        waveManager.AddInWave(0, new InWave(Enemy.APC_B1, 10, 1));
        waveManager.AddInWave(1, new InWave(Enemy.APC_B2, 10, 0.5f));
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
        if (waveManager.waves[waveNumber] == null || isSpawning || enemiesAlive != 0) return;
        SoundManager.Instance.PlayButtonClick();
        waveNumberText.text = "Wave " + (waveNumber + 1).ToString();
        StartCoroutine(SpawnEnemies(waveNumber++));
    }
    IEnumerator SpawnEnemies(int waveNumber)
    {
        isSpawning = true;
        int inWavesAmount = waveManager.GetInWaveAmount(waveNumber);
        int currentInWave = 0;
        InWave inWave;

        while (currentInWave < inWavesAmount)
        {
            inWave = waveManager.GetInWave(waveNumber, currentInWave);
            for (int i = 0; i < inWave.enemyAmount; i++)
            {
                var waypoint = new Vector3(Waypoints.waypoints[0].x, 0.2f, Waypoints.waypoints[0].y);
                var enemy = Instantiate(EnemyDictionary.Enemies[inWave.enemyType], waypoint, Quaternion.identity);
                enemy.transform.LookAt(waypoint);
                enemiesAlive++;

                yield return new WaitForSeconds(inWave.spawnDelay);
            }

            currentInWave++;
        }
        isSpawning = false;
    }
}
