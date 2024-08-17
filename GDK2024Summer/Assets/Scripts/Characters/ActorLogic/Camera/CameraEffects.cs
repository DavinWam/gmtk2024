using UnityEngine;
using System.Collections;

public class CameraEffects : MonoBehaviour
{
    [Header("Wobble")]
    public float wobbleMagnitude = 0.1f;
    public float wobbleFrequency = 1.0f;
    private float wobbleTime;
    [Header("Shake")]
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.7f;
    public float dampingSpeed = 1.0f;
    private float currentShakeDuration = 0f;
    private bool isShaking = false;
    [Header("Movement")]
    public float smoothTime = 0.1f;


    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {

    }

    private void UpdateWobbleEffect()
    {
        wobbleTime += Time.deltaTime;
        Vector3 wobbleOffset = ComputeWobbleOffset();
        transform.localPosition = initialPosition + wobbleOffset;
    }

    public void TriggerShake()
    {
        currentShakeDuration = shakeDuration;
        isShaking = true;
    }

    private void UpdateShakeEffect()
    {
        if (isShaking)
        {
            if (currentShakeDuration > 0)
            {
                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
                currentShakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                currentShakeDuration = 0f;
                isShaking = false;
                transform.localPosition = initialPosition;
            }
        }
    }

    public IEnumerator SmoothMoveCamera(Vector3 targetPosition, Quaternion targetRotation, float duration)
    {
        float elapsed = 0f;
        Vector3 startPosition = transform.position + new Vector3(0, 0, -1);
        Quaternion startRotation = transform.rotation;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }

    public IEnumerator FrameBoundsFor2DSprites(Bounds bounds, Vector3 center, float duration)
    {
        float distanceFromBounds = bounds.size.magnitude;
        Vector3 cameraPosition = center - (Vector3.forward * distanceFromBounds) + new Vector3(0, 1, -2);
        Quaternion cameraRotation = Quaternion.LookRotation(center - cameraPosition);

        yield return SmoothMoveCamera(cameraPosition, cameraRotation, duration);
    }

    private Vector3 ComputeWobbleOffset()
    {
        float wobbleX = Mathf.Sin(wobbleTime * wobbleFrequency) * wobbleMagnitude;
        float wobbleY = Mathf.Sin((wobbleTime + Mathf.PI * 0.5f) * wobbleFrequency) * wobbleMagnitude;
        return transform.right * wobbleX + transform.up * wobbleY;
    }
}
