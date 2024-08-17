using UnityEngine;
using System.Collections.Generic;
using System;



public class CombatCharacter : ScriptableObject
{

    [Header("Character Attributes")]
    public string characterName;
    public Sprite characterSprite;
    private bool isAlive = true;
    public float distanceCovered;

    [Header("Character Stats & Status Effects")]
    public CharacterStats characterStats;


    // Property to get and set the alive status
    public bool IsAlive
    {
        get
        {
            // Ensure that the character is considered alive only if their health is >= 0
            return characterStats.currentHealth > 0;
        }
        set
        {
            // Set the internal alive status
            isAlive = value;
        }
    }
    public virtual CharacterStats GetStats()
    {
        return this.characterStats; // This assumes that all characters have a field or property named characterStats.
    }


    public Sprite GetSprite(){
        return characterSprite;
    }
    // Damage-related functionalities
    public virtual void TakeDamage(float damage,bool isCritical,bool isWeak,bool isBlock)
    {
        characterStats.currentHealth -= damage;

        if (characterStats.currentHealth <= 0)
        {
            IsAlive = false;
            Die();
        }
    }

    public void Heal(float healAmount, bool isCritical){
        characterStats.Heal(healAmount);
    }

    public virtual void Attack()
    {
        Debug.Log("attacked from character base");
        //just a blueprint
    }
    public virtual void CastSpell()
    {
        Debug.Log("casted spell from character base");
        //just a blueprint
    }
    public virtual void CastSpell(Spell spell, CombatCharacter target)
    {
        // Apply the spell effects on the target
        // This might involve adding more methods in Spell or using delegate/callback mechanisms.
        //spell.ApplyEffect(target);

    }

    public virtual Act Act()
    {
        // Default action for the base character; likely overridden by subclasses.
        return new Act { actionType = ActionType.EMPTY};
    }

    public virtual void Die()
    {
        Debug.Log(characterName + "has died");
        IsAlive = false;
        // Handle death logic here
        // This could include animations, gameplay mechanics, etc.
    }

}

// Note: You would need to provide implementations for `CharacterStats` and `StatusEffect` classes.
// The above code assumes these classes have certain methods or properties, but you can adjust as necessary.
