using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    public float ActivatedDuration;
    public float DeactivatedDuration;
    public ParticleSystem ParticleSystem;
    public BoxCollider BoxCollider;
    public AudioSource TorchSound;
    public float StartOffset;
    public void Awake()
    {
        BoxCollider.enabled = false;
        ParticleSystem.Stop();
        Invoke("activate", StartOffset);
    }
    public void activate()
    {
        ParticleSystem.Play();
        BoxCollider.enabled = true;
        TorchSound.Play();
        Invoke("deactivate", ActivatedDuration);
    }
    public void deactivate()
    {
        ParticleSystem.Stop();
        BoxCollider.enabled = false;
        TorchSound.Stop();
        Invoke("activate", DeactivatedDuration);
    }
}
