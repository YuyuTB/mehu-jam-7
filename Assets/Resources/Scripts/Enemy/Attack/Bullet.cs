using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float ProjectileSpeed = 10f;
    private const float ProjectileLifetime = 0.5f;
    private Vector2 _direction;
    
    private bool _isGoingLeft;
    
    private SpriteRenderer _spriteRenderer;
    private EnemyMovement _enemyMovement;
    private AudioSource _audioSource;
    
    void Update()
    {
        Move();
        CountdownToExpire();
    }
    
    // Initialize the fireball with the character movement on instantiation
    public void Initialize(
        EnemyMovement enemyMovement,
        Vector2 direction
    )
    {
        _audioSource = GetComponent<AudioSource>();
        DefineAudioSource();
        PlayAudio();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyMovement = enemyMovement;
        _spriteRenderer.flipX = _enemyMovement.isGoingLeft;
        _isGoingLeft = _enemyMovement.isGoingLeft;
        _direction = direction.normalized;
    }

    private void Move()
    {
        transform.Translate(
                _direction * (ProjectileSpeed * Time.deltaTime)
        );
    }
    
    private void DefineAudioSource()
    {
        _audioSource.clip = Resources.Load<AudioClip>("Audio/SFX/Shoot");
        _audioSource.time = 0.6f;
    }
    
    private void PlayAudio()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }
    
    private void CountdownToExpire()
    {
        Destroy(gameObject, ProjectileLifetime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent.GetComponent<CharacterDeath>().KillCharacter();
        }
        
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
