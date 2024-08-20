using UnityEngine;

public class TeleportOnTouch : MonoBehaviour
{
    public Transform teleportLocation; // The location to which the player will be teleported
    public Color gizmoColor = Color.green; // Color of the Gizmo in the editor

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            TeleportPlayer(other.transform);
        }
    }

    private void TeleportPlayer(Transform playerTransform)
    {
        // Teleport the player to the specified location
        if (teleportLocation != null)
        {
            playerTransform.position = teleportLocation.position;
            Debug.Log("Player teleported to " + teleportLocation.position);
        }
        else
        {
            Debug.LogError("Teleport location not set.");
        }
    }

    public void SetTeleportLocation(Transform newLocation)
    {
        teleportLocation = newLocation;
        Debug.Log("Teleport location set to " + newLocation.position);
    }

    private void OnDrawGizmos()
    {
        if (teleportLocation != null)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(teleportLocation.position, 0.5f); // Draws a wire sphere at the teleport location
            Gizmos.DrawLine(transform.position, teleportLocation.position); // Draws a line from the object to the teleport location
        }
    }
}
