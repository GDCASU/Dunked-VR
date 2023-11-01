using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreToLeaderboard: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputscore;
    [SerializeField] private TMP_InputField inputname;

    public UnityEvent<string, int> submitScoreEvent;

    public void SubmitScore()
    {
        submitScoreEvent.Invoke(inputname.text, int.Parse(inputscore.text));
    }
}
