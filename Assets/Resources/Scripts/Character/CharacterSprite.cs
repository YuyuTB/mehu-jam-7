using System.Collections;
using UnityEngine;

public class CharacterSprite : MonoBehaviour
{
    private bool _isMoving;
    private bool _wasMovingBeforeJump;
    
    // Sprites Index : 0 Small character, 1 Big character
    private Sprite[] _idleSprites;
    private Sprite[] _runningSprites;
    private RuntimeAnimatorController[] _idleAnimators;
    private RuntimeAnimatorController[] _runningAnimators;
    // Big character specifics
    private Sprite _attackingSprite;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private CharacterMovement _characterMovement;
    private CharacterResolution _characterResolution;

    private void Awake()
    {
        _idleSprites = new Sprite[2];
        _runningSprites = new Sprite[2];
        _idleAnimators = new RuntimeAnimatorController[2];
        _runningAnimators = new RuntimeAnimatorController[2];
    }
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _characterMovement = GetComponent<CharacterMovement>();
        _characterResolution = GetComponent<CharacterResolution>();
        
        DefineCharacterSprites();
    }
    
    void Update()
    {
        CheckIfRunning();
        ApplySpritesAndAnimations();
    }

    private void CheckIfRunning()
    {
        // Update movement state based on character movement
        _isMoving = _characterMovement.isGoingLeft || _characterMovement.isGoingRight;
        
        // Update the state for when the character jumps
        if (_characterMovement.isGrounded)
        {
            _wasMovingBeforeJump = _isMoving;
        }
    }

    private void ApplySpritesAndAnimations()
    {
        // Determine character type: 0 for low definition, 1 for high definition
        int characterType = _characterResolution.isLowDefinition ? 0 : 1;
        
        if (_characterMovement.isMovementStoppedForAttack)
        {
            // Use attacking sprite and disable animations
            _spriteRenderer.sprite = _attackingSprite;
            _spriteRenderer.flipX = _characterMovement.isGoingLeft;
            _animator.runtimeAnimatorController = null;
            return;
        }
        
        // Handle jumping state
        if (!_characterMovement.isGrounded)
        {
            // Use different sprites based on whether the character was moving before jumping
            _spriteRenderer.sprite = _wasMovingBeforeJump
                ? _runningSprites[characterType] // Jumped while running
                : _idleSprites[characterType];  // Jumped while idle
            _animator.runtimeAnimatorController = null; // Disable animations during jump
            return;
        }

        // Handle grounded states: set idle or running sprites and animations depending on movement
        if (_characterMovement.isGrounded)
        {
            _spriteRenderer.flipX = _characterMovement.isGoingLeft;
            _spriteRenderer.sprite = _isMoving
                ? _runningSprites[characterType]
                : _idleSprites[characterType];
            _animator.runtimeAnimatorController = _isMoving 
                ? _runningAnimators[characterType] 
                : _idleAnimators[characterType];
        }
    }

    private void DefineCharacterSprites()
    {
        // Defines the base path for the sprites
        string basePathSmall = "Sprites/Character/Small/";
        string basePathBig = "Sprites/Character/Big/";

        // Sets the character's sprites and animators
        // Small character
        _idleSprites[0] = Resources.Load<Sprite>(basePathSmall + "small_char_idle");
        _idleAnimators[0] = Resources.Load<RuntimeAnimatorController>(basePathSmall + "Animations/small_char_idle_0");
        _runningSprites[0] = Resources.Load<Sprite>(basePathSmall + "small_char_running");
        _runningAnimators[0] = Resources.Load<RuntimeAnimatorController>(basePathSmall + "Animations/small_char_running_0");

        // Big character
        _idleSprites[1] = Resources.Load<Sprite>(basePathBig + "big_char_idle");
        _idleAnimators[1] = Resources.Load<RuntimeAnimatorController>(basePathBig + "Animations/big_char_idle_0");
        _runningSprites[1] = Resources.Load<Sprite>(basePathBig + "big_char_running");
        _runningAnimators[1] = Resources.Load<RuntimeAnimatorController>(basePathBig + "Animations/big_char_running_0");
        // Attack sprite and animator
        _attackingSprite = Resources.Load<Sprite>(basePathBig + "big_char_attacking");
    }
}