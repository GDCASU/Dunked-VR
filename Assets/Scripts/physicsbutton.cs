using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class physicsbutton : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;

    private bool ispressed;
    private Vector3 startpos;
    private ConfigurableJoint joint;
    public UnityEvent onPress, OnRelease;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
        audio = GetComponent<AudioSource>();
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ispressed && GetValue() + threshold >= 1)
            Pressed(); 
        if (ispressed && GetValue() - threshold <= 0)
            Released();
    }

    private float GetValue()
    {
        var value = Vector3.Distance(startpos, transform.localPosition) / joint.linearLimit.limit;

        if (Mathf.Abs(value) < deadZone)
            value = 0;
        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed() 
    {
        ispressed = true;
        audio.Play();
        onPress.Invoke();
        Debug.Log("PRESSED");
    }

    private void Released()
    {
        ispressed = false;
        OnRelease.Invoke();
        Debug.Log("RELEASED");
    }
}
