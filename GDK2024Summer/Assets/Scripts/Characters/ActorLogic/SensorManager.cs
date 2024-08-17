using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorManager : MonoBehaviour
{
    public GameObject sensorPrefab;  // Reference to the sensor prefab
    [Header("SensorManagement")]
    public int sensorCount = 8;  // Number of sensors to spawn
    public float radius = 2.0f;  // Radius from the player to place sensors
    public float minimumDistance = 0.5f;  // Minimum distance for the sensors
    public Vector3 spawnCenterOffset = Vector3.zero;  // Offset for the spawn center
    public LayerMask collisionLayers;  // Layers to check for collisions
    
    [Header("Ledge test")]
    public float MaximumDropHeight = 4f;

    [Header("Debug")]
    public Color defaultGizmoColor = Color.red;  // Default color for the gizmo spheres
    public Color activeGizmoColor = Color.green;  // Color for the active sensor's gizmo sphere
    public bool debugDrawGizmos = false;  // Toggle to draw gizmos for debugging



    private List<GameObject> sensors = new List<GameObject>();
    private GameObject activeSensor;  // Store the active sensor based on movement direction

    void Start()
    {
        SpawnSensors();
    }

    void SpawnSensors()
    {
        for (int i = 0; i < sensorCount; i++)
        {
            float angle = i * Mathf.PI * 2f / sensorCount;  // Calculate the angle for each sensor
            Vector3 sensorPosition = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            GameObject sensor = Instantiate(sensorPrefab, transform.position + sensorPosition + spawnCenterOffset, Quaternion.identity, transform);

            // Set up the SpringCollider on the sensor
            SpringCollider springCollider = sensor.GetComponent<SpringCollider>();
            if (springCollider != null)
            {
                springCollider.targetObject = transform;  // Set the target to the player
                springCollider.springLength = radius;  // Set the spring length to the radius
                springCollider.minimumDistance = minimumDistance;  // Set the minimum distance
                springCollider.collisionLayers = collisionLayers;  // Set the collision layers
                springCollider.dynamicUpdate = false;
            }
            Sensor sense = sensor.GetComponent<Sensor>();
            if (sense != null){
                sense.raycastLength = MaximumDropHeight;
            }
            sensors.Add(sensor);
        }
    }

    // Method to access the sensor most aligned with a given movement vector
    public GameObject GetSensorForMovement(Vector3 movementVector)
    {
        activeSensor = null;
        float maxDot = -1f;

        foreach (GameObject sensor in sensors)
        {
            Vector3 directionToSensor = (sensor.transform.position - transform.position).normalized;
            float dotProduct = Vector3.Dot(movementVector.normalized, directionToSensor);

            if (dotProduct > maxDot)
            {
                maxDot = dotProduct;
                activeSensor = sensor;
            }
        }

        return activeSensor;
    }

    // Method to check for a ledge based on the current movement vector
    public bool CheckLedge(Vector3 movementVector)
    {
        GameObject sensor = GetSensorForMovement(movementVector);
        if (sensor != null)
        {
            return sensor.GetComponent<Sensor>().isLedgeDetected;
        }
        return false;
    }

    // Draws the gizmos in the scene view to visualize sensor positions
    void OnDrawGizmos()
    {
        if (!debugDrawGizmos) return;

        foreach (GameObject sensor in sensors)
        {
            if (sensor == activeSensor)
            {
                Gizmos.color = activeGizmoColor;  // Use the active color for the selected sensor
            }
            else
            {
                Gizmos.color = defaultGizmoColor;  // Use the default color for other sensors
            }

            Gizmos.DrawSphere(sensor.transform.position, 0.2f);  // Adjust the sphere size as needed
        }
    }
}
