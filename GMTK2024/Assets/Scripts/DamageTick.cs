using UnityEngine;
using System;

public class DamageTick : MonoBehaviour
{
    public float damagePerTick = 10f;      // Amount of damage per tick
    public float tickInterval = 1f;        // Time between each tick in seconds
    public bool firstHitImmediate = true;  // Determines if the first hit happens immediately

    private float timeSinceLastTick = 0f;
    private Combatant currentTarget;       // The combatant currently in the trigger zone

    public event Action<Combatant> OnTick; // Event triggered on each tick

    void OnTriggerEnter2D(Collider2D other)
    {
        Combatant target = other.GetComponent<Combatant>();
        if (target != null)
        {
            currentTarget = target;

            if (firstHitImmediate)
            {
                ApplyDamage(target);
                timeSinceLastTick = 0f;  // Reset the timer for the next tick
            }
            else
            {
                timeSinceLastTick = tickInterval;  // Ensure the first tick happens at the next interval
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Combatant target = other.GetComponent<Combatant>();
        if (target != null && currentTarget == target)
        {
            StopDamageTick();
        }
    }

    void Update()
    {
        if (currentTarget != null)
        {
            timeSinceLastTick += Time.deltaTime;

            if (timeSinceLastTick >= tickInterval)
            {
                ApplyDamage(currentTarget);
                timeSinceLastTick = 0f;
            }
        }
    }

    private void ApplyDamage(Combatant target)
    {
        target.TakeDamage(damagePerTick);
        OnTick?.Invoke(target);  // Trigger the tick event
    }

    private void StopDamageTick()
    {
        currentTarget = null;
        timeSinceLastTick = 0f;
    }
}
