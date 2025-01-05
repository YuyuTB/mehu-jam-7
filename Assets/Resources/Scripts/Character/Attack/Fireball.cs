using UnityEngine;

public class Fireball : MonoBehaviour
{
    private const float ProjectileSpeed = 10f;
    private const float ProjectileLifetime = 0.5f;
    
    private bool _isGoingLeft;

    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;
    private CharacterMovement _characterMovement;
    
    void Update()
    {
        Move();
        CountdownToExpire();
    }
    
    // Initialize the fireball with the character movement on instantiation
    public void Initialize(CharacterMovement characterMovement)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        DefineAudioSource();
        // When the fireball is instantiated, the sound starts at 0.5 seconds of the total sound duration
        _audioSource.time = 0.5f;
        _audioSource.Play();
        _characterMovement = characterMovement;
        _spriteRenderer.flipX = _characterMovement.isGoingLeft;
        _isGoingLeft = _characterMovement.isGoingLeft;
    }
    
    private void CountdownToExpire()
    {
        Destroy(gameObject, ProjectileLifetime);
    }

    private void DefineAudioSource()
    {
        _audioSource.clip = Resources.Load<AudioClip>("Audio/SFX/Fireball");
    }
    
    private void Move()
    {
        transform.Translate(
            Vector2.right * (ProjectileSpeed * Time.deltaTime * (_isGoingLeft ? -1 : 1))
        );
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _audioSource.clip = Resources.Load<AudioClip>("Audio/SFX/KillEnemy");
            _audioSource.time = 0.5f;
            _audioSource.Play();
            Destroy(other.transform.parent.gameObject);
        }
        
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
