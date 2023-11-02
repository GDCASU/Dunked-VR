using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{

    [SerializeField] private TMP_Text time;
    // Start is called before the first frame update
    void Start()
    {
        WaveManager.onUpdateTimer += UpdateTimeText;
    }

    public void UpdateTimeText(float timeremaining)
    {
        timeremaining = Mathf.Floor(timeremaining) + 1;
        time.text = timeremaining.ToString();
    }
}
