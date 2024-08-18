using UnityEngine;

public class FireHazard : MonoBehaviour
{
    private ParticleSystem[] childParticleSystems;
    private Collider2D fireCollider;

    void Start()
    {
        // Get all ParticleSystem components in the children
        childParticleSystems = GetComponentsInChildren<ParticleSystem>();

        // Get the BoxCollider2D component on this GameObject
        fireCollider = GetComponent<Collider2D>();

        if (fireCollider == null)
        {
            Debug.LogError("BoxCollider2D component not found on this object. Please ensure it is attached.");
        }
    }

    // Turn on the fire
    public void TurnOn()
    {
        // Enable the BoxCollider2D
        if (fireCollider != null)
        {
            fireCollider.enabled = true;
        }

        // Enable all ParticleSystems in children
        foreach (var particleSystem in childParticleSystems)
        {
            particleSystem.Play();
        }
    }

    // Turn off the fire
    public void TurnOff()
    {
        // Disable the BoxCollider2D
        if (fireCollider != null)
        {
            fireCollider.enabled = false;
        }

        // Stop all ParticleSystems in children
        foreach (var particleSystem in childParticleSystems)
        {
            particleSystem.Stop();
        }
    }
}
