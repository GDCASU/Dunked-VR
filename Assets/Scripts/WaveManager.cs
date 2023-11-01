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
    public static WaveManager instance;

    [Header("Wave Pools")]
    [SerializeField] private int changePoolAfterWaves = 5;
    [SerializeField] private List<WavePool> wavePools = new List<WavePool>();

    [Header("Difficulty")]
    [SerializeField] public WaveDifficulty currentDifficulty = 0;       // Should not be tampered with; just for testing

    [Header("Events")]
    [SerializeField] public UnityEvent<int> onWaveStart;

    private WavePool currentWavePool = null;
    private int waveCounter = 1;
    private int waveCountMult = 1; // Used to keep track of when to change WavePools

    private void Awake()        // Handle Singleton
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (wavePools.Count > 0)
        {
            currentWavePool = wavePools[0];     // Start currentWavePool at first wave in list
            SpawnWave();                       // Spawn the first wave after short delay
        }
    }

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
 
        Instantiate(currentWavePool.RandomWaveSelect(), gameObject.transform);
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
