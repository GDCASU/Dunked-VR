using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class RotatingTarget : Target
{
    [SerializeField] private Transform pivotTransform;
    [SerializeField] private float moveSpeed = -5f;
    [SerializeField] private float angularSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        if (pivotTransform == null)
            pivotTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        float speed = moveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(speed, 0, 0));
    }

    void Rotate()
    {
        float rotateAmt = angularSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotateAmt);
    }
}
