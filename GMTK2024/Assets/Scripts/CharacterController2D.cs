using System.Collections;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;

    [Header("Ground")]
    public float checkRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;
    [Header("Latching")]
    public float horizontalLatchDistance = 0.5f;  // Latch distance for left and right
    public float verticalLatchDistance = 0.3f;  // Latch distance for up and down
    public float latchCooldown = 1.0f;  // Cooldown duration in seconds
    public LayerMask climbableLayer;  // Layer to check for climbable objects
    private Vector2 latchDirection;
    private bool isLatching;
    private bool canLatch = true;  // Flag to check if latching is allowed
    public bool debug = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isLatching)
        {
            Move();
        }
        Jump();
        if (canLatch)
        {
            CheckForLatch();
        }
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && (isGrounded || isLatching))
        {
            if(isLatching) ReleaseLatch();       
            Debug.Log("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void CheckForLatch()
    {
        // Check for latch input and direction
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            latchDirection = Vector2.zero;

            if (Input.GetKey(KeyCode.W))
            {
                latchDirection = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                latchDirection = Vector2.down;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                latchDirection = Vector2.left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                latchDirection = Vector2.right;
            }

            if (latchDirection != Vector2.zero)
            {
                AttemptLatch(latchDirection);
            }
        }

        // Release latch when the left shift key is released
        if (isLatching && Input.GetKeyUp(KeyCode.LeftShift))
        {
            ReleaseLatch();
        }
    }

    void AttemptLatch(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, GetLatchDistance(), climbableLayer);

        if (hit.collider != null)
        {
            isLatching = true;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;  // Stop the character's physics movement
        }
    }

    void ReleaseLatch()
    {
        isLatching = false;
        rb.isKinematic = false;
        StartCoroutine(LatchCooldownCoroutine());
    }

    private IEnumerator LatchCooldownCoroutine()
    {
        canLatch = false;
        yield return new WaitForSeconds(latchCooldown);
        canLatch = true;
    }


    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    public float GetLatchDistance(){
        return (latchDirection == Vector2.up || latchDirection == Vector2.down) ? verticalLatchDistance : horizontalLatchDistance;
                
    }
    // Draw the ground check and latching gizmos if debug is enabled
    void OnDrawGizmos()
    {
        if (debug)
        {
            // Draw ground check gizmo
            if (groundCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
            }

            // Draw latch direction gizmo
            if (latchDirection != Vector2.zero)
            {
                Gizmos.color = Color.blue;
                Vector2 latchEndPosition = (Vector2)transform.position + latchDirection * GetLatchDistance();
                Gizmos.DrawLine(transform.position, latchEndPosition);
                Gizmos.DrawWireSphere(latchEndPosition, 0.1f);

                // Draw a wireframe box to represent the latchable area
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(latchEndPosition, new Vector3(0.2f, 0.2f, 0.2f));
            }
        }
    }
}
