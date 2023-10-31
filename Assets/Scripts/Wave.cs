using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private WaveDifficulty difficulty = 0;  // Just for classification (SHOULD NOT BE USED)
    
    private List<GameObject> targetList;
    private int numberOfTargets = 0;

    private float timerTime = 5f;

    private void Start()
    {
        ITarget.onTargetHit += HandleTargetHit;

        targetList = new List<GameObject>();
        AddTargetsToList();
        numberOfTargets = targetList.Count;
    }

    private void Update()
    {
        CheckTimer();
    }

    private void AddTargetsToList()
    {
        for (int i = 0; i < transform.childCount; i++)      // Finds all children of Wave object and stores th
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
                EndOfWave();
        }
    }

    private void CheckTimer()
    {
        bool isEmpty = false;

        if (timerTime > 0)
            timerTime -= Time.deltaTime;
        else
        {
            isEmpty = CheckListEmpty();
            timerTime = 5f;
        }

        if (isEmpty)
            EndOfWave();
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

    private void EndOfWave()
    {
        WaveManager.instance.UpdateWaveCounter();
        WaveManager.instance.SpawnWave();
        Destroy(gameObject);                  // MAY USE TO CLEAN UP CLUTTER IN GAME
    }


}
