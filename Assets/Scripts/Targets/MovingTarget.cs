using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : Target
{
    [Header("Movement")]
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float boundLeft, boundRight, boundUp, boundDown, boundFwd, boundBack;

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (transform.position.x > boundRight || transform.position.x < boundLeft)
        {
            movementVector.x = -movementVector.x;
        }
        if (transform.position.y > boundUp || transform.position.y < boundDown)
        {
            movementVector.y = -movementVector.y;
        }
        if (transform.position.z > boundFwd || transform.position.z < boundBack)
        {
            movementVector.z = -movementVector.z;
        }

        transform.position += movementVector * Time.deltaTime;
    }
}
