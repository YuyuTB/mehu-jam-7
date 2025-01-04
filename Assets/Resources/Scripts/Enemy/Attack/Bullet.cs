using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float ProjectileSpeed = 10f;
    private Vector2 _direction;
    
    private bool _isGoingLeft;
    
    private SpriteRenderer _spriteRenderer;
    private EnemyMovement _enemyMovement;
    
    void Update()
    {
        Move();
    }
    
    // Initialize the fireball with the character movement on instantiation
    public void Initialize(
        EnemyMovement enemyMovement,
        Vector2 direction
    )
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyMovement = enemyMovement;
        _spriteRenderer.flipX = _enemyMovement.isGoingLeft;
        _isGoingLeft = _enemyMovement.isGoingLeft;
        _direction = -direction.normalized;
        _direction.y = 0;
    }
    
    private void Move()
    {
        transform.Translate(
            _direction * (ProjectileSpeed * Time.deltaTime * (_isGoingLeft ? -1 : 1))
        );
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
