using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class RotatingTarget : Target
{
    [SerializeField] private Transform pivotTransform;
    [SerializeField] private float angularSpeed = -5f;

    // Start is called before the first frame update
    void Start()
    {
        if (pivotTransform == null)
            pivotTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        float rotateAmt = angularSpeed * Time.deltaTime;
        transform.RotateAround(pivotTransform.position, Vector3.forward, rotateAmt);
    }
}
