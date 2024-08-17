using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 2.0f;
    public float sprintSpeed = 4.0f; // Sprint speed
    public float currentSpeed;
    private Vector3 playerVelocity = Vector3.zero;
    private bool playerIsGrounded;
    private float gravityValue = -10f;

    [Header("Debug")]
    public bool isRunning = false; // Check if the player is running
    public float checkDistance = 1.0f; // Distance to check ahead for ledges or non-walkable surfaces

    // Components
    private CharacterController characterController;
    private AnimationController animationController; // Reference to AnimationController
    private SoundController soundController; // Reference to SoundController
    private SpriteRenderer spriteRenderer;
    private Transform dust;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Ensure the AnimationController component is present
        animationController = GetComponent<AnimationController>();
        if (animationController == null)
        {
            animationController = gameObject.AddComponent<AnimationController>();
        }

        // Ensure the SoundController component is present
        soundController = GetComponent<SoundController>();
        if (soundController == null)
        {
            soundController = gameObject.AddComponent<SoundController>();
        }

        // Cache the SpriteRenderer and dust references
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        dust = transform.Find("dustps");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public Vector3 GetMovementDirection()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
    }

    public void Move()
    {
        playerIsGrounded = characterController.isGrounded;

        if (playerIsGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = GetMovementDirection();

        // Check if the player is holding the sprint key (left shift)
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        currentSpeed = isSprinting ? sprintSpeed : movementSpeed;


        // Check for ledges using SensorManager
        // if (move != Vector3.zero && playerIsGrounded)
        // {
        //     SensorManager sensorManager = GetComponent<SensorManager>();
        //     if (sensorManager != null && sensorManager.CheckLedge(move))
        //     {
        //         // If a ledge is detected, prevent movement in that direction
        //         move = Vector3.zero;
        //     }
        // }
        
        characterController.Move(move * Time.deltaTime * currentSpeed);
        
        // Flip sprite based on movement direction
        if (move.x != 0f || move.z != 0f) // Check if there is any movement
        {
            isRunning = true;
            if (playerIsGrounded)
            {
                dust.gameObject.SetActive(true);
            }

            if (move.x > 0f) // Moving right
            {
                spriteRenderer.flipX = false;
                dust.rotation = Quaternion.Euler(0f, 180f, 0f); // Right
            }
            else if (move.x < 0f) // Moving left
            {
                spriteRenderer.flipX = true;
                dust.rotation = Quaternion.Euler(0f, 0f, 0f); // Left
            }

            // Handle diagonal movement using Quaternion.LookRotation
            if (move != Vector3.zero)
            {
                dust.rotation = Quaternion.LookRotation(move) * Quaternion.Euler(0f, 90f, 0f);
            }
        }
        else
        {
            isRunning = false;
            dust.gameObject.SetActive(false);
        }

        // Handle animations
        animationController.SetRunningAnimation(isRunning);

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        // Handle sound
        if (isRunning)
        {
            soundController.PlayRunningSound();
        }
        else
        {
            soundController.StopRunningSound();
        }
    }
}
