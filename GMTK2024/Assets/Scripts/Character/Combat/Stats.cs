using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Character/Stats")]
public class Stats : ScriptableObject
{
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    // You can add more stats here, like attack, defense, etc.
    public void InitializeStats(){
        currentHealth =  maxHealth;
    }
}
