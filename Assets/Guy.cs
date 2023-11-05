using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        WaveManager.onWaveComplete += Cheer;
        LivesManager.onStrike += Boo;
    }

    public void Cheer()
    {
        anim.SetTrigger("Cheer");
    }

    public void Boo()
    {
        anim.SetTrigger("Boo");
    }
}
