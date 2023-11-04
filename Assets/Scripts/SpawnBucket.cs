using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBucket : MonoBehaviour
{
    public GameObject ballPrefab;

    private GameObject currentBall;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentBall) 
        {
            SpawnBall();
        }
    }

    private void Start()
    {
        SpawnBall();
    }

    private void SpawnBall()
    {
        currentBall = Instantiate(ballPrefab, gameObject.transform.position, Quaternion.identity);
    }
}
