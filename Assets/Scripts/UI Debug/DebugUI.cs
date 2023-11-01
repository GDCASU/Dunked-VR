using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugUI : MonoBehaviour
{
    public void Awake()
    {
        Lives.onLifeUpdate += UpdateLivesText;
        WaveManager.onUpdateTimer += UpdateTimerText;
    }

    public TMP_Text LivesText;
    public TMP_Text TimerText;
    private void UpdateLivesText(int lives)
    {
        LivesText.text = "Lives: " + lives;
    }

    private void UpdateTimerText(float timerTime)
    {
        TimerText.text = "Timer: " + timerTime;
    }
}
