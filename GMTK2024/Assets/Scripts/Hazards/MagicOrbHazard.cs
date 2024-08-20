using UnityEngine;

public class MagicOrbHazard : BasicHazard
{
    public float speed = 5f;                    // Speed at which the orb moves
    public float lifetime = 10f;                // Lifetime of the orb
    public Transform player;                    // Reference to the player
    public float directionUpdateInterval = 0.5f; // Time between direction updates
    public float rotationSpeed = 2f;            // Speed of rotation towards the new direction

    private Vector3 direction;                  // Current direction towards the player
    private float timeSinceLastUpdate;          // Timer to track direction updates

    public override void Awake()
    {
        base.Awake();
        if (player == null)
        {
            // Automatically find the player in the scene if not assigned
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Calculate the initial direction towards the player
        UpdateDirection();

        // Destroy the orb after its lifetime
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Update direction periodically based on the interval
        timeSinceLastUpdate += Time.deltaTime;
        if (timeSinceLastUpdate >= directionUpdateInterval)
        {
            UpdateDirection();
            timeSinceLastUpdate = 0f;
        }

        // Smoothly rotate towards the new direction
        direction = Vector3.Lerp(direction, (player.position - transform.position).normalized, rotationSpeed * Time.deltaTime);

        // Move in the current direction
        if (direction != Vector3.zero)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void UpdateDirection()
    {
        if (player != null)
        {
            direction = (player.position - transform.position).normalized;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject, 0.4f);  // Destroy the orb shortly after hitting the player
        }
    }

    public override void SetDuration(float newDuration)
    {
        lifetime = newDuration;
    }

    public override float GetDuration()
    {
        return lifetime;
    }
}
