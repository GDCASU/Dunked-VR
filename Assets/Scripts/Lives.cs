using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    // Events for UI and other stuff; the current playerLives is passed into the event
    public static event Action<int> onLifeUpdate;
    public static event Action onGameOver;

    [Header("Lives/Strikes")]
    [SerializeField] public int playerMaxLives = 3;        // Basically the strikes
    [SerializeField] public int playerLives;

    private void Start()
    {
        playerLives = playerMaxLives;

        onLifeUpdate?.Invoke(playerLives);
    }

    public void ResetLives()
    {
        playerLives = playerMaxLives;
    }

    public void LoseLife()
    {
        if (playerLives > 0)
        {
            playerLives--;
            onLifeUpdate?.Invoke(playerLives);
        }
        else
            GameOver();
    }

    public void GainLife()
    {
        if (playerLives < 3)
        {
            playerLives++;
            onLifeUpdate?.Invoke(playerLives);
        }
    }

    public void GameOver()
    {
        // END THE GAME
        onGameOver?.Invoke();
    }
}
