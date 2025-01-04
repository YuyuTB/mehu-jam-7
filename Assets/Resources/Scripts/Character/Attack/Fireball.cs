using UnityEngine;

public class Fireball : MonoBehaviour
{
    private const float ProjectileSpeed = 10f;
    
    private bool _isGoingLeft;
    
    private SpriteRenderer _spriteRenderer;
    private CharacterMovement _characterMovement;
    
    void Update()
    {
        Move();
    }
    
    // Initialize the fireball with the character movement on instantiation
    public void Initialize(CharacterMovement characterMovement)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _characterMovement = characterMovement;
        _spriteRenderer.flipX = _characterMovement.isGoingLeft;
        _isGoingLeft = _characterMovement.isGoingLeft;
    }
    
    private void Move()
    {
        transform.Translate(
            Vector2.right * (ProjectileSpeed * Time.deltaTime * (_isGoingLeft ? -1 : 1))
        );
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ennemy"))
        {
            Destroy(gameObject);
        }
    }
}
