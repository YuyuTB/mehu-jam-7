using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float attackStopDuration = 0.3f;
    private bool _isTouchingWallLeft;
    private bool _isTouchingWallRight;

    public bool isGoingRight;
    public bool isGoingLeft;
    public bool isGrounded;
    public bool isMovementStoppedForAttack;
    
    private Rigidbody2D _rb;
    private float _gravity;
    private Vector3 _velocity;
    private CharacterResolution _characterResolution;
    
    private AudioSource _audioSource;
    
    public float loopStartTime = 0.5f; // Start time in seconds
    public float loopEndTime = 2.0f;   // End time in seconds

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _characterResolution = GetComponent<CharacterResolution>();
        _audioSource = GetComponent<AudioSource>();
        _gravity = Physics.gravity.y;
        DefineAudioSource();
        PlayAudio();
    }

    void Update()
    {
        if (!isMovementStoppedForAttack)
        {
            CheckIfRunning();
        }
        
        // Check if the audio has passed the loop end time
        if (_audioSource.time >= loopEndTime)
        {
            // Set the time back to the loop start time
            _audioSource.time = loopStartTime;
        }
    }

    void FixedUpdate()
    {
        if (!isMovementStoppedForAttack)
        {
            Move();
            ApplyGravity();
            Jump();
        }
        
        // Apply the vertical velocity
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _velocity.y);
    }
    
    private void DefineAudioSource()
    {
        _audioSource.clip = Resources.Load<AudioClip>("Audio/SFX/Walk");
    }
    
    private void PlayAudio()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
            _audioSource.loop = true;
        }
    }
    
    private void CheckIfRunning()
    {
        isGoingLeft = Input.GetAxis("Horizontal") < 0;
        isGoingRight = Input.GetAxis("Horizontal") > 0;
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        
        // Returns if no input or if the character is touching a wall in the direction of movement
        if (horizontal == 0)
        {
            _audioSource.Pause();
            return;
        }
        if (_isTouchingWallLeft 
            && horizontal < 0)
        {
            _audioSource.Pause();
            return;
        } 
        if (_isTouchingWallRight && horizontal > 0)
        {
            _audioSource.Pause();
            return;
        }
        
        _audioSource.UnPause();
        
        float moveSpeed = speed * horizontal * Time.deltaTime;
        Vector3 movement = new Vector3(moveSpeed, 0, 0);
        _rb.transform.position += movement;
    }

    private void Jump()
    {
        jumpHeight = _characterResolution.isLowDefinition ? 12f : 15f;
        
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            // Play the jump sound effect
            _audioSource.clip = Resources.Load<AudioClip>("Audio/SFX/Jump");
            _audioSource.loop = false;
            _audioSource.Play();
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * _gravity);
            // Go back to the walk sound effect
            _audioSource.clip = Resources.Load<AudioClip>("Audio/SFX/Walk");
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }

    private void ApplyGravity()
    {
        if (!isGrounded)
        {
            _velocity.y += _gravity * fallMultiplier * Time.deltaTime;
        }
        else if (_velocity.y < 0)
        {
            _velocity.y = 0f;
        }
    }
    
    public void StopMovementForAttack()
    {
        StartCoroutine(StopMovementCoroutine());
    }

    // Stops the character's movement for a short duration
    private IEnumerator StopMovementCoroutine()
    {
        isMovementStoppedForAttack = true;
        _velocity.y = 0f;
        yield return new WaitForSeconds(attackStopDuration);
        isMovementStoppedForAttack = false;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") 
            && !_isTouchingWallLeft
            && !_isTouchingWallRight)
        {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("Wall")
            && isGoingLeft)
        {
            _isTouchingWallLeft = true;
        } 
        else if (other.gameObject.CompareTag("Wall")
            && isGoingRight)
        {
            _isTouchingWallRight = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        
        if (other.gameObject.CompareTag("Wall"))
        {
            _isTouchingWallLeft = false;
            _isTouchingWallRight = false;
        }

        if (other.gameObject.CompareTag("Soup"))
        {
            _audioSource.clip = Resources.Load<AudioClip>("Audio/SFX/Drink");
            _audioSource.time = 0.05f;
            _audioSource.loop = false;
            _audioSource.Play();

            StartCoroutine(StopMovementCoroutine());
            StartCoroutine(ResumeWalkingSoundAfterDrink());
        }
    }
    private IEnumerator ResumeWalkingSoundAfterDrink()
    {
        // Wait for the drinking sound to finish
        yield return new WaitForSeconds(_audioSource.clip.length - 0.05f);

        _audioSource.clip = Resources.Load<AudioClip>("Audio/SFX/Move");
        _audioSource.loop = true;
        _audioSource.time = loopStartTime;
        _audioSource.Play();
    }
}