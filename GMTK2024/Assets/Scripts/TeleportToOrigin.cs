using UnityEngine;

public class TeleportToOrigin : MonoBehaviour
{
    public Color gizmoColor = Color.green; // Color of the Gizmo in the editor
    public Transform playerTransform;
    void Start(){
        if(playerTransform.position != Vector3.zero){
            TeleportPlayer(playerTransform,Vector3.zero);

        }
    }

    public void TeleportPlayer(Transform transformToTeleport,Vector3 location)
    {
        // Teleport the player to the specified location
            transformToTeleport.position = location;
            Debug.Log("Player teleported to " + location);
    }

}
