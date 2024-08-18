using System.Collections;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    private Rigidbody2D rb;
    private Collider2D col;

    [Header("Ground")]
    public float checkRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;
    public float coyoteTime = 0.1f;
    [Header("Latching")]
    public float horizontalLatchDistance = 0.5f;  // Latch distance for left and right
    public float verticalLatchDistance = 0.3f;  // Latch distance for up and down
    public float latchCooldown = 1.0f;  // Cooldown duration in seconds
    public float longLatchCooldown = 1.0f;
    public float maxLatchStamina = 100.0f;
    public float staminaDrainMultiplier = 10.0f;
    public float staminaGainMultiplierAir = 2.0f;
    public float staminaGainMultiplierGround = 30.0f;
    public float currLatchStamina;
    public LayerMask climbableLayer;  // Layer to check for climbable objects
    private Vector2 latchDirection;
    private bool isLatching;
    private bool canLatch = true;  // Flag to check if latching is allowed
    public bool debug = false;

    public float attackCooldown = .3f;
    private bool canAttack= true;
    private bool moveLock = false;
    private bool inCoyoteTime = false;
    private bool prevTouchCheck = true;
    private bool isJumping = false;
    public AnimationController2D animationController2D;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        currLatchStamina = maxLatchStamina;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if(isLatching) {
            currLatchStamina -= Time.deltaTime * staminaDrainMultiplier;
        }
        else {
            float gain = isGrounded ? staminaGainMultiplierGround : staminaGainMultiplierAir;
            currLatchStamina += Time.deltaTime * gain;
            if(currLatchStamina > maxLatchStamina) {
                currLatchStamina = maxLatchStamina;
            }
        }

        Move();
        Latch();
        if(isGrounded || isLatching) {
            isJumping = false;
        }

        if(!isGrounded && !isLatching && prevTouchCheck && !isJumping) {
            StartCoroutine(CoyoteTimeCoroutine());
        }
        prevTouchCheck = isGrounded || isLatching;

        Jump();
        Attack();

        if(isLatching && currLatchStamina <= 0.0f) {
            ReleaseLatch(longLatchCooldown);
        }
    }

    private void Attack(){
        if(Input.GetMouseButtonDown(0) && canAttack){
            animationController2D.Attack();
            StartCoroutine(AttackCooldownCoroutine());
        }
    }

    private IEnumerator AttackCooldownCoroutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void Move()
    {
        if(moveLock || isLatching) return;

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && (isGrounded || isLatching || inCoyoteTime))
        {
            float x = 0.0f;
            float y = jumpForce;
            if(!canLatch) {
                ReleaseLatch(latchCooldown);
                x = Input.GetAxis("Horizontal") * moveSpeed * -1.0f;
                if(latchDirection == Vector2.down)
                    y *= -1.0f;
            }
            rb.velocity = new Vector2(rb.velocity.x + x, y);
            isJumping = true;
        }
    }

    Vector2 getInputDirection() {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            return Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            return Vector2.down;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            return Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            return Vector2.right;
        }

        return Vector2.zero;
    }

    void Latch()
    {
        Vector2 lookDirection = getInputDirection();

        if(isLatching && lookDirection != latchDirection) {
            ReleaseLatch(latchCooldown);
        }

        if(!canLatch) return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDirection, GetLatchDistance(), climbableLayer);

        if (hit.collider != null)
        {
            isLatching = true;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;  // Stop the character's physics movement
            canLatch = false;
            latchDirection = lookDirection;
            transform.position = new Vector3(
                    hit.point.x - (latchDirection.x * col.bounds.size.x)/2.0f,
                    hit.point.y - (latchDirection.y * col.bounds.size.y)/2.0f,
                    transform.position.z);
        }
    }

    void ReleaseLatch(float cooldown)
    {
        isLatching = false;
        rb.isKinematic = false;
        StartCoroutine(LatchCooldownCoroutine(cooldown));
    }

    private IEnumerator LatchCooldownCoroutine(float cooldown)
    {
        canLatch = false;
        moveLock = true;
        yield return new WaitForSeconds(cooldown);
        canLatch = true;
        moveLock = false;
    }

    private IEnumerator CoyoteTimeCoroutine()
    {
        inCoyoteTime = true;
        yield return new WaitForSeconds(coyoteTime);
        inCoyoteTime = false;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }
    public bool IsLatching(){
        return isLatching;
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
