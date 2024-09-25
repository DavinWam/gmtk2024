using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource audioSource; // The AudioSource component to play sounds from

    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource != null){
            UpdateVolume();
        }

        
    }
    
    void Update(){
        UpdateVolume();
    }


    public void UpdateVolume()
    {
        // Update the audio source volume based on the current global volume
        audioSource.volume = AudioListener.volume;
    }
}
