using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterDeath : MonoBehaviour
{
    private AudioSource _audioSource;
    
    public void KillCharacter()
    {
        _audioSource = transform.GetComponentInChildren<AudioSource>();
        _audioSource.clip = Resources.Load<AudioClip>("Audio/SFX/GetKilled");
        _audioSource.Play();
        StartCoroutine(WaitAndContinue());
        SceneManager.LoadScene("DeathMenu");
    }
    
    private IEnumerator WaitAndContinue()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
