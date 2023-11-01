using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugUI : MonoBehaviour
{
    public TMP_Text ScoreText;
    public TMP_Text LivesText;
    public TMP_Text TimerText;

    public void Awake()
    {
        ScoreManager.onScoreUpdate += UpdateScoreText;
        LivesManager.onLifeUpdate += UpdateLivesText;
        WaveManager.onUpdateTimer += UpdateTimerText;
    }

    private void UpdateScoreText(int score)
    {
        ScoreText.text = "Score: " + score;
    }
    private void UpdateLivesText(int lives)
    {
        LivesText.text = "Lives: " + lives;
    }

    private void UpdateTimerText(float timerTime)
    {
        TimerText.text = "Timer: " + timerTime;
    }
}
