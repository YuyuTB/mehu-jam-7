using UnityEngine;

public class CharacterSprite : MonoBehaviour
{
    // The sprites and animators for the character
    // Index : 0 Small character, 1 Big character
    [SerializeField] private int totalCharacterTypes = 2;
    private bool _isMoving;
    private bool _wasMovingBeforeJump;
    
    private Sprite[] _idleSprites;
    private Sprite[] _runningSprites;
    private RuntimeAnimatorController[] _idleAnimators;
    private RuntimeAnimatorController[] _runningAnimators;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private CharacterMovement _characterMovement;
    private CharacterResolution _characterResolution;

    private void Awake()
    {
        _idleSprites = new Sprite[totalCharacterTypes];
        _runningSprites = new Sprite[totalCharacterTypes];
        _idleAnimators = new RuntimeAnimatorController[totalCharacterTypes];
        _runningAnimators = new RuntimeAnimatorController[totalCharacterTypes];
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
    }
}