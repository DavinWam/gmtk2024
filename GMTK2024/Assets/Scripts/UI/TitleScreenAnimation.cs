using UnityEngine;
using UnityEngine.Events;

public class TitleScreenAnimation : MonoBehaviour
{
    public UnityEvent onPlayerEnter;  // Event to trigger when the player enters the hitbox
    public UnityEvent onFinishedWalk; // Event to trigger when the player has finished walking

    void OnTriggerEnter2D(Collider2D other)
    {        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Get the CharacterController2D component from the player
            CharacterController2D playerController = other.GetComponent<CharacterController2D>();


            playerController.SetLock(true);
            if (playerController.GetLock()) Debug.Log("locked movement");

            // Assuming the player's Rigidbody2D is needed to stop movement
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                Debug.Log("halt");
                playerRigidbody.velocity = Vector2.zero;  // Set the player's velocity to zero
            }

            if (playerController != null)
            {
                playerController.animationController2D.OnFinishedIntroWalk += TriggerFinishedWalk;
                if (playerController.IsGrounded())
                {
                    onPlayerEnter?.Invoke();  // Invoke the Unity event
                }
                else
                {
                    // Subscribe to the OnGrounded event
                    playerController.OnGrounded += HandlePlayerGrounded;
                }

            }
        }


    }

    private void HandlePlayerGrounded()
    {
        onPlayerEnter?.Invoke();  // Invoke the Unity event


        CharacterController2D playerController = GetComponent<CharacterController2D>();
        if (playerController != null)
        {
            playerController.OnGrounded -= HandlePlayerGrounded;
        }
    }
    private void TriggerFinishedWalk()
    {
        Debug.Log("Player finished walking");
        onFinishedWalk?.Invoke();  // Invoke the Unity event for finished walk
    }
}
