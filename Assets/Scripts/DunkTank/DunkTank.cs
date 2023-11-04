using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DunkTank : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject Npc;
    [SerializeField] Transform NpcResetTransform;
    Rigidbody NpcRb;

    [SerializeField] Transform PlatformHinge;

    [Header("Values")]
    [SerializeField] float rotSpeed = 1.0f;
    [SerializeField] float resetTime = 2.0f;
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

        if(resetTime > 0) Invoke(nameof(ResetPlatform), resetTime);
    }

    public void ResetPlatform()
    {
        dunking = false;
        NpcRb.isKinematic = true;

        PlatformHinge.Rotate(Vector3.right, -amountRotated);
        amountRotated = 0f;

        Npc.transform.position = NpcResetTransform.position;
        Npc.transform.rotation = NpcResetTransform.rotation;

        NpcRb.isKinematic = false;
    }

    private void Start()
    {
        if (AutoDunk) Invoke(nameof(Dunk), 1f);

        if(Npc == null)
        {
            Debug.LogError("DunkTank.cs error: Npc reference not set.");
            Destroy(gameObject);
            return;
        }

        NpcResetTransform.position = Npc.transform.position;
        NpcResetTransform.rotation = Npc.transform.rotation;
        NpcRb = Npc.GetComponent<Rigidbody>();
        
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
