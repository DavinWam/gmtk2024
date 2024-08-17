using UnityEngine;

public class EnemyCombatant :Combatant
{
    // Enemy-specific logic can be added here
    // The base class already handles the damage logic

    protected override void Die()
    {
        // You can add enemy-specific death logic here
        base.Die();
    }
}
