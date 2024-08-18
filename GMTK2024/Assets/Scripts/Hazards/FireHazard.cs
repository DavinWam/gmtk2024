using System.Collections;
using UnityEngine;

public class FireHazard : BasicHazard
{
    private ParticleSystem[] childParticleSystems;
    public override void Awake()
    {
        base.Awake();
        // Get all ParticleSystem components in the children
        childParticleSystems = GetComponentsInChildren<ParticleSystem>();

    }

    // Turn on the fire
    public override void TurnOn()
    {
        base.TurnOn();
        // Enable all ParticleSystems in children
        foreach (var particleSystem in childParticleSystems)
        {
            particleSystem.Play();
        }
        HandleDuration();
    }

    // Turn off the fire
    public override void TurnOff()
    {
        base.TurnOff();
        // Stop all ParticleSystems in children
        foreach (var particleSystem in childParticleSystems)
        {
            particleSystem.Stop();
        }
    }
}
