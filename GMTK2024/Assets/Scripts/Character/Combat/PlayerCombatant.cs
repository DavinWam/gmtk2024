using UnityEngine;

public class PlayerCombatant :Combatant
{
    // The base class already handles the damage logic
    protected override void Die()
    {
        // You can add enemy-specific death logic here
        base.Die();
    }
}
