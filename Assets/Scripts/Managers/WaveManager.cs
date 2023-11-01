using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum WaveDifficulty
{
    easy = 0,
    medium = 1,
    hard = 2,
    veryHard = 3
}

public class WaveManager : MonoBehaviour
{
    // Singleton Instance
    public static WaveManager instance;

    // Static Events
    public static event Action<float> onUpdateTimer;

    [Header("Wave Pool")]
    [SerializeField] private int changePoolAfterWaves = 5;
    [SerializeField] private List<WavePool> wavePools = new List<WavePool>();

    [Header("Timer")]
    [SerializeField] private float maxTimerTime = 30f;      // This is the max time given to a wave at the start of the game
    [SerializeField] private float timerDecrement = 1f;     // This amt will be decremented from the max time after every wave
    [SerializeField] private float timeLimitCap = 0f;       // This limits the timerTime from going under a certain amt after a period of time

    [Header("Difficulty")]
    [SerializeField] public WaveDifficulty currentDifficulty = 0;       // Should not be tampered with; just for testing

    [Header("Unity Events")]
    [SerializeField] public UnityEvent<int> onWaveStart;

    // Private Flags
    private bool gameOverFlag = false;
    
    // Private wave variables
    private Wave currentWave = null;
    private WavePool currentWavePool = null;
    private int waveCounter = 1;
    private int waveCountMult = 1; // Used to keep track of when to change WavePools

    // Private timer variables; these will be updated throughout the game
    private float currentMaxTimerTime;
    private float timerTime;

    // Components
    private LivesManager _lives;

    private void Awake()        // Handle Singleton
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        _lives = GetComponent<LivesManager>();
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (!gameOverFlag)
        {
            Timer();
        }
    }

    public void StartGame()
    {
        currentMaxTimerTime = maxTimerTime;
        timerTime = maxTimerTime;

        if (wavePools.Count > 0)
        {
            currentWavePool = wavePools[0];     // Start currentWavePool at first wave in list
            SpawnWave();                       // Spawn the first wave after short delay
        }

        gameOverFlag = false;
    }

    public void EndGame()
    {
        gameOverFlag = true;
        waveCounter = 1;
        waveCountMult = 1;
        currentDifficulty = 0;
        _lives.ResetLives();
    }

    #region Timer
    private void Timer()
    {
        if (timerTime >= 0)
        {
            timerTime -= Time.deltaTime;
            onUpdateTimer?.Invoke(timerTime);
        }
        else
        {
            // End the Wave
            ResetTimer();
            currentWave?.StopWave();
            _lives.LoseLife();

            if (_lives.playerLives > 0)
            {
                UpdateWaveCounter();
                SpawnWave();
            }
            else
            {
                EndGame();
            }
        }
    }

    public void ResetTimer()
    {
        if (currentMaxTimerTime > timeLimitCap)     // Decrement the max time as long as the cap is not reached
            currentMaxTimerTime--;

        timerTime = currentMaxTimerTime;        // Reset timer value
    }
    #endregion

    public void RaiseDifficulty()
    {
        if (((int)currentDifficulty + 1) < wavePools.Count) // Check if we are not at the end of wavePools; otherwise, stay on last WavePool
        {
            currentDifficulty++;
            currentWavePool = wavePools[((int)currentDifficulty)];
        }
    }

    public void SpawnWave()
    {
        // Event for UI
        onWaveStart?.Invoke(waveCounter);
 
        GameObject wave = Instantiate(currentWavePool.RandomWaveSelect(), gameObject.transform);
        currentWave = wave.gameObject.GetComponent<Wave>();
    }

    public void UpdateWaveCounter()         // Update waveCounter and check if we need to raise the difficulty
    {
        waveCounter++;
        // changeWavePoolAfter * i = nextWavePoolChange; Ex: 10 * 2 = 20 next WavePool after Wave 20
        if (waveCounter >= changePoolAfterWaves * waveCountMult)
        {
            RaiseDifficulty();
            waveCountMult++;
        }
    }
}
