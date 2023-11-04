using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DunkTank : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform PlatformHinge;
    [SerializeField] Rigidbody NpcRb;

    [Header("Values")]
    [SerializeField] float rotSpeed = 1.0f;
    float amountRotated = 0f;

    [SerializeField] float SlamForce = 0f;

    [Header("Debugging")]
    [SerializeField] bool AutoDunk = false;

    bool dunking = false;

    private void Awake()
    {
        WaveManager.onWaveComplete += Dunk;
    }

    public void Dunk()
    {
        dunking = true;
    }

    public void ResetPlatform()
    {
        PlatformHinge.Rotate(Vector3.right, -amountRotated);
        amountRotated = 0f;
    }

    private void Start()
    {
        if (AutoDunk) Invoke(nameof(Dunk), 1f);
    }

    private void Update()
    {
        if (dunking && amountRotated < 90f) RotatePlatform();
    }

    void RotatePlatform()
    {
        PlatformHinge.Rotate(Vector3.right, Time.deltaTime * rotSpeed);
        amountRotated += Time.deltaTime * rotSpeed;

        NpcRb.AddForce(Vector3.down * SlamForce, ForceMode.Impulse);
    }
}
