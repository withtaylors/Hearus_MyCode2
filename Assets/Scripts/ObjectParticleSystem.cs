using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectParticleSystem : MonoBehaviour
{
    public static ObjectParticleSystem instance;
    public ParticleSystem particlePrefab;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StopParticle();
    }

    public void PlayParticle()
    {
        particlePrefab.Play();
    }

    public void StopParticle()
    {
        particlePrefab.Stop();
    }
}
