using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreUI: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputscore;
    [SerializeField] private TMP_InputField inputname;


    public UnityEvent<string, int> submitScoreEvent;
    private void Awake()
    {
        ScoreManager.onScoreUpdate += UpdateScore;
    }
    public void SubmitScore()
    {
        submitScoreEvent.Invoke(inputname.text, int.Parse(inputscore.text));
    }

    public void UpdateScore(int score)
    {
        inputscore.text = score.ToString();
    }
}
