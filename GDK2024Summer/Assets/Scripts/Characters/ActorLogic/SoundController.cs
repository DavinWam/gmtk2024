using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isPlayingRunningSound = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRunningSound()
    {
        if (audioSource != null && !isPlayingRunningSound)
        {
            audioSource.loop = true;
            audioSource.Play();
            isPlayingRunningSound = true;
        }
    }

    public void StopRunningSound()
    {
        if (audioSource != null && isPlayingRunningSound)
        {
            audioSource.Stop();
            isPlayingRunningSound = false;
        }
    }
}