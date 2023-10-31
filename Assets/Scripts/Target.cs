using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            // Update Score
            // Update wave stuff
            // Destroy Ball
            Destroy(other.gameObject);
            // Destroy Target
            Destroy(gameObject);
        }
    }
}
