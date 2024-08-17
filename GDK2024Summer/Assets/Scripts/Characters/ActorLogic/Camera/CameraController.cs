using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMode
{
    Traversal,
}

public class CameraController : MonoBehaviour
{
    public CameraMode currentMode = CameraMode.Traversal;
    public Vector3 traversalPosition;
    private Vector3 currentOffset;

    public Transform followTarget;
    public float teleportDistanceThreshold = 75f;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.1f;

    [Header("Movement Look Ahead")]
    public float lookAheadScalar = 0;

    private CameraEffects cameraEffects;
    private SpringCollider springCollider;

    private Vector3 previousMovementDirection = Vector3.zero; // Store the previous movement direction

    private void Start()
    {
        cameraEffects = GetComponent<CameraEffects>();
        springCollider = GetComponent<SpringCollider>();
        if (springCollider != null)
        {
            springCollider.enabled = false;
        }
        if (currentMode == CameraMode.Traversal)
        {
            SwitchToTraversalMode();
        }
    }

    private void LateUpdate()
    {
        if (currentMode == CameraMode.Traversal)
        {
            if (followTarget != null)
            {
                UpdateTraversalCamera();
            }
        }
    }

    private void MoveCamera(Vector3 targetPosition)
    {
        if (springCollider != null)
        {
            // Set the camera's local position based on the offset
            transform.localPosition = targetPosition;
            springCollider.springLength = Vector3.Distance(transform.position, followTarget.position);
        }
    }
    private Coroutine smoothMoveCoroutine; // Track the currently running smooth move coroutine

    public void SmoothMoveCamera(Vector3 targetPosition)
    {
        // Cancel any existing smooth move before starting a new one
        CancelSmoothMove();

        if (springCollider != null)
        {
            // Start a new coroutine to handle the smooth movement and delayed re-enabling
            smoothMoveCoroutine = StartCoroutine(SmoothMoveCoroutine(targetPosition));
        }
    }

    private IEnumerator SmoothMoveCoroutine(Vector3 targetPosition)
    {
        // Disable the maximum length check during the smooth movement
        bool originalMaxCheckState = springCollider.enableMaxLengthCheck;
        springCollider.enableMaxLengthCheck = false;

        // Perform the smooth movement
        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.01f)
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref velocity, smoothTime);
            springCollider.springLength = Vector3.Distance(transform.position, followTarget.position);
            yield return null; // Wait for the next frame
        }

        // Ensure the final position is set
        transform.localPosition = targetPosition;
        springCollider.springLength = Vector3.Distance(transform.position, followTarget.position);

        // Re-enable the maximum length check after the movement is completed
        springCollider.enableMaxLengthCheck = originalMaxCheckState;

        // Mark the coroutine as finished
        smoothMoveCoroutine = null;
    }

    public void CancelSmoothMove()
    {
        if (smoothMoveCoroutine != null)
        {
            // Stop the running coroutine
            StopCoroutine(smoothMoveCoroutine);
            smoothMoveCoroutine = null;

            // Reset any variables or states as necessary
            if (springCollider != null)
            {
                springCollider.enableMaxLengthCheck = true;
            }

            // Optionally reset the velocity or other related variables
            velocity = Vector3.zero;
        }
    }

    public void SwitchToTraversalMode()
    {
        currentOffset = traversalPosition;
        Camera.main.fieldOfView = 80f;
        transform.rotation = Quaternion.Euler(45, 0, 0);
        currentMode = CameraMode.Traversal;

        // Child the camera to the followTarget
        transform.SetParent(followTarget);

        SetSpringTraversal();
        springCollider.enabled = true;

    }
    void SetSpringTraversal()
    {
        // Set the camera's local position to the traversal offset
        transform.localPosition = currentOffset;

        if (springCollider != null)
        {
            springCollider.targetObject = followTarget;
            springCollider.springLength = Vector3.Distance(transform.position, followTarget.position);
            springCollider.minimumDistance = .5f;
            springCollider.collisionLayers = ~LayerMask.GetMask("IgnoreCamera");

        }

    }
    public void TriggerShake()
    {
        cameraEffects.TriggerShake();
    }

    private void UpdateTraversalCamera()
    {
        Vector3 desiredPosition = CalculateDesiredPosition();
        Vector3 lookaheadPosition = CalculateLookaheadPosition(desiredPosition);

        Vector3 movementDirection = followTarget.GetComponent<PlayerController>().GetMovementDirection();

        if (Vector3.Distance(transform.position, desiredPosition) > teleportDistanceThreshold)
        {
            if (springCollider != null)
            {
                MoveCamera(desiredPosition);
            }
        }
        else if (movementDirection != previousMovementDirection)
        {
            if (springCollider != null)
            {
                SmoothMoveCamera(lookaheadPosition);
            }
            previousMovementDirection = movementDirection; // Update the previous movement direction
        }
    }

    private Vector3 CalculateDesiredPosition()
    {
        // Calculate the local desired position relative to the follow target
        return currentOffset;
    }

    private Vector3 CalculateLookaheadPosition(Vector3 desiredPosition)
    {
        PlayerController playerController = followTarget.GetComponent<PlayerController>();
        if (playerController != null)
        {
            Vector3 movementDirection = playerController.GetMovementDirection();

            // Flatten the movement direction to the XZ plane by setting Y to 0
            movementDirection.y = 0;

            // Calculate the forward offset for lookahead in the XZ plane
            Vector3 forwardCurrentOffset = lookAheadScalar * movementDirection.normalized * playerController.currentSpeed / 3f;

            return desiredPosition + forwardCurrentOffset;
        }

        return desiredPosition;
    }

}
