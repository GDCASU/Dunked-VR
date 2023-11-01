using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public abstract class ITarget : MonoBehaviour
{
    // Events
    public static event Action<GameObject> onTargetHit;

    [Header("Score")]
    [SerializeField] protected int score = 10;

    [Header("Debug")]
    [SerializeField] bool debug;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (debug) Debug.Log(gameObject.name + " collided with " + other.gameObject.name);
        if (other.gameObject.tag == "Ball")
        {
            if (debug) Debug.Log(other.gameObject.name + " was recognized as \"Ball\".");
            // Update Player Score
            // Update wave stuff
            onTargetHit.Invoke(gameObject);
            // Destroy Ball
            Destroy(other.gameObject);
            // Destroy Target
            Destroy(gameObject);
        }
        else
        {
            if (debug) Debug.Log(other.gameObject.name + " was NOT recognized as \"Ball\".");
        }
    }
}
