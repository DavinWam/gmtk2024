using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportLocation; // The location to which the player will be teleported
    public Color gizmoColor = Color.green; // Color of the Gizmo in the editor
    private Transform playerTransform;
    void Start(){

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            TeleportPlayer(other.transform);
        }
    }

    public void TeleportPlayer(Transform transformToTeleport)
    {
        // Teleport the player to the specified location
        if (teleportLocation != null)
        {
            transformToTeleport.position = teleportLocation.position;
            Debug.Log("Player teleported to " + teleportLocation.position);
        }
        else
        {
            Debug.LogError("Teleport location not set.");
        }
    }
    public void TeleportPlayer(Transform transformToTeleport,Vector3 location)
    {
        // Teleport the player to the specified location
            transformToTeleport.position = location;
            Debug.Log("Player teleported to " + location);
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
