using UnityEngine;

public class Sensor : MonoBehaviour
{
    public float raycastLength = 2.0f;  // Length of the raycast for ledge detection
    public LayerMask collisionLayers;  // Layers to check for collisions
    public bool isLedgeDetected { get; private set; }  // Public property to check if a ledge is detected

    private SpringCollider springCollider;  // Reference to the SpringCollider component

    void Start()
    {
        springCollider = GetComponent<SpringCollider>();
        if (springCollider == null)
        {
            Debug.LogError("SpringCollider component missing on sensor.");
        }
    }
 
    void Update()
    {
        CheckForLedge();
    }

    void CheckForLedge()
    {
        isLedgeDetected = true;

        // Perform the raycast from the sensor's position downwards
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastLength, collisionLayers))
        {
            // Check if the raycast hit something within the specified length
            if (hit.distance < raycastLength)
            {
                isLedgeDetected = false;
                // Optional: Do something when a ledge is detected
                Debug.DrawLine(transform.position, hit.point, Color.red);
            }
        }
        else
        {
            // Optional: Debug draw when no ledge is detected
            Debug.DrawRay(transform.position, Vector3.down * raycastLength, Color.green);
        }
    }
}
