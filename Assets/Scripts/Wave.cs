using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private WaveDifficulty difficulty = 0;  // Just for classification (SHOULD NOT BE USED)
    
    // Private stuff
    private List<GameObject> targetList;    // List of current targets in wave
    private int numberOfTargets = 0;

    private void Start()
    {
        ITarget.onTargetHit += HandleTargetHit;

        targetList = new List<GameObject>();
        AddTargetsToList();
        numberOfTargets = targetList.Count;
    }

    private void AddTargetsToList()
    {
        for (int i = 0; i < transform.childCount; i++)      // Finds all children of Wave object and stores the targets
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (!targetList.Contains(child))
            {
                targetList.Add(child);
                numberOfTargets = targetList.Count;
            }
        }
    }

    private void HandleTargetHit(GameObject target)
    {
        if (targetList.Remove(target) || target == null)    // Checks if the enemy exists in the first place than removes it
        {
            numberOfTargets--;

            if (numberOfTargets <= 0)           // Invoke the event to move to the next wave and end the current wa
                EndWave();
        }
    }

    private bool CheckListEmpty()
    {
        if (targetList.Count == 1)
        {
            if (targetList[0] == null)
                return true;
        }
        return false;
    }

    private void EndWave()  // Destroy the wave and update status of wave difficulty/etc; used when the player completes a wave
    {
        WaveManager.instance.UpdateWaveCounter();
        Destroy(gameObject);
    }

    public void StopWave()      // Just destroy the wave
    {
        Destroy(gameObject);
    }
}
