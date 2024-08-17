using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    // Static instance to be accessed globally
    public static PlayerCharacter Instance { get; private set; }

    private PlayerController controller;
    public PlayerCombatant combatComponent;
    private GameObject playerObject;

    // Start is called before the first frame update
    void Awake()
    {
        // Check if an instance already exists and enforce the singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy this instance if another one exists
            return;
        }

        Instance = this; // Set the instance to this one
        DontDestroyOnLoad(gameObject); // Optionally keep this object alive across scenes

        // Grab the references off the object this script is attached to
        controller = GetComponent<PlayerController>();
        playerObject = gameObject; 

        if(combatComponent != null){
            combatComponent = Instantiate(combatComponent);
        }
        // Optional: Add a check to ensure the components were found
        if(controller == null)
        {
            Debug.LogWarning("PlayerController component is missing on the PlayerCharacter object.");
        }

    }

    // Public getter functions to reduce coupling
    public PlayerController GetController()
    {
        return controller;
    }

    public PlayerCombatant GetCombatComponent()
    {
        return combatComponent;
    }

    public GameObject GetPlayerObject()
    {
        return playerObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
