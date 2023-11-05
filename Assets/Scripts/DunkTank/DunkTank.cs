using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DunkTank : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject Npc;                // This is the NPC that gets dunked. please make sure it has a rigidbody.
    [SerializeField] Transform NpcResetTransform;   // This holds the reset information for the npc. It has to be a different transform than the npc's transform.
    Rigidbody NpcRb;

    [SerializeField] Transform PlatformHinge;       // This is the "hinge" of the platform

    [Header("Values")]
    [SerializeField] float rotSpeed = 1.0f;         // Controls speed of rotation when the dunk tank dunks
    [SerializeField] float resetTime = 2.0f;        // Controls the auto-reset of the platform. If > 0, it resets automatically. Otherwise it does not auto reset.
    float amountRotated = 0f;

    [SerializeField] float SlamForce = 0f;          // How fast you want the npc slammed towards the ground.

    [Header("Debugging")]
    [SerializeField] bool AutoDunk = false;

    bool dunking = false;

    // Call this function to dunk the npc
    public void Dunk()
    {
        dunking = true;

        if(resetTime > 0) Invoke(nameof(ResetPlatform), resetTime);
    }

    // Call this function to reset the platform. Automatically called if resetTime > 0.
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

    private void Awake()
    {
        WaveManager.onWaveComplete += Dunk;
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
