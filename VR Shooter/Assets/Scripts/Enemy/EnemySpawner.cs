using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class NewWaveEvent : UnityEvent<int> { }

public class EnemySpawner : MonoBehaviour, ISpawner {

    public NewWaveEvent OnNewWave = new NewWaveEvent();

    #region Serialized Variables

    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    List<Transform> spawnLocations = new List<Transform>();
    [SerializeField]
    List<int> waveCounts;
    [SerializeField]
    List<int> burstSizes;
    [SerializeField]
    [Range(0f, 2f)]
    float timeBetweenSpawns = .4f;
    [SerializeField]
    [Range(1f, 5f)]
    float timeBetweenBursts = 2.0f;

    #endregion

    List<GameObject> activeEnemies = new List<GameObject>();
    //int currentWave = 1;
    int waveIndex;
    int enemiesSpawned = 0;
    int burstSize;

    void Start()
    {
        waveIndex = 0;
    }

    public void StartSpawnLoop()
    {
        print("Starting spawn loop");
        OnNewWave.Invoke(waveCounts[waveIndex]);
        StartCoroutine(ClassicSpawnLoop());
    }


    IEnumerator ClassicSpawnLoop()
    {
        if (enemiesSpawned < waveCounts[waveIndex])
        {
            // spawn enemies in bursts
            spawnEnemy();
            if (enemiesSpawned < waveCounts[waveIndex])
            {
                if (enemiesSpawned % burstSizes[waveIndex] == 0)
                {
                    yield return new WaitForSeconds(timeBetweenBursts);
                    //Enemy.WalkSpeed += .2f;
                }
                else
                {
                    yield return new WaitForSeconds(timeBetweenSpawns);
                }
                StartCoroutine(ClassicSpawnLoop());
            }
            // Increase speed after wave
            //else { Enemy.WalkSpeed += .3f; }
        }
    }

    void spawnEnemy()
    {
        // pick random spawn location
        int sp = Random.Range(0, spawnLocations.Count);
        Transform spawnPoint = spawnLocations[sp];
        // Add noise to spawn point
        float distX = Random.Range(-10f, 10f);
        float distZ = Random.Range(-10f, 10f);
        Vector3 spawnSpot = spawnPoint.transform.position + new Vector3(distX, 0f, distZ);
        GameObject newEnemy = Instantiate(enemyPrefab, spawnSpot, spawnPoint.transform.rotation);
        activeEnemies.Add(newEnemy);
        enemiesSpawned++;
    }

    public void SendNextWave()
    {
        waveIndex++;
        if(waveIndex < waveCounts.Count)
        {
            StartSpawnLoop();
        }
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

}
