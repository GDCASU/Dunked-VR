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
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            // Update Player Score
            // Update wave stuff
            // Destroy Ball
            Destroy(other.gameObject);
            // Destroy Target
            Destroy(gameObject);
        }
    }
}
