using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Combatant : MonoBehaviour, IDamageable
{
    public Stats entityStats;
    public bool invincible;
    public bool dead = false;
    public event System.Action<float> OnDamageTaken;
    public event System.Action OnDeath;
    public event System.Action<float> OnInvinciblity;

    public Coroutine RemoveIncivilibityCourotine;
    // OnS pawn method to initialize health
    public virtual void Awake()
    {
        dead = false;
       entityStats.InitializeStats();
       Debug.Log($"{gameObject.name} spawned with { entityStats.currentHealth} health.");
    }

    public virtual void TakeDamage(float amount)
    {
        if(!invincible && !dead)OnDamageTaken?.Invoke(amount);  // Trigger the damage event
    }

    public virtual Stats GetStats()
    {
        return entityStats;
    }
   public void SetInvincibility(float duration)
    {
        if (RemoveIncivilibityCourotine != null)
        {
            // If already invincible, reset the timer
            StopCoroutine(RemoveIncivilibityCourotine);
        }
        
        invincible = true;
        OnInvinciblity?.Invoke(duration);
        RemoveIncivilibityCourotine = StartCoroutine(RemoveInvincibilityAfterTime(duration));
    }

    // Coroutine to remove invincibility after the specified duration
    private IEnumerator RemoveInvincibilityAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        invincible = false;
        Debug.Log("Invincibility ended.");
    }
    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} died!");

      
        if(RemoveIncivilibityCourotine != null) StopCoroutine(RemoveIncivilibityCourotine);
        invincible = true;
        dead = true;
        OnDeath?.Invoke();
    }
}
