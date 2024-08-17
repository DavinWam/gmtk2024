using UnityEngine;

public class SpringCollider : MonoBehaviour
{
    public Transform targetObject;  // The object at the end of the spring.
    public float springLength = 5.0f;  // The maximum length of the "spring".
    public float minimumDistance = 1.0f;  // The minimum distance the spring can be from the target.
    public LayerMask collisionLayers;  // Layers to check for collisions.
    public float radius = 0.5f;  // Radius for collision detection.
    public bool enableMaxLengthCheck = true; // Flag to enable/disable max length check
    public bool dynamicUpdate = true; // Flag to enable/disable max length check
    private Vector3 initialRelativeDirection;  // The initial direction to maintain orientation

    // Method to set up the initial relative direction from the target
    public void UpdateRelativeDirection()
    {
        initialRelativeDirection = (transform.position - targetObject.position).normalized;
    }

    void Start()
    {
        // Initialize the initial relative direction
        UpdateRelativeDirection();
    }

    void LateUpdate()
    {
        ManageSpring();
    }

    void ManageSpring()
    {
        if(dynamicUpdate){
            // Dynamically update the relative direction
            UpdateRelativeDirection();
        }


        // Calculate the desired position based on the initial relative direction and spring length
        Vector3 desiredPosition = targetObject.position + initialRelativeDirection * springLength;

        // Perform a sphere cast to detect potential collisions
        RaycastHit hit;
        if (Physics.SphereCast(targetObject.position, radius, initialRelativeDirection, out hit, springLength, collisionLayers))
        {
            // If a collision is detected, adjust the position based on the hit point
            float distanceToHit = Vector3.Distance(targetObject.position, hit.point);

            // Ensure the position doesn't go inward past the minimum distance
            transform.position = targetObject.position + initialRelativeDirection * Mathf.Max(distanceToHit, minimumDistance);
        }
        else
        {
            // Only enforce the max length if the check is enabled
            if (enableMaxLengthCheck)
            {
                // Ensure the desired position doesn't go inward past the minimum distance
                float distanceToDesired = Vector3.Distance(targetObject.position, desiredPosition);
                transform.position = targetObject.position + initialRelativeDirection * Mathf.Max(distanceToDesired, minimumDistance);
            }
            else
            {
                // Directly set the position if max length check is disabled
                transform.position = desiredPosition;
            }
        }
    }
}
