using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    
    public bool isGoingLeft;
    
    private Rigidbody2D _rb;
    private EnemyAttack _enemyAttack;
    private AudioSource _audioSource;
    
    public float loopStartTime = 0.5f; // Start time in seconds
    public float loopEndTime = 3.0f;   // End time in seconds

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _audioSource = GetComponent<AudioSource>();
        DefineAudioSource();
        PlayAudio();
    }

    private void Update()
    {
        // Stop to shoot if attacking
        if (!_enemyAttack.isAttacking)
        {
            Move();
            _audioSource.UnPause();
        }
        else
        {
            _audioSource.Pause();
        }
        
        // Check if the audio has passed the loop end time
        if (_audioSource.time >= loopEndTime)
        {
            // Set the time back to the loop start time
            _audioSource.time = loopStartTime;
        }
    }
    
    private void DefineAudioSource()
    {
        _audioSource.clip = Resources.Load<AudioClip>("Audio/SFX/EnemyWalk");
    }
    
    private void PlayAudio()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
            _audioSource.loop = true;
        }
    }
    
    private void Move()
    {
        float moveSpeed = speed * Time.deltaTime;
        Vector3 movement = new Vector3(moveSpeed, 0, 0);
        _rb.transform.position += movement;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Traveling_Stop"))
        {
            speed *= -1;
            isGoingLeft = !isGoingLeft;
        }
    }
}
