using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemies;
        public int[] counts;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;
    private SpawnState state = SpawnState.COUNTING;
    private int totalEnemies;
    private int enemiesEliminated;

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }

        waveCountdown = timeBetweenWaves;
        SetNextWave();
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    private void SetNextWave()
    {
        totalEnemies = 0;
        enemiesEliminated = 0;

        if (nextWave >= waves.Length)
        {
            nextWave = 0; // Restart from the first wave
        }

        for (int i = 0; i < waves[nextWave].counts.Length; i++)
        {
            totalEnemies += waves[nextWave].counts[i];
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning Wave: " + wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.enemies.Length; i++)
        {
            for (int j = 0; j < wave.counts[i]; j++)
            {
                SpawnEnemy(wave.enemies[i]);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }

        state = SpawnState.WAITING;
        SetNextWave();
        waveCountdown = timeBetweenWaves;
    }

    private void SpawnEnemy(Transform enemy)
    {
        Debug.Log("Spawning Enemy: " + enemy.name);

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    private bool EnemyIsAlive()
    {
        return enemiesEliminated < totalEnemies;
    }

    public void EnemyEliminated()
    {
        enemiesEliminated++;

        if (!EnemyIsAlive() && state == SpawnState.WAITING)
        {
            WaveCompleted();
        }
    }

    private void WaveCompleted()
    {
        Debug.Log("Wave Completed!");
        state = SpawnState.COUNTING;
        nextWave++;
    }
}

