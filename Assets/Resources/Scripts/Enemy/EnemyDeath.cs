using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private AudioSource _audioSource;
    
    private void OnDestroy()
    {
        DefineAudioSource();
        PlayAudio();
    }
    
    private void DefineAudioSource()
    {
       
    }

    private void PlayAudio()
    {
        // When the fireball is instantiated, the sound starts at 0.5 seconds of the total sound duration
        _audioSource.time = 0.5f;
        _audioSource.Play();
    }
}
