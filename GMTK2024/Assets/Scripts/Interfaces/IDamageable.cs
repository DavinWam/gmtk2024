using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float amount);
    Stats GetStats();
    event System.Action<float> OnDamageTaken;  // Event to notify when damage is taken

}
