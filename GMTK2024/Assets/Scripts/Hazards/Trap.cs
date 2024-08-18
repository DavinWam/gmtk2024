using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Transform spawnLocation;  // Public transform to set spawn location
    public GameObject trapPrefab;    // Prefab to spawn
    public float duration = 5f;
    public bool debug = false;       // Debug mode to show gizmo

    private Hazard spawnedTrap;      // Reference to the spawned trap
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateTrap()
    {
        Debug.Log("Trap activated!");
        // Spawn the trap at the designated location
        if (trapPrefab != null && spawnLocation != null)
        {
            GameObject trapObject = Instantiate(trapPrefab, spawnLocation.position, spawnLocation.rotation);
            spawnedTrap = trapObject.GetComponent<Hazard>();
            
            if (spawnedTrap != null)
            {
                spawnedTrap.SetDuration(duration);
                spawnedTrap.TurnOn();  // Turn on the trap when it spawns
            }
        }
        else
        {
            Debug.LogError("Trap prefab or spawn location not set.");
        }
    }
    void OnDrawGizmos()
    {
        if (debug && spawnLocation != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(spawnLocation.position, 0.2f);
            Gizmos.DrawLine(transform.position, spawnLocation.position);
        }
    }
}
