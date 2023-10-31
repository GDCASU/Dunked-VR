using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : ITarget
{
    [Header("Movement")]
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float boundX, boundY, boundZ;

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (transform.position.x > boundX || transform.position.x < -boundX)
        {
            movementVector.x = -movementVector.x;
        }
        if (transform.position.y > boundY || transform.position.y < -boundY)
        {
            movementVector.y = -movementVector.y;
        }
        if (transform.position.z > boundZ || transform.position.z < -boundZ)
        {
            movementVector.z = -movementVector.z;
        }

        transform.position += movementVector * Time.deltaTime;
    }
}
