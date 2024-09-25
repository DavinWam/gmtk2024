using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f; // Duration of the shake effect
    public float shakeMagnitude = 0.1f; // Magnitude of the shake effect

    private Vector3 initialPosition; // To store the original position of the camera
    private float remainingShakeTime;
    public AudioSource audioSource;
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (remainingShakeTime > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            remainingShakeTime -= Time.deltaTime;

            if (remainingShakeTime <= 0f)
            {
                transform.localPosition = initialPosition;
            }
        }
    }

    public void TriggerShake(float duration, float magnitude)
    {
        audioSource.Play();
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        remainingShakeTime = duration;
    }

    public void TriggerShake()
    {
        remainingShakeTime = shakeDuration;
    }
}
