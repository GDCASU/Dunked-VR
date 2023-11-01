using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Singleton Instance
    public static ScoreManager instance;

    // Events for UI and other stuff; the current playerLives is passed into the event
    public static event Action<int> onScoreUpdate;

    public int playerScore = 0;

    private void Awake()        // Handle Singleton
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()        // Handle Singleton
    {
        ResetScore();
    }

    public void AddScore(int score)
    {
        playerScore += score;
        onScoreUpdate?.Invoke(playerScore);
    }

    public void ResetScore()
    {
        playerScore = 0;
        onScoreUpdate?.Invoke(playerScore);
    }
}
