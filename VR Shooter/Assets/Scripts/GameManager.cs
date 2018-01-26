using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

//

public class GameManager : Singleton<GameManager> {

    [SerializeField] Transform fireLocation;
    [SerializeField] Transform[] enemySpawns;
    [SerializeField] float timeBetweenSpawns = .4f;
    [SerializeField] float timeBetweenBursts = 2.0f;
    [SerializeField] int burstSize = 5;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Text scoreText;
    [SerializeField] Text messageText;
    [SerializeField] Text highScoreText;

    private int[] waveCounts = { 20, 30, 42, 50, 60, 70 };
    private int[] burstSizes = { 5, 6, 7, 10, 12, 14 };

    // list of enemies on screen
    private List<GameObject> activeEnemies = new List<GameObject>();

    // the current wave of enemies
    private int currentWave;

    // 1 less than the currentWave (for array use)
    private int waveIndex
    {
        get { return currentWave - 1; }
    }

    private int gofs = 176;
    private int ywfs = 212;
    private int levelfs = 127;
    private int cdfs = 100;
    private int enemiesSpawned;
    private float timeSinceLastSpawn;
    private int enemiesKilled;
    private int totalEnemiesKilled;
    private bool playerIsDead;
    private bool playerHasWon;
    private bool gameOver { get { return playerIsDead || playerHasWon; } }
    
    public bool PlayerIsDead { get { return playerIsDead; } }

    public bool GameOver
    {
        get { return gameOver; }
    }

    void Awake()
    {
        Assert.IsNotNull(fireLocation);
        Assert.IsNotNull(enemySpawns);
        Assert.IsNotNull(enemyPrefab);
        Assert.IsNotNull(scoreText);
        Assert.IsNotNull(messageText);
        Assert.IsNotNull(highScoreText);
        Assert.AreEqual(waveCounts.Length, burstSizes.Length);
    }

	// Use this for initialization
	void Start () {
        currentWave = 1;
        PlayGame();
    }
	
	// Update is called once per frame
	void Update () {

        if (!gameOver)
        {
            if(enemiesKilled >= waveCounts[waveIndex])
            {
                currentWave++;
                burstSize = burstSizes[waveIndex];
                if(currentWave > waveCounts.Length)
                {
                    // The game is over, the player has won
                    playerHasWon = true;
                    messageText.gameObject.SetActive(true);
                    messageText.fontSize = ywfs;
                    messageText.text = "You Win!";
                    messageText.GetComponent<Animator>().SetTrigger("GameOver");
                    endGame();
                } else {
                    // continue game with the next wave
                    initializeNewWave();                
                }
            }

            timeSinceLastSpawn += Time.deltaTime;
        } else
        {
            // some sort of menu system
        }

    }

    public void PlayGame()
    {
        clearEnemies();
        enemiesSpawned = 0;
        enemiesKilled = 0;
        UpdateScore();
        highScoreText.gameObject.SetActive(false);
        messageText.gameObject.SetActive(false);
        playerIsDead = false;
        playerHasWon = false;
        totalEnemiesKilled = 0;
        Player.Instance.ResetHealth();
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1f);
        messageText.gameObject.SetActive(true);
        messageText.fontSize = cdfs;
        messageText.text = "Sending Wave in 3";
        yield return new WaitForSeconds(1f);
        messageText.text = "Sending Wave in 2";
        yield return new WaitForSeconds(1f);
        messageText.text = "Sending Wave in 1";
        yield return new WaitForSeconds(1f);
        messageText.gameObject.SetActive(false);
        StartCoroutine(SpawnLoop());
    }

    // reset vars then call waitForNextWave
    private void initializeNewWave()
    {
        // Initialize wave variables
        // UpdateScore() not called until next wave is called
        enemiesSpawned = 0;
        enemiesKilled = 0;
        StartCoroutine(waitForNextWave());
    }

    // creates a pause before spawning more enemies
    IEnumerator waitForNextWave()
    {
        messageText.gameObject.SetActive(true);
        messageText.fontSize = levelfs;
        messageText.text = "Wave " + waveIndex + " Completed!";
        yield return new WaitForSeconds(3f);
        messageText.text = "Sending next wave...";
        yield return new WaitForSeconds(2.5f);
        UpdateScore();
        StartCoroutine(SpawnLoop());
        messageText.gameObject.SetActive(false);
    }

    private void spawnEnemy()
    {
        // pick random spawn location
        int sp = Random.Range(0, enemySpawns.Length);
        Transform spawnPoint = enemySpawns[sp];
        // Add noise to spawn point
        float distX = Random.Range(-10f, 10f);
        float distZ = Random.Range(-10f, 10f);
        Vector3 spawnSpot = spawnPoint.transform.position + new Vector3(distX, 0f, distZ);
        GameObject newEnemy = Instantiate(enemyPrefab, spawnSpot, spawnPoint.transform.rotation) as GameObject;
        activeEnemies.Add(newEnemy);
        enemiesSpawned++;
    }

    IEnumerator SpawnLoop()
    {
        if (enemiesSpawned < waveCounts[waveIndex] && !gameOver)
        {
            // spawn enemies in bursts
            spawnEnemy();
            if(enemiesSpawned != waveCounts[waveIndex])
            {
                if (enemiesSpawned % burstSize == 0)
                {
                    yield return new WaitForSeconds(timeBetweenBursts);
                    Enemy.WalkSpeed += .2f;
                }
                else
                {
                    yield return new WaitForSeconds(timeBetweenSpawns);
                }
                StartCoroutine(SpawnLoop());
            }
            // Increase speed after wave
            else { Enemy.WalkSpeed += .3f; }
        } 
    }

    // delete all enemies from map
    private void clearEnemies()
    {
        if(activeEnemies.Count > 0)
        {
            foreach (GameObject enemy in activeEnemies)
            {
                Destroy(enemy);
            }
        }
    }

    // to be called by Enemy script
    public void RemoveEnemyFromList(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }

    public void KillEnemy()
    {
        if (!playerIsDead)
        {
            enemiesKilled++;
            totalEnemiesKilled++;
            UpdateScore();
        }
    }

    public void UpdateScore()
    {
        scoreText.text = "Kills: " + enemiesKilled;
    }

    private void finalKillsText()
    {
        scoreText.text = "Total Kills: " + totalEnemiesKilled;
    }

    public void PlayerHasDied()
    {
        messageText.gameObject.SetActive(true);
        messageText.fontSize = gofs;
        messageText.text = "Game Over";
        messageText.GetComponent<Animator>().SetTrigger("GameOver");
        playerIsDead = true;
        endGame();
    }

    private void endGame()
    {
        finalKillsText();
        showHighScoreText(totalEnemiesKilled > PlayerPrefs.GetInt("HighScore", 0));
        StartCoroutine(CountdownToRestart());
    }

    private void showHighScoreText(bool isHighScore)
    {
        highScoreText.gameObject.SetActive(true);
        if (isHighScore)
        {
            highScoreText.text = "New High Score!";
            PlayerPrefs.SetInt("HighScore", totalEnemiesKilled);
        } else
        {
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
        }
    }

    IEnumerator CountdownToRestart()
    {
        yield return new WaitForSeconds(5f);
        messageText.fontSize = cdfs;
        for(int seconds = 30; seconds > 0; seconds--)
        {
            messageText.text = "Restarting Game in " + seconds;
            yield return new WaitForSeconds(1f);
        }
        PlayGame();
    }

}
