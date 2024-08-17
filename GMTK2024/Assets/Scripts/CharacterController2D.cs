using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask climbableLayer;  // Layer to check for climbable objects
    public float latchDistance = 0.5f;
    public float checkRadius = 0.2f;
    public bool debug = false;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isLatching;
    public float horizontalLatchDistance = 0.5f;  // Latch distance for left and right
    public float verticalLatchDistance = 0.3f;  // Latch distance for up and down
    private Vector2 latchDirection;
    public Transform groundCheck;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isLatching)
        {
            Move();
            Jump();
        }

        CheckForLatch();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, latchDistance, climbableLayer);

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
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
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
                Vector2 latchEndPosition = (Vector2)transform.position + latchDirection * latchDistance;
                Gizmos.DrawLine(transform.position, latchEndPosition);
                Gizmos.DrawWireSphere(latchEndPosition, 0.1f);

                // Draw a wireframe box to represent the latchable area
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(latchEndPosition, new Vector3(0.2f, 0.2f, 0.2f));
            }
        }
    }
}
