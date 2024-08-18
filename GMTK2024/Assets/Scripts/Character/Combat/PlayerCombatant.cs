using UnityEngine;

public class PlayerCombatant :Combatant
{
    public float HitInvinciblityDuration = 2f;
    public override void TakeDamage(float amount){
        if(!invincible && !dead){
            entityStats.currentHealth -= amount;
            base.TakeDamage(amount);
            Debug.Log($"{gameObject.name} took {amount} damage. Remaining health: {entityStats.currentHealth}");

            if (entityStats.currentHealth <= 0)
            {
                Die();
            }else{
                 SetInvincibility(HitInvinciblityDuration);
                 
            }
        }
    }
    protected override void Die()
    {
        Debug.Log("death player");
        base.Die();
        // You can add enemy-specific death logic here
        GetComponentInParent<CharacterController2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
