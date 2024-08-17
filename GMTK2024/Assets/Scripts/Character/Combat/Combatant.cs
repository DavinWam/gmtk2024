using UnityEngine;

public abstract class Combatant : MonoBehaviour, IDamageable
{
    public Stats entityStats;
    public event System.Action<float> OnDamageTaken;

    // OnS pawn method to initialize health
    public virtual void Awake()
    {
       entityStats.InitializeStats();
       Debug.Log($"{gameObject.name} spawned with { entityStats.currentHealth} health.");
    }

    public virtual void TakeDamage(float amount)
    {
        entityStats.currentHealth -= amount;
        OnDamageTaken?.Invoke(amount);  // Trigger the damage event

        Debug.Log($"{gameObject.name} took {amount} damage. Remaining health: {entityStats.currentHealth}");

        if (entityStats.currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual Stats GetStats()
    {
        return entityStats;
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        // Implement death logic here
        Destroy(gameObject);
    }
}
