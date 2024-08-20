using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public string checkpointID; // Unique ID for the checkpoint
    public Transform checkpointTransform; // The transform that represents the checkpoint location
    public Collider2D externalCollider;   // External Collider2D to be enabled when checkpoint is reached

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Set this checkpoint as the current spawn point in the SpawnManager
            SpawnManager.Instance.SetCheckpoint(this);
            Debug.Log("Checkpoint reached! Spawn point set to " + checkpointTransform.position);
        }
    }

    public void EnableCollider()
    {
        // Enable the external collider if it is assigned
        if (externalCollider != null)
        {
            externalCollider.enabled = true;
            Debug.Log("External collider enabled.");
        }
        else
        {
            Debug.LogError("External collider not set.");
        }
    }

    private void OnDrawGizmos()
    {
        if (checkpointTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(checkpointTransform.position, 0.5f);
        }
    }
}
