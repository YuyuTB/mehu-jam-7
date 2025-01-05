using UnityEngine;

public class EnemySprites : MonoBehaviour
{
    private Sprite _attackingSprite;
    private Sprite _walkingSprite;
    private RuntimeAnimatorController _walkingAnimator;
    
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private EnemyMovement _enemyMovement;
    private EnemyAttack _enemyAttack;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _animator = GetComponent<Animator>();
        SetSprites();
    }

    void Update()
    {
        FlipSprite();
        SetActiveSprite();
    }

    private void SetActiveSprite()
    {
        if (_enemyAttack.isAttacking)
        {
            _spriteRenderer.sprite = _attackingSprite;
            _animator.runtimeAnimatorController = null;
        }
        else
        {
            _spriteRenderer.sprite = _walkingSprite;
            _animator.runtimeAnimatorController = _walkingAnimator;
        }
    }

    private void FlipSprite()
    {
        _spriteRenderer.flipX = _enemyMovement.isGoingLeft;
    }
    
    private void SetSprites()
    {
        string basePathSmall = "Sprites/Enemy/";
        
        _walkingSprite =  Resources.Load<Sprite>(basePathSmall + "enemy");
        _walkingAnimator = Resources.Load<RuntimeAnimatorController>(basePathSmall + "Animations/enemy_0");
        _attackingSprite =  Resources.Load<Sprite>(basePathSmall + "enemy_attacking");
    }
}
