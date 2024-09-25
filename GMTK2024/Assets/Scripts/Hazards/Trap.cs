using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Transform spawnLocation;  // Public transform to set spawn location
    public GameObject trapPrefab;    // Prefab to spawn
    public float duration = 5f;
    public float damage = 10.0f;
    public bool debug = false;       // Debug mode to show gizmo
    public bool isPeriodic = false;  // Whether the trap activates periodically
    public float activationInterval = 10f; // Interval between activations when periodic
    private float secondsSinceActivation = 0.0f;
    public float startOffset = 0.0f;

    private Hazard spawnedTrap;      // Reference to the spawned trap
    private Coroutine periodicRoutine;
    public AudioSource audioSource;
    void Start()
    {
        if (isPeriodic)
        {
            StartPeriodicActivation();
        }
        secondsSinceActivation = activationInterval - startOffset;
    }

    void Update()
    {
        if(secondsSinceActivation < activationInterval) {
            secondsSinceActivation += Time.deltaTime;
        }
    }

    public void ActivateTrap()
    {
        audioSource.Play();
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

                DamageTick dt = trapObject.GetComponent<DamageTick>();
                if(dt != null) {
                    dt.damagePerTick = damage;
                }
            }
        }
        else
        {
            Debug.LogError("Trap prefab or spawn location not set.");
        }
    }

    public void StartPeriodicActivation()
    {
        if (periodicRoutine == null)
        {
            periodicRoutine = StartCoroutine(PeriodicActivation());
        }
    }

    public void StopPeriodicActivation()
    {
        if (periodicRoutine != null)
        {
            StopCoroutine(periodicRoutine);
            periodicRoutine = null;
        }
    }

    private IEnumerator PeriodicActivation()
    {
        while (true)
        {
            if(secondsSinceActivation < activationInterval) {
                yield return new WaitForSeconds(activationInterval - secondsSinceActivation);
            }
            ActivateTrap();
            secondsSinceActivation = 0.0f;
            yield return new WaitForSeconds(activationInterval);
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
