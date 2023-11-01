using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "WavePool", menuName = "Wave Pool")]
public class WavePool : ScriptableObject
{
    [Header("Wave Settings")]
    [SerializeField] private WaveDifficulty waveDifficulty = 0;        // Just for classification (SHOULD NOT BE USED)
    [SerializeField] private List<GameObject> waves = new List<GameObject>();

    public GameObject RandomWaveSelect()      // Select a Random Wave if it doesn't exist then use recursion to select again (highly unlikely)
    {
        if (waves.Count <= 0)       // If no waves currently selected then return null
            return null;

        int roll = Random.Range(0, waves.Count);
        if (waves[roll])
            return waves[roll];
        else
            return RandomWaveSelect();
    }
}
