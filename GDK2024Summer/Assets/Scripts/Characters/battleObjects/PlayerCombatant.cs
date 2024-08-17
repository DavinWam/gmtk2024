using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[CreateAssetMenu(fileName = "NewPlayerCombatant", menuName = "Combatant/PlayerCombatant")]

public class PlayerCombatant : CombatCharacter
{
    private Act currentAction;
    public event Action<Act> OnActionDecided = delegate { };
    private Spell selectedSpell;
    public GameObject wings; 
    public GameObject levelUpTextPrefab; // Assign this in the Inspector
    public List<Spell> spellList; // The list of spells currently available to the character.
    public List<Gear> equippedGear; // Gear currently equipped by the player character, organized by equipment slots.

 
    private void Start(){
        PlayerStats ps = characterStats as PlayerStats;
        ps.CalculateExpToNextLevel();
    }
    private void Update()
    {

    }
    public override void Attack()
    {
    }
    public override void CastSpell()
    {
    }
    public void UseMana(float cost)
    {
        // Ensure characterStats is of type PlayerStats and then deduct mana.
        if(characterStats is PlayerStats playerStatsInstance)
        {
            playerStatsInstance.currentMana -= cost;
            if (playerStatsInstance.currentMana < 0)
            {
                playerStatsInstance.currentMana = 0;
            }
        }
    }
    public override void TakeDamage(float damage,bool isCritical,bool isWeak,bool isBlock)
    {
        characterStats.currentHealth -= damage;
        if(characterStats.currentHealth <= 0)
        {
            characterStats.currentHealth = 0;
            IsAlive = false;
        }
        Debug.Log("playey took "+ damage+" damage.");
        Debug.Log("player health: "+characterStats.currentHealth);
      //  FindObjectOfType<BattleController>().DealDamage(isCritical,isWeak,isBlock,this.transform.position, (int)(damage+.5f));
        if (characterStats.currentHealth <= 0)
        {
            base.Die();
        }
    }


    public void ToggleWings(bool on){

        if(on == true){
            //return early if already on
            if(wings.activeSelf){ return;}

            wings.SetActive(true);
            wings.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color",FireColor.GetFireTypeColor(4f));
            wings.GetComponentInChildren<MeshRenderer>().material.SetColor("_FresnelColor", FireColor.GetComplementaryColor(FireColor.GetFireTypeColor(12f)));
            //sets light to a paler version of the flame color
            float saturation = 0f;
            float hue = 0f;
            float value = 0f;
            Color.RGBToHSV(FireColor.GetFireTypeColor(),out hue,out saturation,out value);
            saturation /= 2f;

            wings.GetComponentInChildren<Light>().color = Color.HSVToRGB(hue, saturation, value);
        }else{
            wings.SetActive(false);
        }
    }
 
    public PlayerStats GetPlayerStats(){
        return characterStats as PlayerStats;
    }
    public List<Gear> GetGear(){
        return equippedGear;
    }
}


