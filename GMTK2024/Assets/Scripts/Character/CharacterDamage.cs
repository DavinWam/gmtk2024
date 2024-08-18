using UnityEngine;

public class CharacterDamage : MonoBehaviour
{
    public LayerMask damageableLayers;  // Layers that can cause damage to the character
    public float damageAmount = 10f;    // Amount of damage to apply

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is on a layer that can cause damage
        if (((1 << other.gameObject.layer) & damageableLayers) != 0)
        {
            // Try to get the Combatant component from the collided object
            Combatant combatant = other.GetComponent<Combatant>();
            if (combatant != null)
            {
                // Call the TakeDamage method on the Combatant
                combatant.TakeDamage(damageAmount);
            }
        }
    }
}
