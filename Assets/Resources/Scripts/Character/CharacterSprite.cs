using System;
using UnityEngine;

public class CharacterSprite : MonoBehaviour
{
    // The sprites and animators for the character
    // Index : 0 Small character, 1 Big character
    [SerializeField] private int totalCharacterTypes = 2;
    
    private Sprite[] _idleSprites;
    private Sprite[] _runningSprites;
    private RuntimeAnimatorController[] _idleAnimators;
    private RuntimeAnimatorController[] _runningAnimators;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private CharacterMovement _characterMovement;

    private void Awake()
    {
        _idleSprites = new Sprite[totalCharacterTypes];
        _runningSprites = new Sprite[totalCharacterTypes];
        _idleAnimators = new RuntimeAnimatorController[totalCharacterTypes];
        _runningAnimators = new RuntimeAnimatorController[totalCharacterTypes];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _characterMovement = GetComponent<CharacterMovement>();

        DefineCharacterSprites();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfRunning();
    }

    private void CheckIfRunning()
    {
        /*
            Checks if the character is moving sideways
            and sets the sprite and animator accordingly
         */
        if (_characterMovement.isGoingLeft || _characterMovement.isGoingRight)
        {
            // Mirrors the walking sprite if the character is going left
            _spriteRenderer.flipX = _characterMovement.isGoingLeft;

            _spriteRenderer.sprite = _runningSprites[0];
            _animator.runtimeAnimatorController = _runningAnimators[0];
        }
        else
        {
            _spriteRenderer.sprite = _idleSprites[0];
            _animator.runtimeAnimatorController = _idleAnimators[0];
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