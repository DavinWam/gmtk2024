using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Orient the sprite towards the camera
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                         mainCamera.transform.rotation * Vector3.up);
    }
}
