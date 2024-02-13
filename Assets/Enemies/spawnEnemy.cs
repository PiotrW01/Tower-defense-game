using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{ //change class name to enemyspawner
    public static int enemiesAlive = 0;
    public static float waveDelay = 10f;
    public int waveNumber = 0;
    public static bool isSpawning = false;
    private WaveManager waveManager;
    public TextMeshProUGUI waveNumberText;

    private class WaveManager
    {
        public List<Wave> waves;

        public WaveManager(int maxWaves)
        {
            waves = new(maxWaves);
        }

        public void Init()
        {
            for (int i = 0; i < waves.Count; i++)
            {
                waves[i] = new Wave();
            }
        }

        public void AddInWave(int waveNumber, InWave inWave)
        {
            if (waves.Count == waveNumber) waves.Add(new Wave());
            waves[waveNumber].inWaves.Add(inWave);
        }

        public void AddWave()
        {
            waves.Add(new Wave());
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
        StartCoroutine(ForceNextWave());

        waveManager = new WaveManager(20);
        waveManager.Init();
        waveManager.AddInWave(0, new InWave(Enemy.APC_B1, 4, 2));
        waveManager.AddInWave(0, new InWave(Enemy.APC_B1, 0, 1));

/*        waveManager.AddInWave(1, new InWave(Enemy.APC_B2, 10, 0.5f));
        waveManager.AddInWave(1, new InWave(Enemy.APC_B2, 10, 0.5f));

        waveManager.AddInWave(2, new InWave(Enemy.APC_B2, 10, 0.3f));
        waveManager.AddInWave(2, new InWave(Enemy.APC_B2, 10, 0.3f));

        waveManager.AddInWave(3, new InWave(Enemy.APC_B2, 10, 0.3f));
        waveManager.AddInWave(3, new InWave(Enemy.APC_B2, 10, 0.3f));

        waveManager.AddInWave(4, new InWave(Enemy.APC_B2, 10, 0.3f));
        waveManager.AddInWave(4, new InWave(Enemy.APC_B2, 10, 0.3f));

        waveManager.AddInWave(5, new InWave(Enemy.APC_B2, 10, 0.3f));
        waveManager.AddInWave(5, new InWave(Enemy.APC_B2, 10, 0.3f));*/
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
        if (isSpawning || enemiesAlive != 0) return;
        if(waveManager.waves.Count == waveNumber)
        {
            GenerateNextWave();
        }
        SoundManager.Instance.PlayButtonClick();
        waveNumberText.text = "Wave " + (waveNumber + 1).ToString();
        StartCoroutine(SpawnEnemies(waveNumber));
        waveNumber++;
    }

    public void GenerateNextWave()
    {
        List<Enemy> elements = new List<Enemy> { Enemy.APC_B1, Enemy.APC_B2, Enemy.BAGI_ROCKET1 };
        float difficulty = Mathf.Clamp((float)waveNumber / 2, 0.1f, 999);
        List<float> chances = new List<float> 
        {   
            5,
            4 * difficulty,
            6 * difficulty,
        };

        var inWaveCount = new System.Random(waveNumber).Next(2,5);
        waveManager.AddWave();

        for (int i = 0; i < inWaveCount; i++)
        {
            var rand = new System.Random(waveNumber + i);
            Enemy selectedEnemy = WeightedRandomPick(elements, chances, rand.Next());
            int enemyAmount = rand.Next(5 + (int)difficulty, 20 + (int)difficulty);
            float spawnDelay = Mathf.Lerp(0.2f, 2.0f, (float)rand.NextDouble());
            InWave inWave = new InWave(selectedEnemy, enemyAmount, spawnDelay);
            waveManager.AddInWave(waveNumber, inWave);
        }
    }

    static T WeightedRandomPick<T>(List<T> elements, List<float> chances, int seed)
    {
        if (elements.Count != chances.Count)
        {
            throw new ArgumentException("The number of elements must be equal to the number of chances.");
        }

        float totalWeight = chances.Sum();
        double randomValue = new System.Random(seed).NextDouble() * totalWeight;
        Debug.Log(totalWeight);
        Debug.Log(randomValue);
        Debug.Log("");
        for (int i = 0; i < elements.Count; i++)
        {
            randomValue -= chances[i];
            if (randomValue <= 0)
            {
                return elements[i];
            }
        }

        throw new InvalidOperationException("Weighted random selection failed.");
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

    IEnumerator ForceNextWave()
    {
        while(waveNumber == 0) yield return null;

        while (Player.isAlive)
        {
            if (!isSpawning && enemiesAlive == 0)
            {
                int lastWaveNumber = waveNumber;
                yield return new WaitForSeconds(waveDelay);
                Debug.Log(lastWaveNumber);
                Debug.Log(waveNumber);
                if (lastWaveNumber == waveNumber)
                {
                    SpawnWave();
                }
            }
            yield return null;
        }
    }
}
