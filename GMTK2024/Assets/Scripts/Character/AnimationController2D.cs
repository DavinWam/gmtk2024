using UnityEngine;

public class AnimationController2D : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private CharacterController2D characterController;

 void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Get the CharacterController2D from the parent object
        characterController = GetComponentInParent<CharacterController2D>();

        // Check if characterController is found
        if (characterController == null)
        {
            Debug.LogError("CharacterController2D not found on parent object. Please ensure it is attached to the parent.");
            return;
        }

        // Get the Rigidbody2D from the CharacterController2D
        rb =  GetComponentInParent<Rigidbody2D>();

        // Check if Rigidbody2D is found
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on CharacterController2D. Please ensure it is attached to the parent.");
            return;
        }

        // Check if animator is found
        if (animator == null)
        {
            Debug.LogError("Animator component not found on this object. Please ensure it is attached.");
        }

        // Check if spriteRenderer is found
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on this object. Please ensure it is attached.");
        }
    }


    void Update()
    {
        FlipSprite();
        UpdateAnimatorParameters();
    }

    void FlipSprite()
    {
        float moveInput = rb.velocity.x;

        if (moveInput > 0.05)
        {
            spriteRenderer.flipX = false; // Facing right
        }
        else if (moveInput < -0.05)
        {
            spriteRenderer.flipX = true; // Facing left
        }
    }

    void UpdateAnimatorParameters()
    {
        bool isGrounded = characterController.IsGrounded(); // Assuming IsGrounded is a method in CharacterController2D
        bool isLatching = characterController.IsLatching();

        if (isLatching)
        {
            animator.SetBool("IsLatching", true);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsJumping", false);
        }else{
             animator.SetBool("IsLatching", false);
            // Check if character is moving horizontally and grounded
            if (Mathf.Abs(rb.velocity.x) > 0.1f && isGrounded)
            {
                animator.SetBool("IsRunning", true);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }

            // Check if the character is not grounded
            if (!isGrounded)
            {
                animator.SetBool("IsJumping", true);
            }
            else
            {
                animator.SetBool("IsJumping", false);
            }
        }

    }
}
