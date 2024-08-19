using UnityEngine;
using UnityEngine.Events;
public class DragonCombatant :Combatant
{
    public DragonCombatant[] linkedSegments;
    // Enemy-specific logic can be added here
    // The base class already handles the damage logic
    public float HitInvinciblityDuration = 2f;
    public event System.Action<bool> OnUnlocked;
    public UnityEvent onDeathUnityEvent;
    public UnityEvent<float> onHitUnityEvent;
    public UnityEvent onUnlockUnityEvent;
    public UnityEvent onLockUnityEvent;

    public override void Awake() {
        base.Awake();
        entityStats = Instantiate(entityStats);
    }

    public override void TakeDamage(float amount) {
        if(!invincible && !dead){
            entityStats.currentHealth -= amount;
            base.TakeDamage(amount);
            Debug.Log($"{gameObject.name} took {amount} damage. Remaining health: {entityStats.currentHealth}");
            onHitUnityEvent.Invoke(amount);
            if (entityStats.currentHealth <= 0) {
                Die();
            }
            else {
                 SetInvincibility(HitInvinciblityDuration);
            }
        }
    }

    protected override void Die() {
        // You can add enemy-specific death logic here
        base.Die();
        ProgressSegments();
        onDeathUnityEvent.Invoke();
    }

    private void ProgressSegments() {
        foreach(DragonCombatant segment in linkedSegments){
            segment.unlock(true);
        }
    }

    public void unlock(bool unlock) {
        if(unlock) {
            SetUnlock();
        }
        else {
            SetLock();
        }

    }

    public void SetLock() {
        invincible = true;
        OnUnlocked?.Invoke(false);
        onUnlockUnityEvent.Invoke();
    }

    public void SetUnlock() {
        invincible = false;
        Debug.Log(gameObject.name);
        OnUnlocked?.Invoke(true);
        onLockUnityEvent.Invoke();
    }
}
